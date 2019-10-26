using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceStats : MonoBehaviour
{
    protected private int team1Steel = 0;
    protected private int team2Steel = 0;

    protected private int team1Wood = 0;
    protected private int team2Wood = 0;

    public int Team1Steel { get { return team1Steel; } set { team1Steel = value; } }
    public int Team2Steel { get { return team2Steel; } set { team2Steel = value; } }
    public int Team1Wood { get { return team1Wood; } set { team1Wood = value; } }
    public int Team2Wood { get { return team2Wood; } set { team2Wood = value; } }

}
