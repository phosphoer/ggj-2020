using UnityEngine;

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
  private Transform _visualRoot = null;

  [SerializeField]
  private float _acceleration = 10;

  [SerializeField]
  private float _maxSpeed = 1;

  private Vector3 _moveVector;
  private float _zRot;

  private void Update()
  {
    // Orient to face movement direction
    if (_rb.velocity.sqrMagnitude > 0.01f)
    {
      Quaternion desiredRot = Quaternion.LookRotation(_rb.velocity, Vector3.up);
      transform.rotation = Mathfx.Damp(transform.rotation, desiredRot, 0.25f, Time.deltaTime * 5);
    }

    float targetZRot = Mathf.Abs(_moveVector.x) > 0.1f ? Mathf.Sign(_moveVector.x) * -90 : 0;
    _zRot = Mathfx.Damp(_zRot, targetZRot, 0.5f, Time.deltaTime * 5);
    _visualRoot.localEulerAngles = _visualRoot.localEulerAngles.WithZ(_zRot);
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
    if (_roomInhabitant != null)
    {
      _roomInhabitant.PressInteraction();
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