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
  private float _moveForce = 10;

  private Vector3 _moveVector;

  private void Update()
  {
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
    _rb.AddForce(_moveVector * _moveForce * Time.fixedDeltaTime, ForceMode.Force);
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