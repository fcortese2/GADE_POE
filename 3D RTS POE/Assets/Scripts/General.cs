using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class General : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public GameObject wave1Red;
    public GameObject wave2Red;

    public GameObject blueKnights;
    public GameObject redKnights;

    public GameObject gate;
    void Update()
    {
        if (wave1Red.transform.childCount <= 2)
        {
            Debug.Log("wave 2");
            for (int i = 0; i < wave2Red.transform.childCount; i++)
            {
                wave2Red.transform.GetChild(i).GetComponent<RangedUnit>().enabled = true;
            }
            wave2Red.GetComponent<RangedUnit>().enabled = true;
        }
        if (wave2Red.transform.childCount == 0)
        {
            Object.Destroy(gate.gameObject);

            for (int i = 0; i < blueKnights.transform.childCount; i++)
            {
                blueKnights.transform.GetChild(i).GetComponent<RangedUnit>().enabled = true;
            }

            for (int i = 0; i < redKnights.transform.childCount; i++)
            {
                redKnights.transform.GetChild(i).GetComponent<RangedUnit>().enabled = true;
            }

        }
    }
}
