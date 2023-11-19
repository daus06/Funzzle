using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "New Stat", menuName = "Hangman/Save")]
public class SCR_BaseStats : ScriptableObject
{
    [SerializeField] int totalWins;
    [SerializeField] int totalLosses;
    [SerializeField] float winRatio;
    [SerializeField] int gamesPlayed;
    [SerializeField] int fastestTime = 9999;//IN SECONDS

    void OnEnable()
    {
        if(PlayerPrefs.HasKey("FunzzleStats"))
        {
            string savedata = PlayerPrefs.GetString("FunzzleStats");
            string[] splitStats = savedata.Split('|');

            //Check if splitStats has at least 6 elements before accessing them
            if (splitStats.Length >= 5)
            {
                totalWins = int.Parse(splitStats[0]);
                totalLosses = int.Parse(splitStats[1]);
                winRatio = float.Parse(splitStats[2]);
                gamesPlayed = int.Parse(splitStats[3]);
                fastestTime = int.Parse(splitStats[4]);
            }
            else
            {
                // Handle the case when the array doesn't have enough elements
                Debug.LogError("Not enough elements in splitStats array.");
            }
        }
    }

    public void SaveStats(bool hasWonGame, int playtime)
    {   
        //AssetDatabase.Refresh();
        
        totalWins += (hasWonGame)?1:0;
        totalLosses += (!hasWonGame)?1:0;
        gamesPlayed = totalLosses + totalWins;


        winRatio = ((float)totalWins / gamesPlayed) * 100;
        if(hasWonGame)
        {
            fastestTime = (playtime >= fastestTime)?fastestTime:playtime;
        }
        

        //EditorUtility.SetDirty(this);
        //AssetDatabase.SaveAssets();
        string saveData = "";
        saveData += totalWins.ToString() + "|";
        saveData += totalLosses.ToString() + "|";
        saveData += winRatio.ToString() + "|";
        saveData += gamesPlayed.ToString() + "|";
        saveData += fastestTime.ToString();

        PlayerPrefs.SetString("FunzzleStats",saveData);

    }

    public List<int> GetStats()
    {
        //AssetDatabase.Refresh();
        
        
        List<int> statsList = new List<int>();
        statsList.Add(totalWins);
        statsList.Add(totalLosses);
        statsList.Add(Mathf.RoundToInt(winRatio));
        statsList.Add(gamesPlayed);
        statsList.Add(fastestTime);

        return statsList;
    }

    public void DeleteSave()
    {
        if(PlayerPrefs.HasKey("FunzzleStats"))
        {
            PlayerPrefs.DeleteKey("FunzzleStats");
        }
    }
}
