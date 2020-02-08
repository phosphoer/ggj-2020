using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuUIHandler : MonoBehaviour
{
  
  public GameObject TutorialObj;
  public float TutorialAnimSpeed = 1f;
  Animator tutAnim;
  
  public void OnNewGameClicked()
  {
    GameStateManager.Instance.SetGameStage(GameStateManager.GameStage.Game);
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
