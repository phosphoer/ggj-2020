using UnityEngine;

public class ParticleSystemSounds : MonoBehaviour
{
  public SoundBank StartSound;

  [SerializeField]
  private ParticleSystem _particleSystem = null;

  private bool _isPlaying;

  private void Update()
  {
    if (_particleSystem == null || AudioManager.Instance == null)
      return;

    if (_particleSystem.isPlaying && !_isPlaying)
    {
      _isPlaying = true;
      AudioManager.Instance.PlaySound(gameObject, StartSound);
    }
    else if (!_particleSystem.isPlaying && _isPlaying)
    {
      _isPlaying = false;
    }
  }
}