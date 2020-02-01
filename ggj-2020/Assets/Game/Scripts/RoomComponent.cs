using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomComponent : MonoBehaviour
{
    private List<RoomInhabitantComponent> _roomInhabitants = new List<RoomInhabitantComponent>();
    public List<RoomInhabitantComponent> RoomInhabitants
    {
      get { return _roomInhabitants; }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
      RoomInhabitantComponent inhabitant= other.gameObject.GetComponentInParent<RoomInhabitantComponent>();
      if (inhabitant != null)
      {
        _roomInhabitants.Add(inhabitant);
        inhabitant.OnRoomEntered(this);
      }
    }

    private void OnTriggerExit(Collider other)
    {
      RoomInhabitantComponent inhabitant= other.gameObject.GetComponentInParent<RoomInhabitantComponent>();
      if (inhabitant != null)
      {
        inhabitant.OnRoomExited(this);
        _roomInhabitants.Remove(inhabitant);
      }
    }
}
