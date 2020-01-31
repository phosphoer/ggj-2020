using UnityEngine;

public class RandomMaterial : MonoBehaviour
{
  [SerializeField]
  private Material[] _materials = null;

  [SerializeField]
  private Renderer _renderer = null;

  private void Start()
  {
    if (_renderer != null && _materials != null && _materials.Length > 0)
    {
      _renderer.sharedMaterial = _materials[Random.Range(0, _materials.Length)];
    }
  }
}