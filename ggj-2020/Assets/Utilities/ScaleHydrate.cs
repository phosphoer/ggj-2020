using UnityEngine;
using System.Collections;

public class ScaleHydrate : MonoBehaviour
{
  public event System.Action Dehydrated;

  public bool HydrateOnStart = true;
  public bool StartDehydrated = false;
  public float HydrateTime = 1.0f;
  public float DehydrateTime = 1.0f;
  public bool UseUnscaledTime = true;
  public AnimationCurve ScaleInCurve = AnimationCurve.EaseInOut(0.0f, 0.0f, 1.0f, 1.0f);
  public AnimationCurve ScaleOutCurve = AnimationCurve.EaseInOut(0.0f, 1.0f, 1.0f, 0.0f);

  private Coroutine _currentRoutine;
  private Vector3 _startScale;

  public Coroutine Hydrate()
  {
    if (_currentRoutine != null)
    {
      StopCoroutine(_currentRoutine);
      _currentRoutine = null;
    }

    gameObject.SetActive(true);
    _currentRoutine = StartCoroutine(HydrateRoutine());
    return _currentRoutine;
  }

  public Coroutine Dehydrate(bool destroyOnFinish = true)
  {
    if (_currentRoutine != null)
    {
      StopCoroutine(_currentRoutine);
      _currentRoutine = null;
    }

    gameObject.SetActive(true);
    _currentRoutine = StartCoroutine(DehydrateRoutine(destroyOnFinish));
    return _currentRoutine;
  }

  private IEnumerator HydrateRoutine()
  {
    Vector3 startScale = _startScale * ScaleInCurve.Evaluate(0);
    Vector3 endScale = _startScale * ScaleInCurve.Evaluate(1);
    for (float time = 0; time < HydrateTime; time += UseUnscaledTime ? Time.unscaledDeltaTime : Time.deltaTime)
    {
      float t = time / HydrateTime;
      float tCurve = ScaleInCurve.Evaluate(t);
      transform.localScale = Vector3.LerpUnclamped(startScale, endScale, tCurve);
      yield return null;
    }

    transform.localScale = endScale;
  }

  private IEnumerator DehydrateRoutine(bool destroyOnFinish)
  {
    Vector3 startScale = _startScale * ScaleOutCurve.Evaluate(1);
    Vector3 endScale = _startScale * ScaleOutCurve.Evaluate(0);
    for (float time = 0; time < HydrateTime; time += UseUnscaledTime ? Time.unscaledDeltaTime : Time.deltaTime)
    {
      float t = time / HydrateTime;
      transform.localScale = Vector3.LerpUnclamped(startScale, endScale, ScaleOutCurve.Evaluate(t));
      yield return null;
    }

    transform.localScale = startScale;

    if (destroyOnFinish)
    {
      Destroy(gameObject);
    }
    else
    {
      gameObject.SetActive(false);
    }

    Dehydrated?.Invoke();
  }

  private void Awake()
  {
    _startScale = transform.localScale;
  }

  private void OnDisable()
  {
    transform.localScale = _startScale;
  }

  private void Start()
  {
    if (HydrateOnStart)
    {
      Hydrate();
    }
    else if (StartDehydrated)
    {
      transform.localScale = _startScale * ScaleOutCurve.Evaluate(0);
      gameObject.SetActive(false);
    }
  }
}