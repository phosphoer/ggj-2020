using UnityEngine;

public class PlayerAstronautController : MonoBehaviour
{
  private Rewired.Player _rewiredPlayer;
  private RoomInhabitantComponent _roomInhabitant;

  private void Start()
  {
    _roomInhabitant= GetComponent<RoomInhabitantComponent>();
  }

  private void Update()
  {
    float moveHorizontal = _rewiredPlayer.GetAxis(RewiredConsts.Action.MoveHorizontal);
    float moveVertical = _rewiredPlayer.GetAxis(RewiredConsts.Action.MoveVertical);

    UpdateInteraction();
  }

  private void UpdateInteraction()
  {
    bool isInteractionPressed= _rewiredPlayer.GetButton(RewiredConsts.Action.Interact);

    if (_roomInhabitant)
    {
      if (isInteractionPressed && !_roomInhabitant.IsUsingInteraction)
      { 
        _roomInhabitant.StartInteraction();
      }
      else if (!isInteractionPressed && _roomInhabitant.IsUsingInteraction)
      { 
        _roomInhabitant.StopInteraction();
      }
    }
  }
}