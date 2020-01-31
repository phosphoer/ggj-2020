using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class CanvasCursor : MonoBehaviour
{
  public static bool IsEnabled;
  public static bool IsVisible { get; private set; }

  [SerializeField]
  private float _autoHideTime = 10.0f;

  private Canvas _canvas;
  private RectTransform _canvasRect;
  private RectTransform _cursorRect;
  private Image _cursorImage;

  private float _targetScale = 1;
  private float _autoHideTimer;
  private bool _autoHidden;

  private void Awake()
  {
    _canvas = GetComponentInParent<Canvas>();
    _canvasRect = _canvas.GetComponent<RectTransform>();
    _cursorRect = GetComponent<RectTransform>();
    _cursorImage = GetComponent<Image>();

    Cursor.visible = false;
    _autoHideTimer = _autoHideTime;
  }

  private void Update()
  {
    IsVisible = IsEnabled && !_autoHidden;
    _cursorImage.enabled = IsVisible;
    Cursor.visible = false;

    // Convert real cursor coords to canvas 
    Vector3 cursorNormalized = Input.mousePosition;
    cursorNormalized.x /= Screen.width;
    cursorNormalized.y /= Screen.height;
    cursorNormalized.z = 0;

    Vector3 cursorPos = cursorNormalized;
    cursorPos.x *= _canvasRect.rect.width;
    cursorPos.y *= _canvasRect.rect.height;

    // Auto hide when mouse doesn't move 
    _autoHidden = _autoHideTimer >= _autoHideTime;
    if (Mathf.Approximately(_cursorRect.anchoredPosition.x, cursorPos.x) &&
        Mathf.Approximately(_cursorRect.anchoredPosition.x, cursorPos.x))
    {
      _autoHideTimer += Time.unscaledDeltaTime;
    }
    else
    {
      _autoHideTimer = 0;
    }

    // Position cursor icon
    _cursorRect.anchoredPosition = cursorPos;

    // Click animation
    if (Input.GetKey(KeyCode.Mouse0))
    {
      _targetScale = 1.5f;
    }
    else
    {
      _targetScale = 1;
    }

    _cursorRect.localScale = Mathfx.Damp(_cursorRect.localScale, Vector3.one * _targetScale, 0.5f, Time.unscaledDeltaTime * 10.0f);
  }
}