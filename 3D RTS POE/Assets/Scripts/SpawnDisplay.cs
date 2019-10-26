using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnDisplay : MonoBehaviour
{
    private float mapWidth = 1;
    private float mapLength = 1;

    public float MapWidth { get { return mapWidth; } set { mapWidth = value; } }
    public float MapLength { get { return mapLength; } set { mapLength = value; } }

    public float yPos { get { return this.transform.position.y; } }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(this.transform.position, new Vector3(mapLength, 12, mapWidth));
    }
}
