using UnityEngine;

public class PlaySoundWhileEnabled : MonoBehaviour
{
  public SoundBank SoundBank;
  private bool _queued;

  private void OnEnable()
  {
    if (!_queued)
    {
      _queued = true;
      StartCoroutine(AudioManager.QueuePlaySoundRoutine(gameObject, SoundBank));
    }
  }

  private void OnDisable()
  {
    _queued = false;

    if (AudioManager.Instance != null)
    {
      AudioManager.Instance.StopSound(gameObject, SoundBank);
    }
  }
}