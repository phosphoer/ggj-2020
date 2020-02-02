using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyDispensorComponent : InteratibleDeviceComponent
{
  public float ChargePerInteraction = 1.0f;
  public float BaseCooldownTimer= 3.0f;
  public float BrokenDeviceDelay= 0.5f;

  private float _cooldownTimer= 0;
  
  [SerializeField]
  private GameObject _emitterFX = null;

  private void Update()
  {
    if (_cooldownTimer > 0)
    {
      _cooldownTimer= Mathf.Max(_cooldownTimer - Time.deltaTime, 0);
      if (_cooldownTimer <= 0)
      {
        SetEmitterFXActive(true);
      }
    }
  }

  public override void OnInteractionPressed(GameObject gameObject)
  {
    base.OnInteractionPressed(gameObject);

    BatteryComponent battery= gameObject.GetComponentInChildren<BatteryComponent>();
    if (battery != null && !battery.HasCharge)
    {
      battery.AddCharge(ChargePerInteraction);
      StartCooldownTimer();
    }
  }

  private void StartCooldownTimer()
  {
    _cooldownTimer= BaseCooldownTimer + BrokenDeviceDelay * GameStateManager.Instance.ShipHealth.DamagedDeviceCount;
    SetEmitterFXActive(false);
  }

  private void SetEmitterFXActive(bool isActive)
  {
    if (_emitterFX != null)
    {
      _emitterFX.SetActive(isActive);
    }
  }

  public override bool DrainsBatteryOnInteraction()
  {
    return false;
  }
}
