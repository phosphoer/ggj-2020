using UnityEngine;
using System.Collections.Generic;

public class CollisionSafeAnchor : MonoBehaviour
{
  public float AllowedRadius = 20;
  public float CollisionRadius = 10;
  public bool CollideWithOtherInstances = true;
  public LayerMask CollisionLayers;

  private Vector3 _startPos;
  private Vector3 _correctionVector;
  private Collider[] _collisionResults = new Collider[8];
  private List<Transform> _ignoredTransforms = new List<Transform>();

  private static List<CollisionSafeAnchor> _instances = new List<CollisionSafeAnchor>();

  private const int kPlaceAttemptCount = 100;

  public void AddIgnoredTransform(Transform t)
  {
    _ignoredTransforms.Add(t);
  }

  public void RemoveIgnoredTransform(Transform t)
  {
    _ignoredTransforms.Remove(t);
  }

  public void ResetStartPos()
  {
    _startPos = transform.position;
  }

  public bool TryMoveToValidPosition()
  {
    for (int attemptIndex = 0; attemptIndex < 100; ++attemptIndex)
    {
      float t = (attemptIndex / (float)(kPlaceAttemptCount - 1));
      float currentRadius = t * AllowedRadius;
      float angle = t * 360 * 3;
      Vector3 offset = Quaternion.Euler(0, angle, 0) * Vector3.forward;
      Vector3 attemptPos = _startPos + offset * currentRadius;

      // Test collision at this position
      bool isAttemptValid = true;
      int count = Physics.OverlapSphereNonAlloc(attemptPos, CollisionRadius, _collisionResults, CollisionLayers, QueryTriggerInteraction.Ignore);
      if (count > 0)
      {
        for (int j = 0; j < count; ++j)
        {
          if (!IsTransformIgnored(_collisionResults[j].transform))
          {
            isAttemptValid = false;
            break;
          }
        }
      }

      // Test 'collision' with other instances
      for (int i = 0; isAttemptValid && CollideWithOtherInstances && i < _instances.Count; ++i)
      {
        CollisionSafeAnchor other = _instances[i];
        if (other != this)
        {
          Vector3 toAnchor = other.transform.position - transform.position;
          float dist = toAnchor.magnitude;
          if (dist < CollisionRadius + other.CollisionRadius)
          {
            isAttemptValid = false;
            continue;
          }
        }
      }

      if (isAttemptValid)
      {
        transform.position = attemptPos;
        return true;
      }
    }

    return false;
  }

  private void Start()
  {
    _startPos = transform.position;
  }

  private void OnEnable()
  {
    _instances.Add(this);
  }

  private void OnDisable()
  {
    _instances.Remove(this);
  }

  private void OnDrawGizmosSelected()
  {
    Gizmos.color = Color.green;
    Gizmos.DrawWireSphere(transform.position, CollisionRadius);
    Gizmos.color = Color.white;
    Gizmos.DrawWireSphere(transform.position, AllowedRadius);
  }

  private bool IsTransformIgnored(Transform child)
  {
    foreach (Transform t in _ignoredTransforms)
    {
      if (child.IsChildOf(t))
      {
        return true;
      }
    }

    return false;
  }
}