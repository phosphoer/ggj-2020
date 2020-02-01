using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairableDeviceComponent : InteratibleDeviceComponent
{
  public enum ERepairState
  {
    Fixed,
    Broken
  }

  private ERepairState _currentRepairState;
  public ERepairState CurrentRepairState
  {
    get { return _currentRepairState; }
  }

  [SerializeField]
  private GameObject _repairedGameObject = null;

  [SerializeField]
  private GameObject _brokenGameObject = null;

  // Start is called before the first frame update
  public override void Start()
  {
    base.Start();
     _currentRepairState= ERepairState.Fixed;
    OnRepairStateChanged(_currentRepairState);
  }

  public override void OnInteractionPressed()
  {
    base.OnInteractionPressed();

    SetRepairState(CurrentRepairState == ERepairState.Fixed ? ERepairState.Broken : ERepairState.Fixed);
  }

  public void SetRepairState(ERepairState newState)
  {
    if (newState != CurrentRepairState)
    {
      _currentRepairState= newState;
      OnRepairStateChanged(newState);
    }
  }

  public virtual void OnRepairStateChanged(ERepairState newState)
  {    
    if (_repairedGameObject != null)
    {
      _repairedGameObject.SetActive(newState == ERepairState.Fixed);
    }

    if (_brokenGameObject != null)
    {
      _brokenGameObject.SetActive(newState == ERepairState.Broken);
    }

    switch (newState)
    {
    case ERepairState.Fixed:
      GameStateManager.Instance.ShipHealth.OnDeviceBecameFixed();
      break;
    case ERepairState.Broken:
      GameStateManager.Instance.ShipHealth.OnDeviceBecameDamaged();
      break;
    }
  }
}
