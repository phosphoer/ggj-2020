using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergySinkController : InteratibleDeviceComponent
{
  [SerializeField]
  private SoundBank _doorAlert = null;

  public List<Animator> _doorAnimators;
  public List<GameObject> powerGears;
  
  public float gearTurnTime = 2;
  private float _gearTurnTimer = 0;

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
	
  private void Update ()
  {
	  if (_gearTurnTimer > 0)
	  {
		_gearTurnTimer -= Time.deltaTime;
		if (_gearTurnTimer <= 0)
		{
			foreach (GameObject gear in powerGears)
			{
				gear.SetActive(false);
			}
		}
	  }
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

          if (_doorAlert != null)
          {
            AudioManager.Instance.PlaySound(_doorAlert);
          }
        }
		foreach (GameObject gear in powerGears)
		{
			gear.SetActive(true);
		}
		_gearTurnTimer = gearTurnTime;
		
        _openedDoors++;
      }
    }
  }
}
