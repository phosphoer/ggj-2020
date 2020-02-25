using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MainMenuUIHandler : MonoBehaviour
{
  
  public GameObject TutorialObj;
  public GameObject ScalableText;
  public float TutorialAnimSpeed = 1f;
  Animator tutAnim;
  
  public List <GameObject> MainMenuObjs;
  public List <GameObject> DifficultyObjs;
  EventSystem eventSystem;

  GameObject tutorialObj;
    
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
    SetInstructionsVisible(false);

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
    //Spawn the tutorial object if we don't have one.
    if(!tutorialObj) SpawnTutorialObject();
    else tutorialObj.SetActive(!tutorialObj.activeSelf); //We don't want to toggle tutorial visibility if we just created it.
    
    ScalableText.SetActive(tutorialObj.activeSelf);//Regardless the scalable text inside the main menu should be toggled to the state of the tutorial.
    //Next part controls the playing of the animation and setting of the speed scaler, set on this objecct.
    if(!tutAnim)
    {
      tutAnim = tutorialObj.GetComponentInChildren<Animator>();
      tutAnim.speed = TutorialAnimSpeed;
    }
    //If we just activated the tutorial we want the animation to play from the top. This prevents it starting in the middle on toggle on.
    if(tutorialObj.activeSelf) tutAnim.Play("Start");
  }
  public void SetInstructionsVisible(bool state)
  {
    if(tutorialObj)
    {
      tutorialObj.SetActive(state);
      ScalableText.SetActive(state);
    }
  }
  public void SpawnTutorialObject()
  {
    tutorialObj = Instantiate(TutorialObj,TutorialObj.transform.position,TutorialObj.transform.rotation) as GameObject;
  }

  public void OnSettingsClicked()
  {
    SetInstructionsVisible(false);
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
