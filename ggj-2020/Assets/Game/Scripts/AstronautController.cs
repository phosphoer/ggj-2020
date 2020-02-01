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
}