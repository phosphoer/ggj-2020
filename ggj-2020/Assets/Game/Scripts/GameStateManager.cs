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
    PostGame
  }

  public GameStage DefaultStage;
  public GameObject MainMenuPrefab;
  public SoundBank MusicMenuLoop;
  public CameraControllerBase MenuCamera;
  public CameraControllerGame GameCamera;

  private GameStage _gameStage = GameStage.Invalid;
  private GameObject _mainMenu = null;

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
        break;
      case GameStage.PostGame:
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

          Destroy(_mainMenu);
          _mainMenu = null;
        }
        break;
      case GameStage.Game:
        {
          CameraControllerStack.Instance.PopController(GameCamera);
        }
        break;
      case GameStage.PostGame:
        {
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
          _mainMenu = (GameObject)Instantiate(MainMenuPrefab, Vector3.zero, Quaternion.identity);

          if (MusicMenuLoop != null)
          {
            AudioManager.Instance.FadeInSound(gameObject, MusicMenuLoop, 3.0f);
          }
        }
        break;
      case GameStage.Game:
        {
          CameraControllerStack.Instance.PushController(GameCamera);
        }
        break;
      case GameStage.PostGame:
        {
        }
        break;
    }
  }
}
