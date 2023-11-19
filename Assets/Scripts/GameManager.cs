using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;
using System.Linq;
using UnityEngine.SocialPlatforms;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    
    List<string> solvedList = new List<string>();
    string[] unsolvedWord;
    [Header("Letters")]
    [Space]
    public GameObject letterPreferb;
    List<TMP_Text> letterHolderList = new List<TMP_Text>();
    public Transform letterHolder;
    [Header("Categories")]
    [Space]
    public Category[] categories;
    public TMP_Text categoryText;
    [Header("Timer")]
    [Space]
    public TMP_Text timerText;
    int playTime;
    bool gameOver;
    [Header("Hints")]
    [Space]
    public int maxHints = 3;
    [Header("Mistakes")]
    [Space]
    public Animator[] petalList;
    [SerializeField]
    int maxMistakes;
    int currentMistakes;
    

    void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        maxMistakes = petalList.Length;
        Initialize();
        StartCoroutine(Timer());
    }

    void Initialize()
    {
        //PICK A CATEGORY FIRST
        int cIndex = UnityEngine.Random.Range(0,categories.Length);
        categoryText.text = categories[cIndex].name;
        int wIndex = UnityEngine.Random.Range(0,categories[cIndex].wordList.Length);

        //PICK A WORD FROM A LIST OR CATEGORY
        string pickedWord = categories[cIndex].wordList[wIndex];

        //SPLIT THE WORD INTO SINGLE LETTER
        string[] splittedWord = pickedWord.ToCharArray().Select(c => c.ToString()).ToArray();
        unsolvedWord = new string[splittedWord.Length];
        foreach (string letter in splittedWord)
        {
            solvedList.Add(letter);
        }
        //CREATED THE VISUAL
        for (int i = 0; i < solvedList.Count; i++)
        {
            GameObject tempLetter = Instantiate(letterPreferb,letterHolder,false);
            letterHolderList.Add(tempLetter.GetComponent<TMP_Text>());
        }

    }

    public void InputFromButton(string requestedLetter, bool isThatAHint)
    {
        //CHECK IF THE GAME IS NOT GAME OVER YET

        //SEARCH MECHANIC FOR SOLVED LIST
        CheckLetter(requestedLetter, isThatAHint);
    }

    void CheckLetter(string requestedLetter, bool isThatAHint)
    {
        if(gameOver)
        {
            return;
        }

        bool letterFound = false;
        //FIND THE LETTER IN THE SOLVED LIST
        for (int i = 0; i < solvedList.Count; i++)
        {
            if(solvedList[i] == requestedLetter)
            {
                letterHolderList[i].text = requestedLetter;
                unsolvedWord[i] = requestedLetter;
                letterFound = true;
            }
        }

        if(!letterFound && !isThatAHint)
        {
            //MISTAKE STUFF - GRAPHIC REPRESENTATION
            petalList[currentMistakes].SetTrigger("miss");
            currentMistakes++;
        
            //AND DO GAME OVER 
            if(currentMistakes == maxMistakes)
            {
                //Debug.Log("Lost Game");
                UiHandler.instance.LoseCondition(playTime);
                gameOver = true;
                return;
            }
        }

        //CHECK IF GAME WON
        Debug.Log("Game Won?: " + CheckIfWon());
        gameOver = CheckIfWon();
        if(gameOver)
        {
            //SHOW UI
            UiHandler.instance.WinCondition(playTime);
        }
    }

    bool CheckIfWon()
    {
        //CHECK MACHANIC 
        for (int i = 0; i < unsolvedWord.Length; i++)
        {
            if(unsolvedWord[i] != solvedList[i])
            {
                return false;
            }
        }
        return true;
    }

    IEnumerator Timer()
    {
        int seconds = 0;
        int minutes = 0;
        timerText.text = minutes.ToString("D2") + ":" + seconds.ToString("D2");
        while(!gameOver)
        {
            yield return new WaitForSeconds(1);
            playTime++;

            seconds = playTime % 60;
            minutes = (playTime / 60) % 60;

            timerText.text = minutes.ToString("D2") + ":" + seconds.ToString("D2");
        }
    }

    public bool GameOver()
    {
        return gameOver;
    }
}
