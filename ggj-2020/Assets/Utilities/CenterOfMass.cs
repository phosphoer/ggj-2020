using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CenterOfMass : MonoBehaviour
{
  [SerializeField]
  private Vector3 Center = Vector3.zero;

  [SerializeField]
  private Vector3 InertiaTensor = Vector3.one;

  private void Start()
  {
    Rigidbody rb = GetComponent<Rigidbody>();
    rb.centerOfMass = Center;
    rb.inertiaTensor = InertiaTensor;
  }
}