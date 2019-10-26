using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPointScript : MonoBehaviour
{
    void OnDrawGizmos()
    {
        if (this.gameObject.tag == "Team1")
        {
            Gizmos.color = Color.blue;
        }
        else
        {
            Gizmos.color = Color.red;
        }
        Gizmos.DrawWireSphere(this.transform.position, .8f);
    }

}
