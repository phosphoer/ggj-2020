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

    _isInteractionPressed = _astronaut.RoomInhabitant.CurrentDevice != null;
  }

  private void UpdateInteraction()
  {
    if (_isInteractionPressed && _astronaut.RoomInhabitant.CurrentDevice != null)
    {
      bool shouldInteract = true;
      InteratibleDeviceComponent device = _astronaut.RoomInhabitant.CurrentDevice;
      RepairableDeviceComponent repairable = device as RepairableDeviceComponent;
      if (repairable != null)
      {
        if (repairable.CurrentRepairState == RepairableDeviceComponent.ERepairState.Broken)
        {
          shouldInteract = false;
        }
      }

      if (shouldInteract)
      {
        _astronaut.PressInteraction();
      }
    }
  }
}
