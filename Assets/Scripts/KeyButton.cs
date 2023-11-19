using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyButton : MonoBehaviour
{
    string letter;

    public void SetButton(string _letter)
    {
        letter = _letter;
    }

    public void Sendletter(bool isThatAHint)//BUTTON INPUT OR HINT
    {
        Debug.Log("My letter is: " + letter);
        GameManager.instance.InputFromButton(letter, isThatAHint);
        ButtonCreator.instance.RemovedLetter(this);
        GetComponent<Button>().interactable = false;
    }
}
