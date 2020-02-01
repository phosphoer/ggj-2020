using UnityEngine;

public class CameraControllerGame : CameraControllerBase
{
  public RangedFloat ZoomRange = new RangedFloat(10, 30);
  public float ViewportBorder = 0.25f;
  public float ZoomSensitivity = 2;

  private void Update()
  {
    Camera cam = CameraControllerStack.Instance.Camera;
    Vector3 playerCenter = Vector3.zero;
    float zoomDelta = 0;
    for (int i = 0; i < PlayerManager.Instance.Players.Count; ++i)
    {
      PlayerAstronautController player = PlayerManager.Instance.Players[i];
      playerCenter += player.Astronaut.transform.position;

      // Increase amount of desired zoom by how much player is off screen
      Vector3 viewportPos = cam.WorldToViewportPoint(player.Astronaut.transform.position);
      float maxViewPos = Mathf.Max(Mathf.Abs(viewportPos.x), Mathf.Abs(viewportPos.y));
      float delta = Mathf.Clamp((maxViewPos + ViewportBorder) - 1, -1, 1) * ZoomSensitivity;
      if (delta > 0)
        zoomDelta = Mathf.Max(zoomDelta + delta, 0);
      else
        zoomDelta += delta;
    }

    if (PlayerManager.Instance.Players.Count > 0)
    {
      playerCenter /= PlayerManager.Instance.Players.Count;
    }

    // Snap to desired pos to avoid stuttering 
    float desiredZoom = ZoomRange.Clamp(MountPoint.position.y + zoomDelta);
    float zoom = Mathfx.Damp(MountPoint.position.y, desiredZoom, 0.5f, Time.deltaTime * 5);
    Vector3 desiredPos = playerCenter.WithY(zoom);
    MountPoint.position = desiredPos;

    Quaternion desiredRot = Quaternion.LookRotation(playerCenter - MountPoint.position, Vector3.forward);
    MountPoint.rotation = Mathfx.Damp(MountPoint.rotation, desiredRot, 0.5f, Time.deltaTime * 5);
  }
}
