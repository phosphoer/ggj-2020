using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VentSwitchComponent : LeverComponent
{
  public GameObject SwitchHandle;

  // Start is called before the first frame update
  public override void Start()
  {
      base.Start();
      UpdateLeverTransform(CurrentLeverState);
  }

  public override void OnLeverStateChanged(ELeverState newState)
  {
    base.OnLeverStateChanged(newState);
    UpdateLeverTransform(newState);
  }

  private void UpdateLeverTransform(ELeverState state)
  {
    if (SwitchHandle == null)
      return;

    Transform pivot= SwitchHandle.transform.parent;
    switch(state)
    {
    case ELeverState.TurnedOff:
      pivot.localRotation= Quaternion.Euler(0, 45, 0);
      break;
    case ELeverState.TurnedOn:
      pivot.localRotation= Quaternion.Euler(0, -45, 0);
      break;
    }
  }
}
