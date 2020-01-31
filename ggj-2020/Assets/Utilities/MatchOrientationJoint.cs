using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MatchOrientationJoint : MonoBehaviour
{
  public Transform Target;
  public float Torque = 10.0f;

  private Rigidbody _rb;

  private void Start()
  {
    _rb = GetComponent<Rigidbody>();
  }

  private void FixedUpdate()
  {
    Quaternion toTargetRotation = Target.rotation * Quaternion.Inverse(transform.rotation);
    float angle = Mathf.Asin(toTargetRotation.w);
    if (float.IsNaN(angle))
      angle = 0.0f;
    _rb.AddTorque(toTargetRotation.x * Torque * angle, toTargetRotation.y * Torque * angle, toTargetRotation.z * Torque * angle);
  }
}