using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairableDeviceComponent : InteratibleDeviceComponent
{
  public enum ERepairState
  {
    Fixed,
    Broken
  }

  public static IReadOnlyList<RepairableDeviceComponent> Instances => _instances;

  private ERepairState _currentRepairState;
  public ERepairState CurrentRepairState
  {
    get { return _currentRepairState; }
  }

  [SerializeField]
  private GameObject _repairedGameObject = null;

  [SerializeField]
  private GameObject _brokenGameObject = null;

  [SerializeField]
  private SoundBank _brokenSound = null;

  [SerializeField]
  private SoundBank _fixedSound = null;

  private static List<RepairableDeviceComponent> _instances = new List<RepairableDeviceComponent>();

  private void Start()
  {
    _currentRepairState = ERepairState.Fixed;
    OnRepairStateChanged(_currentRepairState);
  }

  private void OnEnable()
  {
    _instances.Add(this);
  }

  private void OnDisable()
  {
    _instances.Remove(this);
  }

  protected override void OnInteractionPressed(GameObject gameObject)
  {
    SetRepairState(CurrentRepairState == ERepairState.Fixed ? ERepairState.Broken : ERepairState.Fixed);
  }

  public void SetRepairState(ERepairState newState)
  {
    if (newState != CurrentRepairState)
    {
      _currentRepairState = newState;
      OnRepairStateChanged(newState);
    }
  }

  public virtual void OnRepairStateChanged(ERepairState newState)
  {
    if (_repairedGameObject != null)
    {
      _repairedGameObject.SetActive(newState == ERepairState.Fixed);
    }

    if (_brokenGameObject != null)
    {
      _brokenGameObject.SetActive(newState == ERepairState.Broken);
    }

    switch (newState)
    {
      case ERepairState.Fixed:
        GameStateManager.Instance.ShipHealth.OnDeviceBecameFixed();
        if (_fixedSound)
          AudioManager.Instance.PlaySound(gameObject, _fixedSound);
        break;
      case ERepairState.Broken:
        GameStateManager.Instance.ShipHealth.OnDeviceBecameDamaged();
        if (_brokenSound)
          AudioManager.Instance.PlaySound(gameObject, _brokenSound);
        break;
    }
  }
}
