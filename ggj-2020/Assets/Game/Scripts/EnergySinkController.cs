using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergySinkController : InteratibleDeviceComponent
{
  public List<Animator> _doorAnimators;

  private int _openedDoors = 0;
  public float openedDoors
  {
    get { return _openedDoors; }
  }

  public float OpenedFractionOfMax
  {
    get { return _doorAnimators.Count > 0 ? (float)_openedDoors / (float)_doorAnimators.Count : 0; }
  }

  public bool IsFull
  {
    get { return _openedDoors >= _doorAnimators.Count; }
  }

  protected override void OnInteractionPressed(GameObject gameObject)
  {
    BatteryComponent battery = gameObject.GetComponentInChildren<BatteryComponent>();
    if (battery != null && battery.HasCharge)
    {
      battery.DrainCharge();

      if (_openedDoors < _doorAnimators.Count)
      {
        if (_doorAnimators[_openedDoors] != null)
        {
          _doorAnimators[_openedDoors].SetBool("IsOpen", true);
        }

        _openedDoors++;
      }
    }
  }
}
