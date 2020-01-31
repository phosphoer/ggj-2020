using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomInhabitantComponent : MonoBehaviour
{
    InteratibleDeviceComponent CurrentDevice;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public virtual void OnInteractionEntered(InteratibleDeviceComponent Device)
    {
      CurrentDevice = Device;
    }

    public virtual void OnInteractionExited(InteratibleDeviceComponent Device)
    {
      if (CurrentDevice == Device)
      {
        if (CurrentDevice.IsInteractionActive)
        {
          CurrentDevice.OnInteractionStopped();
        }
        CurrentDevice = null;
      }
    }

    public void StartInteraction()
    {
      if (CurrentDevice != null)
      {
        CurrentDevice.OnInteractionStarted();
      }
    }
}
