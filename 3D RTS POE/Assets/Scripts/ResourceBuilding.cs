using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceBuilding : MonoBehaviour
{
    private int steelResourcesProduced = 0;
    private int woodResourcesProduced = 0;
    public int steelPerRound = 2;
    public int woodPerRound = 2;
    private float health = 100;
    public float generateAfterSeconds = 5f;
    public ResourceStats resources;


    // Update is called once per frame
    void Start()
    {
        InvokeRepeating("SpawnResources", generateAfterSeconds, generateAfterSeconds);        
    }

    void SpawnResources()
    {
        if (this.gameObject.tag == "Team1")
        {
            resources.Team1Steel += steelPerRound;
            resources.Team1Wood += steelPerRound;
        }
        else if (this.gameObject.tag == "Team2")
        {
            resources.Team2Steel += steelPerRound;
            resources.Team2Wood += woodPerRound;
        }
        steelResourcesProduced += steelPerRound;
        woodResourcesProduced += woodPerRound;
    }
}
