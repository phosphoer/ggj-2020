using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscapePodComponent : MonoBehaviour
{
  private bool _isEscapePodEntered;
  public bool IsEscapePodEntered
  {
    get { return _isEscapePodEntered; }
  }

  private void OnTriggerEnter(Collider other)
  {
    var inhabitant = other.gameObject.GetComponentInParent<RoomInhabitantComponent>();
    if (inhabitant != null)
    {
      _isEscapePodEntered= true;
    }
  }
}
