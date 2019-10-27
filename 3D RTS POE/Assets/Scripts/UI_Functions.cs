using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Functions : MonoBehaviour
{
    float currentTimescale = 1;

    public Text timescaleText;

    public void ToggleTimescale()
    {
        if (currentTimescale == 1)
        {
            currentTimescale = 3;
            Time.timeScale = currentTimescale;
            timescaleText.text = ">>>";
        }
        else if (currentTimescale == 3)
        {
            currentTimescale = 7;
            Time.timeScale = currentTimescale;
            timescaleText.text = ">";
        }
        else if (currentTimescale == 7)
        {
            currentTimescale = 1;
            Time.timeScale = currentTimescale;
            timescaleText.text = ">>";
        }
    }

    public void PlayPause()
    {
        if (Time.timeScale != 0.001f)
        {
            Time.timeScale = 0.001f;
        }
        else
        {
            Time.timeScale = 1f;
        }
        
    }
}
