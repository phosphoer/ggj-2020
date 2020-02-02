using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteratibleDeviceComponent : MonoBehaviour
{
  public bool InteractionEnabled = true;

  private void OnTriggerEnter(Collider other)
  {
    var inhabitant = other.gameObject.GetComponentInParent<RoomInhabitantComponent>();
    if (inhabitant != null)
    {
      inhabitant.OnInteractionEntered(this);
    }
  }

  private void OnTriggerExit(Collider other)
  {
    var inhabitant = other.gameObject.GetComponentInParent<RoomInhabitantComponent>();
    if (inhabitant != null)
    {
      inhabitant.OnInteractionExited(this);
    }
  }

  public void TriggerInteraction(GameObject gameObject)
  {
    if (InteractionEnabled)
      OnInteractionPressed(gameObject);
  }

  protected virtual void OnInteractionPressed(GameObject gameObject)
  {
  }

  public virtual bool DrainsBatteryOnInteraction()
  {
    return true;
  }
}
