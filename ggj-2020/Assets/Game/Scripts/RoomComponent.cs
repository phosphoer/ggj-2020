using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomComponent : MonoBehaviour
{
  public IReadOnlyList<RoomInhabitantComponent> RoomInhabitants => _roomInhabitants;

  private List<RoomInhabitantComponent> _roomInhabitants = new List<RoomInhabitantComponent>();

  private void OnTriggerEnter(Collider other)
  {
    RoomInhabitantComponent inhabitant = other.GetComponentInParent<RoomInhabitantComponent>();
    if (inhabitant != null)
    {
      _roomInhabitants.Add(inhabitant);
      inhabitant.OnRoomEntered(this);
    }
  }

  private void OnTriggerExit(Collider other)
  {
    RoomInhabitantComponent inhabitant = other.GetComponentInParent<RoomInhabitantComponent>();
    if (inhabitant != null)
    {
      inhabitant.OnRoomExited(this);
      _roomInhabitants.Remove(inhabitant);
    }
  }
}
