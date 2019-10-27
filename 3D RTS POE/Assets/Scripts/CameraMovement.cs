using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    protected float speed = 15f;
    protected float vertSpeed = 10f;

    public float Speed { set { speed = value; } }


    void Update()
    {
        Vector3 pos = transform.position;

        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            speed += 1.5f;
            vertSpeed += 1.5f;
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            speed -= 1.5f;
            vertSpeed -= 1.5f;
        }

        speed = Mathf.Clamp(speed, 0.1f, Mathf.Infinity);
        vertSpeed = Mathf.Clamp(vertSpeed, 0, Mathf.Infinity);

        if (Input.GetKey(KeyCode.W))
        {
            pos.z -= speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            pos.z += speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A))
        {
            pos.x += speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {
            pos.x -= speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.Q))
        {
            pos.y -= vertSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.E))
        {
            pos.y += vertSpeed * Time.deltaTime;
        }

        pos.x = Mathf.Clamp(pos.x, -97f, 97f);
        pos.y = Mathf.Clamp(pos.y, 1.9f, 50f);
        pos.z = Mathf.Clamp(pos.z, -145f, 100f);

        transform.position = pos;

    }
}
