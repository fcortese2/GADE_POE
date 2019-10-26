using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    protected GameObject target;
    protected float bulletDamage;
    public float bulletProximitySensitivity = .5f;
    [Range(1f, 5f)] public float bulletSpeed;

    public GameObject Target { get { return target; } set { target = value; } }
    public float BulletDamage { get { return bulletDamage; } set { bulletDamage = value; } }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(this.transform.position, target.transform.position) <= bulletProximitySensitivity)
        {
            target.GetComponent<RangedUnit>().DealDamage(bulletDamage);
            Object.Destroy(this.gameObject);
            target = null;
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
            this.transform.position = Vector3.Lerp(this.transform.position, target.transform.position, bulletSpeed * Time.deltaTime);
            this.transform.LookAt(target.transform.position);
        }

    }

}
