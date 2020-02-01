using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteratibleDeviceComponent : MonoBehaviour
{
  private RoomInhabitantComponent CurrentInhabitant;

  private bool _isInteractionPressed;
  public bool IsInteractionPressed
  {
    get { return _isInteractionPressed; }
  }

  // Start is called before the first frame update
  public virtual void Start()
  {
    _isInteractionPressed = false;
  }

  private void OnTriggerEnter(Collider other)
  {
    if (CurrentInhabitant == null)
    {
      CurrentInhabitant = other.gameObject.GetComponentInParent<RoomInhabitantComponent>();

      if (CurrentInhabitant != null)
      {
        this.OnInteractionEntered(CurrentInhabitant);
        CurrentInhabitant.OnInteractionEntered(this);
      }
    }
  }

  private void OnTriggerExit(Collider other)
  {
    if (CurrentInhabitant != null)
    {
      RoomInhabitantComponent Inhabitant = other.gameObject.GetComponentInParent<RoomInhabitantComponent>();

      if (Inhabitant != null && Inhabitant == CurrentInhabitant)
      {
        CurrentInhabitant.OnInteractionExited(this);
        this.OnInteractionExited(CurrentInhabitant);
      }
    }
  }

  protected virtual void OnInteractionEntered(RoomInhabitantComponent Inhabitant)
  {
    CurrentInhabitant = Inhabitant;
  }

  protected virtual void OnInteractionExited(RoomInhabitantComponent Inhabitant)
  {
    if (CurrentInhabitant == Inhabitant)
    {
      CurrentInhabitant = null;
    }
  }
  public virtual void OnInteractionPressed()
  {
    _isInteractionPressed = true;
  }
  public virtual void OnInteractionReleased()
  {
    _isInteractionPressed = false;
  }
}
