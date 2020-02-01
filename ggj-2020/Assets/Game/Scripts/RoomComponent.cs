using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomComponent : MonoBehaviour
{
    private List<RoomInhabitantComponent> RoomInhabitants;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
      RoomInhabitantComponent inhabitant= other.gameObject.GetComponent<RoomInhabitantComponent>();
      if (inhabitant != null)
      {
        RoomInhabitants.Add(inhabitant);
        inhabitant.OnRoomEntered(this);
      }
    }

    private void OnTriggerExit(Collider other)
    {
      RoomInhabitantComponent inhabitant= other.gameObject.GetComponent<RoomInhabitantComponent>();
      if (inhabitant != null)
      {
        inhabitant.OnRoomExited(this);
        RoomInhabitants.Remove(inhabitant);
      }
    }
}
