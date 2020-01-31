using UnityEngine;

public class SoftFollowObject : MonoBehaviour
{
  public float InterpolateSpeed = 3;
  public Transform FollowTarget = null;
  public Vector3 WorldOffset;

  private void OnEnable()
  {
    if (FollowTarget != null)
    {
      transform.position = FollowTarget.position + WorldOffset;
    }
  }

  private void Update()
  {
    if (FollowTarget != null)
    {
      transform.position = Mathfx.Damp(transform.position, FollowTarget.position + WorldOffset, 0.5f, Time.deltaTime * InterpolateSpeed);
    }
  }
}