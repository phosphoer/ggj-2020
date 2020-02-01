using UnityEngine;

public class CameraControllerGame : CameraControllerBase
{
  public RangedFloat ZoomRange = new RangedFloat(10, 30);

  private void Update()
  {
    Vector3 playerCenter = Vector3.zero;
    for (int i = 0; i < PlayerManager.Instance.Players.Count; ++i)
    {
      PlayerAstronautController player = PlayerManager.Instance.Players[i];
      playerCenter += player.Astronaut.transform.position;
    }

    // Snap to desired pos to avoid stuttering 
    Vector3 desiredPos = playerCenter.WithY(ZoomRange.MinValue);
    MountPoint.position = desiredPos;

    Quaternion desiredRot = Quaternion.LookRotation(playerCenter - MountPoint.position, Vector3.forward);
    MountPoint.rotation = Mathfx.Damp(MountPoint.rotation, desiredRot, 0.5f, Time.deltaTime * 5);
  }
}
