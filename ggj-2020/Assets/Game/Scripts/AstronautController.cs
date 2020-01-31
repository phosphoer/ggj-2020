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

  private Vector3 _moveVector;

  private void Update()
  {
    // Orient to face movement direction
  }

  private void FixedUpdate()
  {
    // Apply movement to physics
    _rb.AddForce(_moveVector * Time.fixedDeltaTime, ForceMode.Force);
  }
}