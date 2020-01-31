using UnityEngine;

public class UnparentOnStart : MonoBehaviour
{
  private void Start()
  {
    transform.parent = null;
  }
}