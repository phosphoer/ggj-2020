using UnityEngine;

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
  public Vector3 MoveVector
  {
    get { return _moveVector; }
    set
    {
      _moveVector = Vector3.ClampMagnitude(value, 1);
    }
  }

  [SerializeField]
  private RoomInhabitantComponent _roomInhabitant = null;

  [SerializeField]
  private Rigidbody _rb = null;

  [SerializeField]
  private Animator _animator = null;

  [SerializeField]
  private Transform _visualRoot = null;

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

  private static readonly int kAnimIdleState = Animator.StringToHash("IdleState");
  private static readonly int kAnimEmoteState = Animator.StringToHash("EmoteState");
  private static readonly int kAnimEmoteName = Animator.StringToHash("Emote");

  public void PlayEmote(AstronautEmote emote)
  {
    _animator.SetFloat(kAnimEmoteState, (float)emote);
    _animator.Play(kAnimEmoteName, 0, 0);
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
    _zRot = Mathfx.Damp(_zRot, targetZRot, 0.5f, Time.deltaTime * 5);
    _visualRoot.localEulerAngles = _visualRoot.localEulerAngles.WithZ(_zRot);

    // Update idle anim state 
    // if (_roomInhabitant.IsInSpace)
    // {
    //   _currentIdleState = AstronautIdle.Panic;
    // }
    if (_moveVector.sqrMagnitude > 0.01f)
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

    _attackCooldownTimer -= Time.deltaTime;
  }

  private void FixedUpdate()
  {
    // Apply movement to physics
    _rb.AddForce(MoveVector * _acceleration * Time.deltaTime, ForceMode.Acceleration);
    _rb.velocity = Vector3.ClampMagnitude(_rb.velocity, _maxSpeed);
  }

  public bool IsPressingInteraction()
  {
    return _roomInhabitant != null ? _roomInhabitant.IsPressingInteraction : false;
  }

  public void PressInteraction()
  {
    if (_roomInhabitant != null && _attackCooldownTimer <= 0)
    {
      _attackCooldownTimer = _attackCooldown;
      _roomInhabitant.PressInteraction();
      PlayEmote(AstronautEmote.Attack);
    }
  }
  public void ReleaseInteraction()
  {
    if (_roomInhabitant != null)
    {
      _roomInhabitant.ReleaseInteraction();
    }
  }
}