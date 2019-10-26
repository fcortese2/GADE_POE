using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Unit Details", menuName = "Unit Details", order = 1)]
//FILIPPO CORTESE
public class UnitDefinition : ScriptableObject
{
    public string UnitName;
    public int attack;
    public int defence;
    public float speed;
    public float range;
    public float fireRate;
}
