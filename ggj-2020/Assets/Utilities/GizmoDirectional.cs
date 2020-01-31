using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class GizmoDirectional : MonoBehaviour
{
  public float Scale = 1.0f;
  public Color Color = Color.white;

#if UNITY_EDITOR

  private void OnDrawGizmos()
  {
    Vector3 bottomLeft = transform.position - transform.right * Scale * 0.5f * transform.localScale.x;
    Vector3 bottomRight = transform.position + transform.right * Scale * 0.5f * transform.localScale.x;
    Vector3 top = transform.position + transform.forward * Scale * 2.0f * transform.localScale.z;
    Handles.color = Color;
    Handles.DrawLine(bottomLeft, top);
    Handles.DrawLine(bottomRight, top);
    Handles.DrawLine(bottomRight, bottomLeft);

    bottomLeft += -transform.forward * 0.1f - transform.right * 0.1f;
    bottomRight += -transform.forward * 0.1f + transform.right * 0.1f;
    top += transform.forward * 0.1f;
    Handles.DrawLine(bottomLeft, top);
    Handles.DrawLine(bottomRight, top);
    Handles.DrawLine(bottomRight, bottomLeft);
  }
#endif
}