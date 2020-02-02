using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipHealthComponent : MonoBehaviour
{
  public float TotalShipHealth = 100;
  public float VictoryTime = 300;
  public float ShipDamageRate = 1;
  public float ShipRepairRate = 1;

  public float ShipHealthHalf = 50.0f;
  public float ShipHealthWarning = 25.0f;
  public float ShipHealthCritical = 10.0f;

  [SerializeField]
  private SoundBank _shipHealthHalfAlert = null;

  [SerializeField]
  private SoundBank _shipHealthWarningAlert = null;

  [SerializeField]
  private SoundBank _shipHealthCriticalAlert = null;

  private float _currentShipHealth;
  public float CurrentShipHealthGame
  {
    get { return _currentShipHealth; }
  }

  private float _shipAliveTimer;
  public float ShipAliveTimer
  {
    get { return _shipAliveTimer; }
  }

  private int _damagedDeviceCount = 0;
  public int DamagedDeviceCount
  {
    get { return _damagedDeviceCount; }
  }

  public bool IsShipAlive
  {
    get { return _currentShipHealth > 0.0f; }
  }

  public float ShipHealthFraction
  {
    get { return _currentShipHealth / TotalShipHealth; }
  }

  public bool IsShipVictory
  {
    get { return _shipAliveTimer >= VictoryTime; }
  }

  public float ShipVictoryFraction
  {
    get { return _shipAliveTimer / VictoryTime; }
  }

  public void OnStartedGame()
  {
    _currentShipHealth = TotalShipHealth;
    _damagedDeviceCount = 0;
  }

  public void OnCompletedGame()
  {
    _currentShipHealth = TotalShipHealth;
  }

  public void OnDeviceBecameDamaged()
  {
    _damagedDeviceCount++;
  }

  public void OnDeviceBecameFixed()
  {
    _damagedDeviceCount = Mathf.Max(_damagedDeviceCount - 1, 0);
  }

  void Update()
  {
    if (IsShipAlive)
    {
      if (_damagedDeviceCount > 0)
      {
        float previousShipHealth = _currentShipHealth;
        _currentShipHealth = Mathf.Max(_currentShipHealth - ShipDamageRate * _damagedDeviceCount * Time.deltaTime, 0.0f);

        PostShipHealthAlerts(previousShipHealth, _currentShipHealth);
      }
      else
      {
        _currentShipHealth = Mathf.Min(_currentShipHealth + ShipRepairRate * Time.deltaTime, TotalShipHealth);
      }
    }

    if (IsShipAlive)
    {
      _shipAliveTimer += Time.deltaTime;
    }
  }

  void PostShipHealthAlerts(float PreviousHealth, float NewHealth)
  {
    if (PreviousHealth > ShipHealthCritical && NewHealth <= ShipHealthCritical)
    {
      if (_shipHealthCriticalAlert != null)
      {
        AudioManager.Instance.PlaySound(_shipHealthCriticalAlert);
      }
    }
    else if (PreviousHealth > ShipHealthWarning && NewHealth <= ShipHealthWarning)
    {
      if (_shipHealthWarningAlert != null)
      {
        AudioManager.Instance.PlaySound(_shipHealthWarningAlert);
      }
    }
    else if (PreviousHealth > ShipHealthHalf && NewHealth <= ShipHealthHalf)
    {
      if (_shipHealthHalfAlert != null)
      {
        AudioManager.Instance.PlaySound(_shipHealthHalfAlert);
      }
    }
  }
}
