using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomInhabitantComponent : MonoBehaviour
{
    private RoomComponent _roomComponent;
    private InteratibleDeviceComponent _currentDevice;
    public bool IsUsingInteraction
    {
      get { return _currentDevice != null && _currentDevice.IsInteractionActive; }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
  
    public virtual void OnRoomEntered(RoomComponent room)
    {
      _roomComponent= room;
    }
    public virtual void OnRoomExited(RoomComponent room)
    {
      if (room == _roomComponent)
      {
        _roomComponent= null;
      }
    }

    public virtual void OnInteractionEntered(InteratibleDeviceComponent Device)
    {
      _currentDevice = Device;
    }

    public virtual void OnInteractionExited(InteratibleDeviceComponent Device)
    {
      if (_currentDevice == Device)
      {
        if (_currentDevice.IsInteractionActive)
        {
          _currentDevice.OnInteractionStopped();
        }
        _currentDevice = null;
      }
    }

    public void StartInteraction()
    {
      if (_currentDevice != null)
      {
        _currentDevice.OnInteractionStarted();
      }
    }

    public void StopInteraction()
    {
      if (_currentDevice != null)
      {
        _currentDevice.OnInteractionStopped();
      }
    }
}
