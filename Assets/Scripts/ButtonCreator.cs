using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class ButtonCreator : MonoBehaviour
{
    public static ButtonCreator instance;

    public GameObject buttonPrefab;
    string[] lettersToUse = new string[26]
    {
        "A","B","C","D","E","F","G","H","I",
        "J","K","L","M","N","O","P","Q","R",
        "S","T","U","V","W","X","Y","Z",
    };
    public Transform buttonHolder;

    List<KeyButton> letterList = new List<KeyButton>();
    
    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        PopulatedKeyboard();
    }

    void PopulatedKeyboard()
    {

        for (int i = 0; i < lettersToUse.Length; i++)
        {
            GameObject newButton = Instantiate(buttonPrefab,buttonHolder,false);
            newButton.GetComponentInChildren<TMP_Text>().text = lettersToUse[i];
            KeyButton myLetter = newButton.GetComponent<KeyButton>();
            myLetter.SetButton(lettersToUse[i]);

            letterList.Add(myLetter);
        }

    }

    public void RemovedLetter(KeyButton theButton)
    {
        letterList.Remove(theButton);
    }

    public void UseHint()//FROM THE HINT BUTTON
    {
        if(GameManager.instance.GameOver() || GameManager.instance.maxHints <= 0)
        {
            Debug.Log("THERE IS NO HINTS LEFT!");
            return;
        }
        GameManager.instance.maxHints --;
        int randomIndex = Random.Range(0,letterList.Count);
        letterList[randomIndex].Sendletter(true);
    }

}
