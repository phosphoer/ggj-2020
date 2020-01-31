using UnityEngine;

public class MeshDisableCulling : MonoBehaviour
{
  private void Start()
  {
    MeshFilter filter = GetComponent<MeshFilter>();
    if (filter != null)
    {
      filter.mesh.bounds = new Bounds(-Vector3.one * Mathf.Infinity, Vector3.one * Mathf.Infinity);
    }
  }
}