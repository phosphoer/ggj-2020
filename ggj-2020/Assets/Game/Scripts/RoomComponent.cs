using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomComponent : MonoBehaviour
{
    private List<RoomInhabitantComponent> _roomInhabitants = new List<RoomInhabitantComponent>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
      RoomInhabitantComponent inhabitant= other.gameObject.GetComponent<RoomInhabitantComponent>();
      if (inhabitant != null)
      {
        _roomInhabitants.Add(inhabitant);
        inhabitant.OnRoomEntered(this);
      }
    }

    private void OnTriggerExit(Collider other)
    {
      RoomInhabitantComponent inhabitant= other.gameObject.GetComponent<RoomInhabitantComponent>();
      if (inhabitant != null)
      {
        inhabitant.OnRoomExited(this);
        _roomInhabitants.Remove(inhabitant);
      }
    }
}
