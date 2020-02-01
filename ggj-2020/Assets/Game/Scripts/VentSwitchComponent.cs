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

  [SerializeField]
  private CameraFocusPoint _cameraFocusPoint = null;

  public float ResetDuration= 5.0f;

  private float _resetTimer;

  // Start is called before the first frame update
  public override void Start()
  {
    base.Start();
    UpdateLeverTransform(CurrentLeverState);
    UpdateAirlock(CurrentLeverState);
    UpdateCameraFocus(CurrentLeverState);
  }

  public void Update()
  {
    if (CurrentLeverState == ELeverState.TurnedOn)
    {
      _resetTimer+= Time.deltaTime;

      if (_resetTimer > ResetDuration)
      {
        SetLeverState(ELeverState.TurnedOff);
      }
    }
  }

  public override void OnLeverStateChanged(ELeverState newState)
  {
    base.OnLeverStateChanged(newState);
    _resetTimer = 0;
    UpdateLeverTransform(newState);
    UpdateAirlock(newState);
    UpdateCameraFocus(newState);
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

  private void UpdateCameraFocus(ELeverState state)
  {
    if (_cameraFocusPoint != null)
    {
      _cameraFocusPoint.enabled = state == ELeverState.TurnedOn;
    }
  }
}
