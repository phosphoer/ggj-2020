using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteriorAirlockComponent : MonoBehaviour
{
  [SerializeField]
  private RoomComponent _room;

  [SerializeField]
  private Animator _hatchAnimator;

  public enum EAirlockState
  {
    Closed,
    Open
  }

  private EAirlockState _currentAirlockState;
  public EAirlockState CurrentAirlockState
  {
    get { return _currentAirlockState; }
  }

  private void Start()
  {
     _currentAirlockState= EAirlockState.Open;
    OnAirlockStateChanged(EAirlockState.Open);
  }

  public void SetAirlockState(EAirlockState newState)
  {
    if (newState != CurrentAirlockState)
    {
      _currentAirlockState= newState;
      OnAirlockStateChanged(newState);
    }
  }

  public void OnAirlockStateChanged(EAirlockState newState)
  {
    if (_hatchAnimator != null)
    {
      _hatchAnimator.SetBool("IsOpen", newState == EAirlockState.Open);
    }
  }
}
