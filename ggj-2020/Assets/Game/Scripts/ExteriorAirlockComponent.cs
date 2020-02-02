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

  [SerializeField]
  private SoundBank _ventSound = null;

  private EAirlockState _currentAirlockState;

  private void Start()
  {
    _currentAirlockState = EAirlockState.Closed;
    OnAirlockStateChanged(EAirlockState.Closed);
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

  private IEnumerator WooshAsync()
  {
    for (float timer = 0; timer < 1.5f; timer += Time.deltaTime)
    {
      yield return null;
    }

    WaitForFixedUpdate fixedUpdate = new WaitForFixedUpdate();
    Vector3 suctionPoint = transform.position;
    while (CurrentAirlockState == EAirlockState.Open)
    {
      for (int i = 0; i < _room.RoomInhabitants.Count; ++i)
      {
        RoomInhabitantComponent inhabitant = _room.RoomInhabitants[i];
        ApplyWoosh(inhabitant, suctionPoint);
      }

      yield return fixedUpdate;
    }
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

    if (newState == EAirlockState.Open)
    {
      StartCoroutine(WooshAsync());

      if (_ventSound != null)
      {
        AudioManager.Instance.FadeInSound(gameObject, _ventSound, 0.25f);
      }
    }
    else if (newState == EAirlockState.Closed)
    {
      if (_ventSound != null)
      {
        AudioManager.Instance.FadeOutSound(gameObject, _ventSound, 0.25f);
      }
    }
  }
}
