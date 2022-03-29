using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    private GameObject player;
    private float _moveSpeed = 0.2f;
    Vector3 dir;

    private void Start()
    {
        Destroy(gameObject, 1.5f);
    }
    void FixedUpdate()
    {
        transform.position += dir * _moveSpeed;
    }

    public void GetTarget(Transform target)
    {
        player = target.gameObject;
        dir = player.transform.position - transform.position;
        dir.y = 0f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == player)
        {
            other.GetComponent<PlayerHealth>().HitLocation(transform.position);
            other.GetComponent<PlayerHealth>().HitRotation(transform.rotation);
            other.GetComponent<PlayerHealth>().HitByBullet("Cop");
        }
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }


}
