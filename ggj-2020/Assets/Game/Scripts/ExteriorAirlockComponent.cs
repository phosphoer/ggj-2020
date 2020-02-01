using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExteriorAirlockComponent : MonoBehaviour
{
  [SerializeField]
  private RoomComponent _room = null;

  [SerializeField]
  private Animator _hatchAnimator = null;

  public float VentForceScale = 1000.0f;

  public enum EAirlockState
  {
    Closed,
    Open
  }

  private EAirlockState _currentAirlockState;
  public EAirlockState CurrentAirlockState
  {
    get
    {
      return _currentAirlockState;
    }
  }

  private void Start()
  {
    _currentAirlockState = EAirlockState.Closed;
  }

  private void FixedUpdate()
  {
    // Suck everyone in the room out of the airlock
    if (_room != null && CurrentAirlockState == EAirlockState.Open)
    {
      Vector3 suctionPoint = GetAirlockCenter();

      foreach (RoomInhabitantComponent inhabitant in _room.RoomInhabitants)
      {
        Rigidbody rigidBody = inhabitant.PhysicsRigidBody;

        if (rigidBody != null)
        {
          Vector3 directionToAirlock = (suctionPoint - rigidBody.transform.position).normalized;
          Vector3 ventForce = directionToAirlock * VentForceScale * Time.deltaTime;

          rigidBody.AddForce(ventForce, ForceMode.Acceleration);
        }

        inhabitant.NotifySuckedIntoSpace();
      }
    }
  }

  public Vector3 GetAirlockCenter()
  {
    return gameObject.transform.position;
  }

  public void SetAirlockState(EAirlockState newState)
  {
    if (newState != CurrentAirlockState)
    {
      _currentAirlockState = newState;
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
