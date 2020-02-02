using UnityEngine;

public class RandomColor : MonoBehaviour
{
  [SerializeField]
  private Color[] _colors = null;

  [SerializeField]
  private MeshRenderer _renderer = null;

  private Material _matInstance;

  private void Start()
  {
    _matInstance = Instantiate(_renderer.sharedMaterial);
    _renderer.sharedMaterial = _matInstance;
    _matInstance.color = _colors[Random.Range(0, _colors.Length)];
  }

  private void OnDestroy()
  {
    Destroy(_matInstance);
  }
}