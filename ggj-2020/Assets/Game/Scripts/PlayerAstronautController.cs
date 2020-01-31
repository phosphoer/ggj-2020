using UnityEngine;

public class PlayerAstronautController : MonoBehaviour
{
  private Rewired.Player _rewiredPlayer;

  private void Update()
  {
    float moveHorizontal = _rewiredPlayer.GetAxis(RewiredConsts.Action.MoveHorizontal);
    float moveVertical = _rewiredPlayer.GetAxis(RewiredConsts.Action.MoveVertical);
  }
}