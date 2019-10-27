using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControlledSpawning : MonoBehaviour
{
    public GameObject[] arrSpawnableObjects;
    public GameObject toSpawn;
    private int selectionIndex = 0;

    void Start()
    {
        toSpawn = arrSpawnableObjects[selectionIndex];
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 1000))
            {
                if (hit.transform.tag == "Ground1")
                {
                    Object.Instantiate(toSpawn, hit.point, Quaternion.identity);
                }

            }
        }
    }
}
