using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipHealthComponent : MonoBehaviour
{
  public float TotalShipHealth = 100;
  public float VictoryTime= 300;
  public float ShipDamageRate= 1;
  public float ShipRepairRate= 1;

  private float _currentShipHealth;
  public float CurrentShipHealth
  {
    get { return _currentShipHealth; }
  }

  private float _shipAliveTimer;
  public float ShipAliveTimer
  {
    get { return _shipAliveTimer; }
  }

  private int _damagedDeviceCount= 0;
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

  private bool _isGameRunning;

  // Start is called before the first frame update
  void Start()
  {
      _isGameRunning= false;
  }

  public void OnStartedGame()
  {
    _isGameRunning = true;
    _currentShipHealth= TotalShipHealth;
    _damagedDeviceCount= 0;
  }

  public void OnCompletedGame()
  {
    _isGameRunning = false;
    _currentShipHealth= TotalShipHealth;
  }

  public void OnDeviceBecameDamaged()
  {
    _damagedDeviceCount++;
  }

  public void OnDeviceBecameFixed()
  {
    _damagedDeviceCount= Mathf.Max(_damagedDeviceCount-1, 0);
  }

  void Update()
  {
    if (_isGameRunning)
    {
      if (IsShipAlive)
      {
        if (_damagedDeviceCount > 0)
        {
          _currentShipHealth= Mathf.Max(_currentShipHealth - ShipDamageRate*_damagedDeviceCount*Time.deltaTime, 0.0f);
        }
        else
        {
          _currentShipHealth= Mathf.Min(_currentShipHealth + ShipRepairRate*Time.deltaTime, TotalShipHealth);
        }
      }

      if (IsShipAlive)
      {
        _shipAliveTimer+= Time.deltaTime;
      }    
    }
  }
}
