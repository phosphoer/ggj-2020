using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteratibleDeviceComponent : MonoBehaviour
{
    private RoomInhabitantComponent CurrentInhabitant;
    
    private bool _isInteractionPressed;
    public bool IsInteractionPressed { 
      get { return _isInteractionPressed; }
      set { _isInteractionPressed= value; }
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
      if (CurrentInhabitant == null)
      {
        CurrentInhabitant= other.gameObject.GetComponent<RoomInhabitantComponent>();
        this.OnInteractionEntered(CurrentInhabitant);
        CurrentInhabitant.OnInteractionEntered(this);
      }
    }

    private void OnTriggerExit(Collider other)
    {
      if (CurrentInhabitant != null && other.gameObject.GetComponent<RoomInhabitantComponent>() == CurrentInhabitant)
      {
        this.OnInteractionExited(CurrentInhabitant);
        CurrentInhabitant.OnInteractionExited(this);
        CurrentInhabitant = null;
      }
    }

    protected virtual void OnInteractionEntered(RoomInhabitantComponent Inhabitant)
    {
      CurrentInhabitant = Inhabitant;
    }

    protected virtual void OnInteractionExited(RoomInhabitantComponent Inhabitant)
    {
      if (CurrentInhabitant == Inhabitant)
      {
        CurrentInhabitant = null;
      }
    }
    public virtual void OnInteractionPressed()
    {
      IsInteractionPressed= true;
    }
    public virtual void OnInteractionReleased()
    {
      IsInteractionPressed= false;
    }
}
