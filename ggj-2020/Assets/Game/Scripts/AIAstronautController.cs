using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAstronautController : MonoBehaviour
{
  private bool _isInteractionPressed;

  [SerializeField]
  private RoomInhabitantComponent _roomInhabitant = null;

  [SerializeField]
  private AstronautController _astronaut = null;

  private Vector3 _moveDir;

  private void Update()
  {
    UpdateInput();
    UpdateInteraction();
  }

  private void OnCollisionEnter(Collision col)
  {
    _moveDir = Random.insideUnitSphere;
  }

  private void UpdateInput()
  {
    _moveDir += Random.insideUnitSphere * Time.deltaTime;
    _astronaut.MoveVector = _moveDir;
  }

  private void UpdateInteraction()
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
