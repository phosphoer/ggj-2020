using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyDispensorComponent : InteratibleDeviceComponent
{
  float ChargePerInteraction = 1;

  protected override void OnInteractionPressed(GameObject gameObject)
  {
    BatteryComponent battery = gameObject.GetComponentInChildren<BatteryComponent>();
    if (battery != null && !battery.HasCharge)
    {
      battery.AddCharge(ChargePerInteraction);
    }
  }

  public override bool DrainsBatteryOnInteraction()
  {
    return false;
  }
}
