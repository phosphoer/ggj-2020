using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FPSCounter : MonoBehaviour
{
  [SerializeField]
  private Text _fpsTextUI = null;

  [SerializeField]
  private GameObject _waterMark = null;

  private float _frameTimer;
  private int _frameCounter;

  private void Awake()
  {
    _fpsTextUI.enabled = false;
    _waterMark.SetActive(false);
  }

  private void Update()
  {
    _frameTimer += Time.unscaledDeltaTime;
    _frameCounter += 1;

    if (_frameTimer >= 1.0f)
    {
      _fpsTextUI.text = _frameCounter.ToString();
      _frameTimer = 0;
      _frameCounter = 0;
    }

    if (Input.GetKeyDown(KeyCode.F1))
    {
      _fpsTextUI.enabled = !_fpsTextUI.enabled;
    }

    if (Input.GetKeyDown(KeyCode.F2))
    {
      _waterMark.SetActive(!_waterMark.activeSelf);
    }
  }
}