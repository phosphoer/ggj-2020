using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class SoftRelativeJoint : MonoBehaviour
{
  public Rigidbody ConnectedBody;
  public float Force = 10.0f;
  public float Torque = 10.0f;
  public float OppositeForceRatio = 0.5f;

  private Rigidbody _rb;
  private Transform _targetTransform;

  private void Start()
  {
    _rb = GetComponent<Rigidbody>();

    _targetTransform = new GameObject("soft-joint-target").transform;
    _targetTransform.SetParent(ConnectedBody.transform);
    _targetTransform.position = transform.position;
    _targetTransform.rotation = transform.rotation;
  }

  private void FixedUpdate()
  {
    Vector3 toDesiredPos = _targetTransform.position - transform.position;
    // _rb.position = _targetTransform.position;
    _rb.AddForce(toDesiredPos * Force);
    ConnectedBody.AddForce(-toDesiredPos * Force * OppositeForceRatio);

    Quaternion toTargetRotation = _targetTransform.rotation * Quaternion.Inverse(transform.rotation);
    _rb.AddTorque(toTargetRotation.x * Torque, toTargetRotation.y * Torque, toTargetRotation.z * Torque);
    ConnectedBody.AddTorque(toTargetRotation.x * -Torque * OppositeForceRatio, toTargetRotation.y * -Torque * OppositeForceRatio, toTargetRotation.z * -Torque * OppositeForceRatio);
  }
}