using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceBuilding : MonoBehaviour
{
    private int steelResourcesProduced = 0;
    private int woodResourcesProduced = 0;
    public int steelPerRound = 2;
    public int woodPerRound = 2;
    private float health = 60;
    public float generateAfterSeconds = 5f;
    private ResourceStats resources;
    public float resourcePool = 60;
    

    // Update is called once per frame
    void Start()
    {
        resources = GameObject.Find("GM").GetComponent<ResourceStats>();
        InvokeRepeating("SpawnResources", generateAfterSeconds, generateAfterSeconds);
        health = Random.Range(60f, 100f);
        resourcePool = Random.Range(40f, 80f);
        steelPerRound = Random.Range(1, 6);
        woodPerRound = Random.Range(1, 6);
    }

    void SpawnResources()
    {
        if (resourcePool - steelPerRound < 0)
        {
            Object.Destroy(this.gameObject);
        }

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

        if (steelPerRound > woodPerRound)
        {
            resourcePool -= steelPerRound;
        }
        else
        {
            resourcePool -= woodPerRound;
        }
        

    }

    public void DealDamage(float damage)
    {
        if ((health - damage) <= 0)
        {
            Destroy();
        }
        else
        {
            health -= damage;
            //Debug.Log(currentHealth);
        }
    }

    void Destroy()
    {
        Object.Destroy(this.gameObject);
    }
}
