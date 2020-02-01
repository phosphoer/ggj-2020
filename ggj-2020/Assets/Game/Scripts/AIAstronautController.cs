using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAstronautController : MonoBehaviour
{
  private bool _isInteractionPressed;

  [SerializeField]
  private RoomInhabitantComponent _roomInhabitant= null;

  // Update is called once per frame
  void Update()
  {
    UpdateInput();
    UpdateInteraction();
  }
  void UpdateInput()
  {
    // TODO: Decide when to press interaction
    _isInteractionPressed= false;
  }

  void UpdateInteraction()
  {
    if (_roomInhabitant)
    {
      if (_isInteractionPressed && !_roomInhabitant.IsPressingInteraction)
      { 
        _roomInhabitant.PressInteraction();
      }
      else if (!_isInteractionPressed && _roomInhabitant.IsPressingInteraction)
      { 
        _roomInhabitant.ReleaseInteraction();
      }
    }
  }
}
