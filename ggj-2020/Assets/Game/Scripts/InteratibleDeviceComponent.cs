using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteratibleDeviceComponent : MonoBehaviour
{
    private RoomInhabitantComponent CurrentInhabitant;
    
    private bool _isInteractionActive;
    public bool IsInteractionActive { 
      get { return _isInteractionActive; }
      set { _isInteractionActive= value; }
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
    public virtual void OnInteractionStarted()
    {
      IsInteractionActive= true;
    }
    public virtual void OnInteractionStopped()
    {
      IsInteractionActive= false;
    }

    void Update()
    {
        
    }
}
