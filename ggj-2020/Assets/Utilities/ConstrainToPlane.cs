using UnityEngine;

public class ConstrainToPlane : MonoBehaviour
{
  public enum Axis
  {
    X,
    Y,
    Z
  }

  public Transform PlaneTransform;
  public Axis ConstraintAxis;
  public bool UseLocalPosition;

  private void Update()
  {
    Vector3 pos = UseLocalPosition ? transform.localPosition : transform.position;
    Vector3 planePos = PlaneTransform.position;
    switch (ConstraintAxis)
    {
      case Axis.X:
        pos.x = planePos.x;
        break;
      case Axis.Y:
        pos.y = planePos.y;
        break;
      case Axis.Z:
        pos.z = planePos.z;
        break;
      default:
        break;
    }

    if (UseLocalPosition)
    {
      transform.localPosition = pos;
    }
    else
    {
      transform.position = pos;
    }
  }
}