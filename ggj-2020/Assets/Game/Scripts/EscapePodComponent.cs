using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscapePodComponent : MonoBehaviour
{
  [SerializeField]
  private Animation _escapePodAnimation;

  private bool _isEscapePodEntered = false;

  public float EscapeDuration = 3.0f;
  private float _escapeTimer = 0.0f;
  public bool HasEscapeDurationElasped
  {
    get { return _escapeTimer >= EscapeDuration; }
  }

  private void Update()
  {
    if (_isEscapePodEntered && !HasEscapeDurationElasped)
    {
      _escapeTimer = Mathf.Min(_escapeTimer + Time.deltaTime, EscapeDuration);
    }
  }

  private void OnTriggerEnter(Collider other)
  {
    var inhabitant = other.gameObject.GetComponentInParent<RoomInhabitantComponent>();
    if (inhabitant != null && inhabitant.CanUseEscapePod)
    {
      if (!_isEscapePodEntered)
      {
        _isEscapePodEntered = true;

        if (_escapePodAnimation != null)
        {
          _escapePodAnimation.Play();
        }
      }

      // Attach the inhabitant to the escape pod
      inhabitant.gameObject.transform.SetParent(this.transform);
    }
  }
}
