using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryComponent : MonoBehaviour
{
    [SerializeField]
    private GameObject _chargeEffect= null;

    private float _chargeAmount= 0;
    
    public bool HasCharge
    {
      get { return _chargeAmount > 0; }
    }

    public void AddCharge(float Charge)
    {
      _chargeAmount+= Charge;
      OnChargeUpdated();
    }

    public float DrainCharge()
    {
      float charge= _chargeAmount;
      _chargeAmount= 0;
      OnChargeUpdated();

      return charge;
    }

    void OnChargeUpdated()
    { 
      if (_chargeEffect != null)
      {
        _chargeEffect.SetActive(HasCharge);
      }
    }
}
