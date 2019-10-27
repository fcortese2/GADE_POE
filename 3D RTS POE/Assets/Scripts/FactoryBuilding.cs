using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactoryBuilding : MonoBehaviour
{
    public int steelSpawnCost = 5, woodSpawnCost = 5;
    public float spawnInterval = 10f;
    private ResourceStats resources;
    public GameObject[] unitsToSpawn;
    private float health;
    
    private bool spawning = false;

    #region "UNITY"

    void Start()
    {
        resources = GameObject.Find("GM").GetComponent<ResourceStats>();
        health = Random.Range(60f, 100f);
    }

    // Update is called once per frame
    void Update()
    {
        if (!spawning)
        {
            StartCoroutine("SpawnUnit");
        }
    }

    #endregion

    #region "Factory Spawning"
    IEnumerator SpawnUnit()
    {
        spawning = true;
        GameObject objToSpawn = RandomGameObject();

        if (this.gameObject.tag == "Team1")
        {
            if ((resources.Team1Steel >= steelSpawnCost) && (resources.Team1Wood >= woodSpawnCost))
            {
                GameObject.Instantiate(objToSpawn, this.transform.Find("SpawnPoint").transform.position, this.transform.Find("SpawnPoint").transform.rotation);
                resources.Team1Steel -= steelSpawnCost;
                resources.Team1Wood -= woodSpawnCost;
            }
        }
        else if (this.gameObject.tag == "Team2")
        {
            if ((resources.Team2Steel >= steelSpawnCost) && (resources.Team2Wood >= woodSpawnCost))
            {
                GameObject.Instantiate(objToSpawn, this.transform.Find("SpawnPoint").transform.position, this.transform.Find("SpawnPoint").transform.rotation);
                resources.Team2Steel -= steelSpawnCost;
                resources.Team2Wood -= woodSpawnCost;
            }
        }

        
        yield return new WaitForSeconds(spawnInterval);
        spawning = false;
    }

    private GameObject RandomGameObject()   
    /*
     * Used arrays to store spawnable game objects
     * so that they can be easily added at a later stage
     * without having to come back and edit the code.
     */
    {
        float ran;
        ran = Random.Range((int)0, unitsToSpawn.Length);
        //Debug.Log(ran);

        return unitsToSpawn[(int)ran];
    }

    #endregion

    #region "General"

    private void Destroy()
    {
        Object.Destroy(this.gameObject);
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

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(this.transform.position, 4 + 2);
    }
    #endregion
}
