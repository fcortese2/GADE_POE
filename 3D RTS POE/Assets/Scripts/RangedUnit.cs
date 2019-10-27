using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;


public class RangedUnit : MonoBehaviour
{
    #region "SETUP"
    private string TEAM;
    private NavMeshAgent agent;
    public UnitDefinition UNIT;
    public GameObject bullet;
    public GameObject shootingPoint;

    private GameObject target;
    private bool isFiring = false;

    private float currentHealth;

    private void Start()
    {
        agent = this.GetComponent<NavMeshAgent>();
        TEAM = this.gameObject.tag;
        agent.speed = UNIT.speed;
        currentHealth = UNIT.defence;
        //InvokeRepeating("FindTarget", 7f, 7f);
    }

    public string Team { get { return TEAM; } set { TEAM = value; } }
    public GameObject Target { get { return target; } set { target = value; } }

    #endregion

    void FixedUpdate()
    {
        if (Target == null)
        {
            FindTarget();
            agent.isStopped = true;
        }
        else if (!GameObject.Find(target.name))
        {
            target = null;
        }
        else if (Target != null)
        {
            agent.isStopped = false;
            
            if (UNIT.name == "Knight")
            {
                if (!target.GetComponent<RangedUnit>())
                {
                    if (!target.GetComponent<ResourceBuilding>())
                    {
                        if (Vector3.Distance(this.transform.position, target.transform.position) <= UNIT.range + 1 && isFiring == false)    //FIX THIS
                        {
                            Debug.Log("in range");
                            agent.destination = this.transform.position;
                            agent.isStopped = true;
                            StartCoroutine("Shoot");
                        }
                        else if (agent.isStopped == true)
                        {
                            Debug.Log("Move");
                            agent.destination = target.transform.position;

                            agent.isStopped = false;
                        }
                    }
                    else if (!target.GetComponent<FactoryBuilding>())
                    {
                        if (Vector3.Distance(this.transform.position, target.transform.position) <= UNIT.range + 1 && isFiring == false)
                        {
                            agent.destination = this.transform.position;
                            agent.isStopped = true;
                            StartCoroutine("Shoot");
                        }
                        else
                        {
                            agent.destination = target.transform.position;

                            agent.isStopped = false;
                        }
                    }
                }
                else if (Vector3.Distance(this.transform.position, target.transform.position) <= UNIT.range && isFiring == false)
                {
                    agent.destination = this.transform.position;
                    agent.isStopped = true;
                    StartCoroutine("Shoot");
                    return;
                }
            }
            //old here on after

            if (UNIT.range < Vector3.Distance(this.transform.position, Target.transform.position))           //if other unit is not in range
            {
                agent.destination = target.transform.position;

                agent.isStopped = false;
            }
            else if (UNIT.range >= Vector3.Distance(this.transform.position, Target.transform.position) && isFiring == false )
            {
                //Debug.Log("In range to shoot");
                agent.destination = this.transform.position;
                agent.isStopped = true;
                StartCoroutine("Shoot");
            }
            Aim();
        }
    }

    #region "Actions"

    IEnumerator Shoot()
    {
        if (UNIT.name == "Knight")
        {
            //Debug.Log("isKnight");
            isFiring = true;
            Hit();
            yield return new WaitForSeconds(UNIT.fireRate);
            isFiring = false;
        }
        else if (UNIT.name != "Knight")
        {
            GameObject arrow;
            isFiring = true;
            //Debug.Log("Shoot");
            //target.GetComponent<RangedUnit>().DealDamage(UNIT.attack);

            arrow = Object.Instantiate(bullet, shootingPoint.transform.position, Quaternion.LookRotation(target.transform.position));
            arrow.GetComponent<BulletScript>().Target = target;
            arrow.GetComponent<BulletScript>().BulletDamage = UNIT.attack;

            yield return new WaitForSeconds(UNIT.fireRate);
            isFiring = false;
        }
        
    }

    void Hit()
    {
        //animator stuff here...
        if (target.GetComponent<Wizard>())
        {
            target.GetComponent<Wizard>().DealDamage(UNIT.attack);
        }
        else if (!target.GetComponent<RangedUnit>() && !target.GetComponent<ResourceBuilding>())
        {
            target.GetComponent<FactoryBuilding>().DealDamage(UNIT.attack);
        }
        else
        {
            if (!target.GetComponent<RangedUnit>())
            {
                target.GetComponent<ResourceBuilding>().DealDamage(UNIT.attack);
            }
            else
            {
                target.GetComponent<RangedUnit>().DealDamage(UNIT.attack);
            }
        }
        
        Debug.Log("Hit");
    }

    #endregion

    #region "Targeting"

    void FindTarget()
    {
        GameObject _target = null;
        GameObject[] targets;
        GameObject[] targets_wiz;
        if (Team == "Team1")
        {
            targets = new GameObject[GameObject.FindGameObjectsWithTag("Team2").Length];
            targets = GameObject.FindGameObjectsWithTag("Team2");
            targets_wiz = new GameObject[GameObject.FindGameObjectsWithTag("Team3").Length];
            targets_wiz = GameObject.FindGameObjectsWithTag("Team3");
        }
        else
        {
            targets = new GameObject[GameObject.FindGameObjectsWithTag("Team1").Length];
            targets = GameObject.FindGameObjectsWithTag("Team1");
            targets_wiz = new GameObject[GameObject.FindGameObjectsWithTag("Team3").Length];
            targets_wiz = GameObject.FindGameObjectsWithTag("Team3");
        }
        float distance = Mathf.Infinity;

        for (int i = 0; i < targets.Length; i++)
        {
            if (Vector3.Distance(targets[i].transform.position, this.transform.position) < distance)
            {
                _target = targets[i];
                distance = Vector3.Distance(targets[i].transform.position, this.transform.position);
            }
        }
        foreach (GameObject obj in targets_wiz)
        {
            if (Vector3.Distance(this.transform.position, obj.transform.position) < distance)
            {
                _target = obj;
                distance = Vector3.Distance(this.transform.position, obj.transform.position);
            }
        }

        this.Target = _target;
    }

    private void Aim()
    {
        var qTo = Target.transform.localRotation;
        var v3T = Target.transform.position - this.transform.position;
        v3T.y = this.transform.position.y;
        qTo = Quaternion.LookRotation(v3T);
        var rotation = qTo.eulerAngles;
        this.transform.rotation = Quaternion.RotateTowards(this.transform.rotation, qTo, 55f * Time.deltaTime);

    }

    #endregion

    #region "General"
    public void Destroy()
    {
        Destroy(this.gameObject);
    }

    public void DealDamage(float damage)
    {
        if ((currentHealth - damage) <= 0)
        {
            Destroy();
        }
        else
        {
            currentHealth -= damage;
            //Debug.Log(currentHealth);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(this.transform.position, UNIT.range);
    }

    #endregion
}
