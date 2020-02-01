using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomInhabitantComponent : MonoBehaviour
{
    private RoomComponent _roomComponent;
    private InteratibleDeviceComponent _currentDevice;
    private Rigidbody _rigidBody;
    public bool IsPressingInteraction
    {
      get { return _currentDevice != null && _currentDevice.IsInteractionPressed; }
    }

    // Start is called before the first frame update
    void Start()
    {
      _rigidBody= GetComponent<Rigidbody>();
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
        if (_currentDevice.IsInteractionPressed)
        {
          _currentDevice.OnInteractionReleased();
        }
        _currentDevice = null;
      }
    }

    public void StartInteraction()
    {
      if (_currentDevice != null)
      {
        _currentDevice.OnInteractionPressed();
      }
    }

    public void StopInteraction()
    {
      if (_currentDevice != null)
      {
        _currentDevice.OnInteractionReleased();
      }
    }
}
