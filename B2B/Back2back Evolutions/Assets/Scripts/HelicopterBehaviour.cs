using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelicopterBehaviour : MonoBehaviour
{
    Vector3 direction;
    [SerializeField]
    Transform target;
    float distance;
    Vector3 rotateAmount;
    private Rigidbody _rb;
    public float speed;
    public float rotateSpeed;

    private Vector3 ogPos;
    public float maxHeight;
    public float hoverSpeed;


    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        ogPos = this.transform.position;
        //maxHeight = new Vector3(0f, 1f, 0f);
    }

    void FixedUpdate()
    {
        direction.Normalize();

        direction = target.position - gameObject.transform.position;

        rotateAmount = Vector3.Cross(direction, gameObject.transform.right);

        distance = Vector2.Distance((Vector2)transform.position, (Vector2)target.transform.position);

        _rb.angularVelocity = -rotateAmount * rotateSpeed;

        if (distance > 1f)
        {
            _rb.velocity = transform.right * speed;
        }
        else
        {
            _rb.velocity = Vector3.zero;
        }

        //if (gameObject.transform.position.y < (ogPos.y + maxHeight.y))
        //{
        //    _rb.velocity += Vector3.up;
        //} else
        //    _rb.velocity += Vector3.down;

        gameObject.transform.position = new Vector3(0f, maxHeight, 0f) * Mathf.Cos(Time.time) * hoverSpeed;
    }
}
