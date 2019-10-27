using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceStats : MonoBehaviour
{
    public Text blue_res, red_res;

    protected private int team1Steel = 0;
    protected private int team2Steel = 0;

    protected private int team1Wood = 0;
    protected private int team2Wood = 0;

    public int Team1Steel { get { return team1Steel; } set { team1Steel = value; } }
    public int Team2Steel { get { return team2Steel; } set { team2Steel = value; } }
    public int Team1Wood { get { return team1Wood; } set { team1Wood = value; } }
    public int Team2Wood { get { return team2Wood; } set { team2Wood = value; } }

    void LateUpdate()
    {
        blue_res.text = "Resources \nWood: " + team1Wood.ToString() + "\nSteel: " + team1Steel.ToString();
        red_res.text = "Resources \nWood: " + team2Wood.ToString() + "\nSteel: " + team2Steel.ToString();
    }

}
