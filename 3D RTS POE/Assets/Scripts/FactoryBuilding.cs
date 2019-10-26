using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactoryBuilding : MonoBehaviour
{
    public float spawnInterval = 10f;
    public GameObject[] unitsToSpawn;
    private float health;

    private bool spawning = false;

    // Update is called once per frame
    void Update()
    {
        if (!spawning)
        {
            StartCoroutine("SpawnUnit");
        }
    }

    IEnumerator SpawnUnit()
    {
        spawning = true;
        GameObject objToSpawn = RandomGameObject();

        GameObject.Instantiate(objToSpawn, this.transform.Find("SpawnPoint").transform.position, this.transform.Find("SpawnPoint").transform.rotation);
        yield return new WaitForSecondsRealtime(spawnInterval);
        spawning = false;
    }

    private GameObject RandomGameObject()
    {
        float ran;
        ran = Random.Range((int)0, unitsToSpawn.Length);
        Debug.Log(ran);

        return unitsToSpawn[(int)ran];
    }

    private void Destroy()
    {
        DestroyObject(this);
    }
}
