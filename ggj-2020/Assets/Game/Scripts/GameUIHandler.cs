using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public struct TutorialLine
{
  public SoundBank TutorialAudio;
  public string TutorialText;
  public float Duration;
}

public class GameUIHandler : MonoBehaviour
{
  public List<TutorialLine> TutorialLines;
  private int _tutorialLineIndex=0;
  private float _tutorialLineTimer=0;

  [SerializeField]
  private RectTransform _healthBarRectTransform = null;

  [SerializeField]
  private Image _TimerImage = null;

  [SerializeField]
  private Text _tutorialTextField = null;

  private void Start()
  {
    FireCurrentTutorialLine();
  }

  void Update()
  {
    UpdateTutorialLineTimer();
    RefreshHealthBar();
    RefreshTimer();
  }

  void UpdateTutorialLineTimer()
  {
    if (_tutorialLineIndex < TutorialLines.Count)
    {
      TutorialLine tutorialLine= TutorialLines[_tutorialLineIndex];
    
      if (_tutorialLineTimer >= tutorialLine.Duration)
      {
        _tutorialLineTimer= 0;
        _tutorialLineIndex++;
        FireCurrentTutorialLine();
      }
      else
      {
        _tutorialLineTimer+= Time.deltaTime;
      }
    }
  }

  void FireCurrentTutorialLine()
  {
    if (_tutorialLineIndex < TutorialLines.Count)
    {
      TutorialLine tutorialLine= TutorialLines[_tutorialLineIndex];

      if (tutorialLine.TutorialAudio != null)
      {
        AudioManager.Instance.PlaySound(tutorialLine.TutorialAudio);
      }

      if (_tutorialTextField != null)
      {
        _tutorialTextField.text= tutorialLine.TutorialText;
      }
    }
    else
    {
      if (_tutorialTextField != null)
      {
        _tutorialTextField.text= "";
      }
    }
  }

  void RefreshHealthBar()
  {
    _healthBarRectTransform.transform.localScale= new Vector3(GameStateManager.Instance.ShipHealth.ShipHealthFraction, 1, 1);
  }

  void RefreshTimer()
  {
    _TimerImage.fillAmount= GameStateManager.Instance.EnergySink.OpenedFractionOfMax;
  }
}
