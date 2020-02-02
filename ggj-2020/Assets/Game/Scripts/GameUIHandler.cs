using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUIHandler : MonoBehaviour
{
  [SerializeField]
  private RectTransform _healthBarRectTransform = null;

  [SerializeField]
  private Image _TimerImage = null;

  // Update is called once per frame
  void Update()
  {
    RefreshHealthBar();
    RefreshTimer();
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
