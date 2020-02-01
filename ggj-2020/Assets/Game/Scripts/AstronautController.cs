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
  private RoomInhabitantComponent _roomInhabitant= null;

  [SerializeField]
  private Rigidbody _rb = null;

  [SerializeField]
  private float _acceleration = 10;

  [SerializeField]
  private float _decceleration = 5;

  [SerializeField]
  private float _maxSpeed = 1;

  private Vector3 _moveVector;
  private Vector3 _currentVelocity;

  private void Update()
  {
    if (MoveVector.sqrMagnitude > 0.1f)
      _currentVelocity = Mathfx.Damp(_currentVelocity, MoveVector * _maxSpeed, 0.25f, Time.deltaTime * _acceleration);
    else
      _currentVelocity = Mathfx.Damp(_currentVelocity, MoveVector * _maxSpeed, 0.5f, Time.deltaTime * _decceleration);

    // Orient to face movement direction
    if (_rb.velocity.sqrMagnitude > 0.01f)
    {
      Quaternion desiredRot = Quaternion.LookRotation(_rb.velocity, Vector3.up);
      transform.rotation = Mathfx.Damp(transform.rotation, desiredRot, 0.25f, Time.deltaTime * 5);
    }
  }

  private void FixedUpdate()
  {
    // Apply movement to physics
    _rb.velocity = _currentVelocity;
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