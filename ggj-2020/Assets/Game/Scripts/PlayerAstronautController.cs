using UnityEngine;

public class PlayerAstronautController : MonoBehaviour
{
  public Rewired.Player RewiredPlayer
  {
    get { return _rewiredPlayer; }
    set { _rewiredPlayer = value; }
  }

  public AstronautController Astronaut
  {
    get { return _astronaut; }
    set
    {
      _astronaut = value;
    }
  }

  [SerializeField]
  private AstronautController _astronautPrefab = null;

  private AstronautController _astronaut;
  private Rewired.Player _rewiredPlayer;

  private void Awake()
  {
    Spawn();
  }

  private void Update()
  {
    if (!Rewired.ReInput.isReady || _astronaut == null || _rewiredPlayer == null)
    {
      return;
    }

    float moveHorizontal = _rewiredPlayer.GetAxis(RewiredConsts.Action.MoveHorizontal);
    float moveVertical = _rewiredPlayer.GetAxis(RewiredConsts.Action.MoveVertical);
    Vector3 moveVector = new Vector3(moveHorizontal, 0, moveVertical);
    _astronaut.MoveVector = moveVector;

    UpdateInteraction();
  }

  private void UpdateInteraction()
  {
    bool isInteractionPressed = _rewiredPlayer.GetButtonDown(RewiredConsts.Action.Interact);

    if (_astronaut != null)
    {
      if (isInteractionPressed && !_astronaut.IsPressingInteraction())
      {
        _astronaut.PressInteraction();
      }
      else if (!isInteractionPressed && _astronaut.IsPressingInteraction())
      {
        _astronaut.ReleaseInteraction();
      }
    }
  }

  private void Spawn()
  {
    _astronaut = Instantiate(_astronautPrefab, transform);
    _astronaut.Died += OnDied;
    _astronaut.Despawned += OnDespawned;
  }

  private void OnDied()
  {
    Destroy(_astronaut.GetComponent<CameraFocusPoint>());
  }

  private void OnDespawned()
  {
    Spawn();
  }
}