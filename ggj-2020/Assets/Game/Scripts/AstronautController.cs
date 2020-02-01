using UnityEngine;
using System.Collections.Generic;

public enum AstronautEmote
{
  Attack = 0,
  HitReact,
}

public enum AstronautIdle
{
  Idle = 0,
  Move,
  Stunned,
  Panic
}

public class AstronautController : MonoBehaviour
{
  public static IReadOnlyList<AstronautController> Instances => _instances;

  public event System.Action Died;
  public event System.Action Despawned;

  public Vector3 MoveVector
  {
    get { return _moveVector; }
    set
    {
      _moveVector = Vector3.ClampMagnitude(value, 1);
    }
  }

  public RoomInhabitantComponent RoomInhabitant => _roomInhabitant;
  public bool IsStunned => _stunTimer > 0;

  [SerializeField]
  private RoomInhabitantComponent _roomInhabitant = null;

  [SerializeField]
  private Rigidbody _rb = null;

  [SerializeField]
  private Animator _animator = null;

  [SerializeField]
  private Transform _visualRoot = null;

  [SerializeField]
  private GameObject[] _hideOnDie = null;

  [SerializeField]
  private GameObject[] _showOnDie = null;

  [SerializeField]
  private float _acceleration = 10;

  [SerializeField]
  private float _maxSpeed = 1;

  [SerializeField]
  private float _attackCooldown = 1;

  private Vector3 _moveVector;
  private float _zRot;
  private float _attackCooldownTimer;
  private float _idleBlend;
  private AstronautIdle _currentIdleState;
  private bool _isDead;
  private bool _isColliding;
  private float _stunTimer;

  private static List<AstronautController> _instances = new List<AstronautController>();

  private static readonly int kAnimIdleState = Animator.StringToHash("IdleState");
  private static readonly int kAnimEmoteState = Animator.StringToHash("EmoteState");
  private static readonly int kAnimEmoteName = Animator.StringToHash("Emote");

  public void PlayEmote(AstronautEmote emote)
  {
    _animator.SetFloat(kAnimEmoteState, (float)emote);
    _animator.Play(kAnimEmoteName, 0, 0);
  }

  public void PressInteraction()
  {
    if (_attackCooldownTimer <= 0)
    {
      _attackCooldownTimer = _attackCooldown;
      PlayEmote(AstronautEmote.Attack);

      if (_roomInhabitant.CurrentDevice != null)
      {
        _roomInhabitant.CurrentDevice.OnInteractionPressed();
      }
      else
      {
        TryWhackAstronaut();
      }
    }
  }

  private void OnEnable()
  {
    _instances.Add(this);
  }

  private void OnDisable()
  {
    _instances.Remove(this);
  }

  private void Update()
  {
    // Orient to face movement direction
    if (_rb.velocity.sqrMagnitude > 0.01f)
    {
      Quaternion desiredRot = Quaternion.LookRotation(_rb.velocity, Vector3.up);
      transform.rotation = Mathfx.Damp(transform.rotation, desiredRot, 0.25f, Time.deltaTime * 5);
    }

    // Roll based on movement
    float targetZRot = Mathf.Abs(_moveVector.x) > 0.1f ? Mathf.Sign(_moveVector.x) * -90 : 0;
    if (IsStunned)
    {
      targetZRot = 0;
    }

    _zRot = Mathfx.Damp(_zRot, targetZRot, 0.5f, Time.deltaTime * 5);
    _visualRoot.localEulerAngles = _visualRoot.localEulerAngles.WithZ(_zRot);

    // Update idle anim state 
    if (_roomInhabitant.IsBeingSuckedIntoSpace)
    {
      _currentIdleState = AstronautIdle.Panic;
    }
    else if (IsStunned)
    {
      _currentIdleState = AstronautIdle.Stunned;
    }
    else if (_moveVector.sqrMagnitude > 0.01f)
    {
      _currentIdleState = AstronautIdle.Move;
    }
    else
    {
      _currentIdleState = AstronautIdle.Idle;
    }

    // Blend idle state
    _idleBlend = Mathfx.Damp(_idleBlend, (float)_currentIdleState, 0.25f, Time.deltaTime * 5);
    _animator.SetFloat(kAnimIdleState, _idleBlend);

    // Handle sucked into space 
    if (_roomInhabitant.IsBeingSuckedIntoSpace)
    {
      if (_hideOnDie[0].activeSelf)
      {
        foreach (GameObject obj in _hideOnDie)
          obj.SetActive(false);

        foreach (GameObject obj in _showOnDie)
          obj.SetActive(true);
      }

      if (!_isDead && _roomInhabitant.Room == null)
      {
        _isDead = true;
        Died?.Invoke();
      }

      if (_isDead && !Mathfx.IsPointInViewport(transform.position, Camera.main))
      {
        Destroy(gameObject);
        Despawned?.Invoke();
        Destroy(gameObject);
        return;
      }
    }

    _attackCooldownTimer -= Time.deltaTime;
    _stunTimer -= Time.deltaTime;
  }

  private void OnCollisionEnter(Collision col)
  {
    _isColliding = true;
  }

  private void OnCollisionExit(Collision col)
  {
    _isColliding = false;
  }

  private void FixedUpdate()
  {
    // Move out of spaceship when sucked out
    if (_roomInhabitant.IsBeingSuckedIntoSpace)
    {
      if (_isColliding)
      {
        _rb.constraints = RigidbodyConstraints.None;
        _rb.AddForce(Vector3.up * Time.deltaTime * 10, ForceMode.Acceleration);
      }
    }
    // Apply movement to physics
    else
    {
      if (!IsStunned)
      {
        _rb.AddForce(MoveVector * _acceleration * Time.deltaTime, ForceMode.Acceleration);
      }

      _rb.velocity = Vector3.ClampMagnitude(_rb.velocity, _maxSpeed);
    }
  }

  private void TryWhackAstronaut()
  {
    for (int i = 0; i < AstronautController.Instances.Count; ++i)
    {
      AstronautController astro = AstronautController.Instances[i];
      if (astro != this)
      {
        Vector3 toAstro = astro.transform.position - transform.position;
        if (toAstro.magnitude < 3.0f && Vector3.Angle(transform.forward, toAstro) < 30)
        {
          astro.GetWhacked(transform.position);
          return;
        }
      }
    }
  }

  private void GetWhacked(Vector3 fromPos)
  {
    Debug.Log($"{name} got whacked");
    _rb.AddForce((transform.position - fromPos).normalized * 3, ForceMode.VelocityChange);
    PlayEmote(AstronautEmote.HitReact);
    _stunTimer = 5;
  }
}