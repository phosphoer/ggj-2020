using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyDispensorComponent : InteratibleDeviceComponent
{
  float ChargePerInteraction = 1;

  public override void OnInteractionPressed(GameObject gameObject)
  {
    base.OnInteractionPressed(gameObject);

    BatteryComponent battery= gameObject.GetComponentInChildren<BatteryComponent>();
    if (battery != null && !battery.HasCharge)
    {
      battery.AddCharge(ChargePerInteraction);
    }
  }
}
