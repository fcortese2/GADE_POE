using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Wizard : MonoBehaviour
{
    private NavMeshAgent agent;
    public UnitDefinition UNIT;

    private GameObject Target = null;
    private float health;

    void Start()
    {
        agent = this.GetComponent<NavMeshAgent>();
        agent.isStopped = true;
        health = UNIT.defence;
        InvokeRepeating("Spell", UNIT.fireRate, UNIT.fireRate);
    }

    void FixedUpdate()
    {
        if (Target == null)
        {
            float closestDistance = Mathf.Infinity;

            GameObject[] T1gameObjects = new GameObject[GameObject.FindGameObjectsWithTag("Team1").Length];
            GameObject[] T2gameObjects = new GameObject[GameObject.FindGameObjectsWithTag("Team2").Length];
            T1gameObjects = GameObject.FindGameObjectsWithTag("Team1");
            T2gameObjects = GameObject.FindGameObjectsWithTag("Team2");

            foreach (GameObject obj in T1gameObjects)
            {
                if (Vector3.Distance(this.transform.position, obj.transform.position) <= closestDistance)
                {
                    closestDistance = Vector3.Distance(this.transform.position, obj.transform.position);
                    Target = obj;
                }
            }
            foreach (GameObject obj in T2gameObjects)
            {
                if (Vector3.Distance(this.transform.position, obj.transform.position) <= closestDistance)
                {
                    closestDistance = Vector3.Distance(this.transform.position, obj.transform.position);
                    Target = obj;
                }
            }
        }
        else
        {
            if (Vector3.Distance(this.transform.position, Target.transform.position) <= UNIT.range - .8f)  //-.8f to make sure the other unit doesnt go out of the AOE as Spell() is called
            {
                agent.isStopped = true;
            }
            else
            {
                agent.isStopped = false;
                agent.SetDestination(Target.transform.position);
            }
            
        }
        

        

    }

    void Spell()
    {
        GameObject[] T1gameObjects = new GameObject[GameObject.FindGameObjectsWithTag("Team1").Length];
        GameObject[] T2gameObjects = new GameObject[GameObject.FindGameObjectsWithTag("Team2").Length];
        T1gameObjects = GameObject.FindGameObjectsWithTag("Team1");
        T2gameObjects = GameObject.FindGameObjectsWithTag("Team2");

        foreach (GameObject target in T1gameObjects)
        {
            if (target.GetComponent<RangedUnit>())
            {
                if (Vector3.Distance(this.transform.position, target.transform.position) <= UNIT.range)
                {
                    target.GetComponent<RangedUnit>().DealDamage(UNIT.attack);
                }
            }
        }

        Target = null; //Wizards refresh their closest target after every spell
    }

    public void DealDamage(float damage)
    {
        if (health - damage <= 0)
        {
            Destroy();
        }
        else
        {
            health -= damage;
        }
        
    }

    void Destroy()
    {
        Object.Destroy(this.gameObject);
    }
}
