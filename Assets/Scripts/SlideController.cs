using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlideController : MonoBehaviour
{
    public GameObject instructionsPanel;
    public Slider slideButton;

    public void OnSliderValueChanged(float value)
    {
        float xPos = value * (Screen.width + instructionsPanel.GetComponent<RectTransform>().rect.width);
        instructionsPanel.GetComponent<RectTransform>().anchoredPosition = new Vector2(xPos, 0);
    }

    public void ToggleInstructions()
    {
        instructionsPanel.SetActive(!instructionsPanel.activeSelf);
    }
}
