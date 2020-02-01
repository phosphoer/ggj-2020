using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAstronautController : MonoBehaviour
{
  private RoomInhabitantComponent _roomInhabitant;
  private bool _isInteractionPressed;

  private void Start()
  {
    _roomInhabitant= GetComponent<RoomInhabitantComponent>();
  }

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
      if (_isInteractionPressed && !_roomInhabitant.IsUsingInteraction)
      { 
        _roomInhabitant.StartInteraction();
      }
      else if (!_isInteractionPressed && _roomInhabitant.IsUsingInteraction)
      { 
        _roomInhabitant.StopInteraction();
      }
    }
  }
}
