using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : Singleton<GameStateManager>
{
  public enum GameStage
  {
    Invalid,
    MainMenu,
    Game,
    WinGame,
    LoseGame,
  }

  public GameStage DefaultStage;
  public GameObject MainMenuUIPrefab;
  public GameObject GameUIPrefab;
  public GameObject WinGameUIPrefab;
  public GameObject LoseGameUIPrefab;
  public SoundBank MusicMenuLoop;
  public CameraControllerBase MenuCamera;
  public CameraControllerGame GameCamera;

  private GameStage _gameStage = GameStage.Invalid;
  private GameObject _mainMenuUI = null;
  private GameObject _gameUI = null;
  private GameObject _winGameUI = null;
  private GameObject _loseGameUI = null;

  [SerializeField]
  private ShipHealthComponent _shipHealth = null;
  public ShipHealthComponent ShipHealth
  {
    get { return _shipHealth; }
  }

  [SerializeField]
  private EnergySinkController _energySink = null;
  public EnergySinkController EnergySink
  {
    get { return _energySink; }
  }

  private void Awake()
  {
    GameStateManager.Instance = this;
  }

  private void Start()
  {
    // Base camera controller
    CameraControllerStack.Instance.PushController(MenuCamera);

    SetGameStage(DefaultStage);
  }

  private void Update()
  {
    GameStage nextGameStage = _gameStage;

    switch (_gameStage)
    {
      case GameStage.MainMenu:
        break;
      case GameStage.Game:
        if (!_shipHealth.IsShipAlive)
        {
          nextGameStage= GameStage.LoseGame;
        }
        else if (_energySink.IsFull)
        {
          nextGameStage= GameStage.WinGame;
        }
        break;
      case GameStage.WinGame:
        break;
      case GameStage.LoseGame:
        break;
    }

    SetGameStage(nextGameStage);
  }

  public void SetGameStage(GameStage newGameStage)
  {
    if (newGameStage != _gameStage)
    {
      OnExitStage(_gameStage);
      OnEnterStage(newGameStage);
      _gameStage = newGameStage;
    }
  }

  public void OnExitStage(GameStage oldGameStage)
  {
    switch (oldGameStage)
    {
      case GameStage.MainMenu:
        {
          if (MusicMenuLoop != null)
          {
            AudioManager.Instance.FadeOutSound(gameObject, MusicMenuLoop, 3.0f);
          }

          Destroy(_mainMenuUI);
          _mainMenuUI = null;
        }
        break;
      case GameStage.Game:
        {
          _shipHealth.OnCompletedGame();

          CameraControllerStack.Instance.PopController(GameCamera);

          Destroy(_gameUI);
          _gameUI = null;
        }
        break;
      case GameStage.WinGame:
        {
          Destroy(_winGameUI);
          _winGameUI = null;
        }
        break;
      case GameStage.LoseGame:
        {
          Destroy(_loseGameUI);
          _loseGameUI = null;
        }
        break;
    }
  }

  public void OnEnterStage(GameStage newGameStage)
  {
    switch (newGameStage)
    {
      case GameStage.MainMenu:
        {
          _mainMenuUI = (GameObject)Instantiate(MainMenuUIPrefab, Vector3.zero, Quaternion.identity);

          if (MusicMenuLoop != null)
          {
            AudioManager.Instance.FadeInSound(gameObject, MusicMenuLoop, 3.0f);
          }
        }
        break;
      case GameStage.Game:
        {
          _gameUI = (GameObject)Instantiate(GameUIPrefab, Vector3.zero, Quaternion.identity);
          _shipHealth.OnStartedGame();

          CameraControllerStack.Instance.PushController(GameCamera);
        }
        break;
      case GameStage.WinGame:
        {
          _winGameUI = (GameObject)Instantiate(WinGameUIPrefab, Vector3.zero, Quaternion.identity);
        }
        break;
      case GameStage.LoseGame:
        {
          _loseGameUI = (GameObject)Instantiate(LoseGameUIPrefab, Vector3.zero, Quaternion.identity);
        }
        break;
    }
  }
}
