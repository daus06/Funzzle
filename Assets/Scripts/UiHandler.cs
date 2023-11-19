using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UiHandler : MonoBehaviour
{
  public static UiHandler instance;

  public Animator gameOverPanel;//ID 1
  public Animator statsPanel;//ID 2
  public Animator winPanel;//ID 3
  public Animator settingsPanel;//ID 4
  [Header("STATS")]
  public TMP_Text statsText;
  [SerializeField] SCR_BaseStats saveFile;


  void Awake()
  {
    instance =  this;
  }

  void Start()
  {
    UpdateStatsText();
  }

  public void SettingsButton()//TOP LEFT CORNER BUTTON
  {
    settingsPanel.SetTrigger("open");
  }

  public void StatsButton()//TOP LEFT CORNER BUTTON
  {
    statsPanel.SetTrigger("open");
    UpdateStatsText();
  }

  void UpdateStatsText()
  {
    List<int> statsList = saveFile.GetStats();
    statsText.text = 
      "Total Wins: " + statsList[0] + "\n" +
      "Total Losses: " + statsList[1] + "\n" +
      "Total Games Played: " + statsList[3] + "\n" +
      "Win % Rate: " + statsList[2] + "% \n" +
      "Fastest Time: " + statsList[4] + " seconds \n"
      ;
  }

  public void ClosePanelButton(int buttonID)
  {
    switch(buttonID)
    {
        case 1:
            gameOverPanel.SetTrigger("close");
        break;
        case 2:
            statsPanel.SetTrigger("close");
        break;
        case 3:
            winPanel.SetTrigger("close");
        break;
        case 4:
            settingsPanel.SetTrigger("close");
        break;
    }
  }

  public void WinCondition(int playTime)//COULD PASS IN MISTAKES USED AND TIME USED
  {
    saveFile.SaveStats(true,playTime);
    winPanel.SetTrigger("open");
  }
  public void LoseCondition(int playTime)//COULD PASS IN MISTAKES USED AND TIME USED
  {
    saveFile.SaveStats(false,playTime);
    gameOverPanel.SetTrigger("open");
  }

  public void BackToMenu(string levelToLoad)
  {
    SceneManager.LoadScene(levelToLoad);
  }
  public void ResetGame()
  {
    //LOAD THE CURRENT OPEN SCENE
    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
  }

  public void ExitGame()
  {
    Application.Quit();
  }
}
