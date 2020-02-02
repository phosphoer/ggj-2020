using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteratibleDeviceComponent : MonoBehaviour
{
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

  public virtual void OnInteractionPressed(GameObject gameObject)
  {
  }

  public virtual bool DrainsBatteryOnInteraction()
  {
    return true;
  }
}
