using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverComponent : InteratibleDeviceComponent
{
  public enum ELeverState
  {
    TurnedOff,
    TurnedOn
  }

  public ELeverState InitialLeverState = ELeverState.TurnedOff;

  private ELeverState _currentLeverState;
  public ELeverState CurrentLeverState
  {
    get { return _currentLeverState; }
  }

  private void Start()
  {
    _currentLeverState = InitialLeverState;
  }

  public void SetLeverState(ELeverState newState)
  {
    if (newState != CurrentLeverState)
    {
      _currentLeverState = newState;
      OnLeverStateChanged(newState);
    }
  }
  protected override void OnInteractionPressed(GameObject gameObject)
  {
    SetLeverState(CurrentLeverState == ELeverState.TurnedOff ? ELeverState.TurnedOn : ELeverState.TurnedOff);
  }

  public virtual void OnLeverStateChanged(ELeverState newState)
  {

  }
}
