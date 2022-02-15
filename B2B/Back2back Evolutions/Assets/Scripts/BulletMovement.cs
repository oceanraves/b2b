using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    public GameObject player;

    private float _moveSpeed = 0.1f;
    Vector3 dir;

    void Start()
    {
        player = GameObject.Find("Player");
        dir = player.transform.position - transform.position;
        dir.y = 0f;
    }

    void FixedUpdate()
    {
        transform.position += dir * _moveSpeed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == player)
        {
            other.GetComponent<PlayerHealth>().HitByBullet("Cop");
        }
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}
