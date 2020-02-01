using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAstronautController : MonoBehaviour
{
  [SerializeField]
  private AstronautController _astronaut = null;

  private bool _isInteractionPressed;
  private Vector3 _moveDir;
  private int _repairDeviceIndex;

  private void Update()
  {
    UpdateInput();
    UpdateInteraction();
  }

  private void OnCollisionEnter(Collision col)
  {
    _moveDir = Random.insideUnitSphere;
  }

  private void UpdateInput()
  {
    _moveDir += Random.insideUnitSphere * Time.deltaTime;
    _astronaut.MoveVector = _moveDir;

    if (_repairDeviceIndex < RepairableDeviceComponent.Instances.Count)
    {
      RepairableDeviceComponent device = RepairableDeviceComponent.Instances[_repairDeviceIndex];
      float distToRepair = Vector3.Distance(_astronaut.transform.position, device.transform.position);
      if (distToRepair < 3)
      {
        _isInteractionPressed = true;
      }
    }
  }

  private void UpdateInteraction()
  {
    if (_isInteractionPressed && _astronaut.RoomInhabitant.CurrentDevice != null)
    {
      _astronaut.RoomInhabitant.CurrentDevice.OnInteractionPressed();
    }
  }
}
