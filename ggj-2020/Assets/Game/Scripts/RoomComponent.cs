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

    CleanUpInhabitants();
  }

  private void OnTriggerExit(Collider other)
  {
    RoomInhabitantComponent inhabitant = other.GetComponentInParent<RoomInhabitantComponent>();
    if (inhabitant != null)
    {
      inhabitant.OnRoomExited(this);
      _roomInhabitants.Remove(inhabitant);
    }

    CleanUpInhabitants();
  }

  private void CleanUpInhabitants()
  {
    for (int i = _roomInhabitants.Count - 1; i >= 0; --i)
    {
      if (_roomInhabitants[i] == null)
      {
        _roomInhabitants.RemoveAt(i);
      }
    }
  }
}
