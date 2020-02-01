using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergySinkController : InteratibleDeviceComponent
{
  public float MaxEnergy= 10;

  private float _depositedEnergy= 0;
  public float DepositedEnergy
  {
    get { return _depositedEnergy; }
  }

  public float DepositFractionOfMax
  {
    get { return DepositedEnergy / MaxEnergy; }
  }

  public bool IsFull
  {
    get { return DepositedEnergy >= MaxEnergy; }
  }

  public override void OnInteractionPressed(GameObject gameObject)
  {
    base.OnInteractionPressed(gameObject);

    BatteryComponent battery= gameObject.GetComponentInChildren<BatteryComponent>();
    if (battery != null && battery.HasCharge)
    {
      _depositedEnergy= Mathf.Min(_depositedEnergy + battery.DrainCharge(), MaxEnergy);
    }
  }
}
