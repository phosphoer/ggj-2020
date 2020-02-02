using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExteriorAirlockComponent : MonoBehaviour
{
  public enum EAirlockState
  {
    Closed,
    Open
  }

  public EAirlockState CurrentAirlockState => _currentAirlockState;

  public float VentForceScale = 1000.0f;

  [SerializeField]
  private RoomComponent _room = null;

  [SerializeField]
  private Animator _hatchAnimator = null;

  [SerializeField]
  private CameraFocusPoint _cameraFocusPoint = null;

  [SerializeField]
  private List<GameObject> _warningEffects = null;

  private EAirlockState _currentAirlockState;
  private List<RoomInhabitantComponent> _wooshTargets = new List<RoomInhabitantComponent>();

  private void Start()
  {
    _currentAirlockState = EAirlockState.Closed;
    OnAirlockStateChanged(EAirlockState.Closed);
  }

  private void FixedUpdate()
  {
    // Suck everyone in the room out of the airlock
    if (_room != null && CurrentAirlockState == EAirlockState.Open)
    {
      Vector3 suctionPoint = GetAirlockCenter();

      for (int i = 0; i < _wooshTargets.Count; ++i)
      {
        RoomInhabitantComponent inhabitant = _wooshTargets[i];
        ApplyWoosh(inhabitant, suctionPoint);
      }

      for (int i = 0; i < _room.RoomInhabitants.Count; ++i)
      {
        RoomInhabitantComponent inhabitant = _room.RoomInhabitants[i];
        if (!_wooshTargets.Contains(inhabitant))
        {
          ApplyWoosh(inhabitant, suctionPoint);
        }
      }
    }
  }

  private void ApplyWoosh(RoomInhabitantComponent inhabitant, Vector3 suctionPoint)
  {
    Rigidbody rigidBody = inhabitant.PhysicsRigidBody;

    if (rigidBody != null)
    {
      Vector3 directionToAirlock = (suctionPoint - rigidBody.transform.position).WithY(0).normalized;

      bool isPastAirlock = Vector3.Dot(GetAirlockForward(), directionToAirlock) < 0;
      if (!isPastAirlock)
      {
        Vector3 ventForce = directionToAirlock * VentForceScale * Time.deltaTime;

        rigidBody.AddForce(ventForce, ForceMode.Acceleration);
      }
    }

    inhabitant.NotifySuckedIntoSpace();
  }

  public Vector3 GetAirlockCenter()
  {
    return transform.position;
  }

  public Vector3 GetAirlockForward()
  {
    return transform.right;
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

    if (_cameraFocusPoint != null)
    {
      // _cameraFocusPoint.enabled= newState == EAirlockState.Open;
    }

    foreach (GameObject effect in _warningEffects)
    {
      effect.SetActive(newState == EAirlockState.Open);
    }

    // Gather all woosh targets
    _wooshTargets.Clear();
    if (newState == EAirlockState.Open)
    {
      _wooshTargets.AddRange(_room.RoomInhabitants);
    }
  }
}
