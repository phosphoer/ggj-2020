using UnityEngine;

public class DestroyEvent : MonoBehaviour
{
  public event System.Action Destroyed;

  private void OnDestroy()
  {
    if (Destroyed != null)
      Destroyed();
  }
}