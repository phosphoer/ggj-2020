using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MainMenuUIHandler : MonoBehaviour
{
  
  public GameObject TutorialObj;
  public float TutorialAnimSpeed = 1f;
  Animator tutAnim;
  
  public List <GameObject> MainMenuObjs;
  public List <GameObject> DifficultyObjs;
  EventSystem eventSystem;
    
  void OnEnable()
  {
    eventSystem = EventSystem.current;
  }

  public void OnNewGameClicked()
  {
	  foreach (GameObject x in MainMenuObjs)
	  {
		x.SetActive(!x.activeSelf);
	  }
	  foreach (GameObject x in DifficultyObjs)
	  {
		x.SetActive(!x.activeSelf);
	  }
	  eventSystem.SetSelectedGameObject(DifficultyObjs[0]);

  }
    
  public void OnEasyClicked()
  {
    GameStateManager.Instance.SetDifficulty("easy");
	GameStateManager.Instance.SetGameStage(GameStateManager.GameStage.Game);
  }
  public void OnMedClicked()
  {
    GameStateManager.Instance.SetGameStage(GameStateManager.GameStage.Game);
	GameStateManager.Instance.SetDifficulty("med");
  }
  public void OnHardClicked()
  {
    GameStateManager.Instance.SetGameStage(GameStateManager.GameStage.Game);
	GameStateManager.Instance.SetDifficulty("hard");
  }

  public void OnInstructionsClicked()
  {
    TutorialObj.SetActive(!TutorialObj.activeSelf);
    if(!tutAnim)
    {
      tutAnim = TutorialObj.GetComponent<Animator>();
      tutAnim.speed = TutorialAnimSpeed;
    }
    if(TutorialObj.activeSelf) tutAnim.Play("Start");
  }

  public void OnSettingsClicked()
  {

  }

  public void OnQuitGameClicked()
  {
    //If we are running in a standalone build of the game
#if UNITY_STANDALONE
    //Quit the application
    Application.Quit();
#endif

    //If we are running in the editor
#if UNITY_EDITOR
    //Stop playing the scene
    UnityEditor.EditorApplication.isPlaying = false;
#endif
  }
}
