using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    #region "SETUP"
    protected GameObject target;
    protected float bulletDamage;
    public float bulletProximitySensitivity = .5f;
    [Range(1f, 5f)] public float bulletSpeed;

    private Vector3 offset = new Vector3(0,1,0);

    public GameObject Target { get { return target; } set { target = value; } }
    public float BulletDamage { get { return bulletDamage; } set { bulletDamage = value; } }

    void Start()
    {
        this.transform.LookAt(target.transform.position);
    }

    #endregion

    void Update()
    {
        if (Vector3.Distance(this.transform.position, target.transform.position + offset) <= bulletProximitySensitivity)
        {
            if (target.GetComponent<Wizard>())
            {
                target.GetComponent<Wizard>().DealDamage(bulletDamage);
            }
            else if (!target.GetComponent<RangedUnit>() && !target.GetComponent<ResourceBuilding>())
            {
                target.GetComponent<FactoryBuilding>().DealDamage(bulletDamage);
                Object.Destroy(this.gameObject);
                target = null; //in case obj not destroyed
            }
            else
            {
                if (!target.GetComponent<RangedUnit>())
                {
                    target.GetComponent<ResourceBuilding>().DealDamage(bulletDamage);
                    Object.Destroy(this.gameObject);
                    target = null; //in case obj not destroyed
                }
                else
                {
                    target.GetComponent<RangedUnit>().DealDamage(bulletDamage);
                    Object.Destroy(this.gameObject);
                    target = null; //in case obj not destroyed
                }
            }
            
        }
    }

    void FixedUpdate()
    {
        if (target == null)
        {
            Object.Destroy(this.gameObject);
        }
        else
        {
            this.transform.position = Vector3.Lerp(this.transform.position, target.transform.position + offset, bulletSpeed * Time.deltaTime);
            
        }

    }

}
