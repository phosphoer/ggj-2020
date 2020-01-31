using UnityEngine;
using System.Collections;

public abstract class ActionBase : MonoBehaviour
{
  public bool CancelRequested => _cancelRequested;

  protected Coroutine _actionRoutine;
  protected bool _cancelRequested;

  public Coroutine StartAction()
  {
    Debug.LogFormat("Starting action {0} in {1}", name, transform.parent.name);

    _cancelRequested = false;
    _actionRoutine = StartCoroutine(DoActionAsync());
    return _actionRoutine;
  }

  public IEnumerator StartActionManual()
  {
    Debug.LogFormat("Starting manual action {0} in {1}", name, transform.parent.name);

    _cancelRequested = false;
    return DoActionAsync();
  }

  public void CancelAction()
  {
    _cancelRequested = true;
  }

  public virtual void ResetActionState()
  {

  }

  protected abstract IEnumerator DoActionAsync();
}