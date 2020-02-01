using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VentSwitchComponent : LeverComponent
{
  public GameObject SwitchHandle;

  [SerializeField]
  private ExteriorAirlockComponent _exteriorAirlock = null;

  [SerializeField]
  private InteriorAirlockComponent _interiorAirlock = null;

  // Start is called before the first frame update
  public override void Start()
  {
    base.Start();
    UpdateLeverTransform(CurrentLeverState);
    UpdateAirlock(CurrentLeverState);
  }

  public override void OnLeverStateChanged(ELeverState newState)
  {
    base.OnLeverStateChanged(newState);
    UpdateLeverTransform(newState);
    UpdateAirlock(CurrentLeverState);
  }

  private void UpdateLeverTransform(ELeverState state)
  {
    if (SwitchHandle == null)
      return;

    Transform pivot = SwitchHandle.transform.parent;
    switch (state)
    {
      case ELeverState.TurnedOff:
        pivot.localRotation = Quaternion.Euler(0, 45, 0);
        break;
      case ELeverState.TurnedOn:
        pivot.localRotation = Quaternion.Euler(0, -45, 0);
        break;
    }
  }

  private void UpdateAirlock(ELeverState state)
  {
    if (_exteriorAirlock != null)
    {
      if (state == ELeverState.TurnedOff)
      {
        _exteriorAirlock.SetAirlockState(ExteriorAirlockComponent.EAirlockState.Closed);
      }
      else
      {
        _exteriorAirlock.SetAirlockState(ExteriorAirlockComponent.EAirlockState.Open);
      }
    }

    if (_interiorAirlock != null)
    {
      if (state == ELeverState.TurnedOff)
      {
        _interiorAirlock.SetAirlockState(InteriorAirlockComponent.EAirlockState.Open);
      }
      else
      {
        _interiorAirlock.SetAirlockState(InteriorAirlockComponent.EAirlockState.Closed);
      }
    }
  }
}
