using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarDriving : MonoBehaviour
{
    public Transform target;
    public Rigidbody _rb;

    private float speed = 25f;
    private float rotateSpeed = 3f;

    Vector3 rotateAmount;

    float distanace;
    //bool stop = false;
    bool isBreaking = false;


    private int origin;
    enum Directions { UP, DOWN, LEFT, RIGHT };
    float breakPoint;

    public int originVertical;
    public int originHorizontal;

    float verticalBreakVal;
    float horizontalBreakVal;
    Vector3 direction;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        GetDirection();
    }

    private void GetDirection()
    {
        if (target.transform.position.z < gameObject.transform.position.z)
        {
            originVertical = ((int)Directions.UP);
            verticalBreakVal = 0.1f;
        }
        else
        {
            originVertical = ((int)Directions.DOWN);
            verticalBreakVal = -0.1f;
        }
        //////////////////////////////////////////////
        if (target.transform.position.x < gameObject.transform.position.x)
        {
             originHorizontal = ((int)Directions.RIGHT);
        }
        else
        {
            originHorizontal = ((int)Directions.LEFT);
        }
    }

    private void FixedUpdate()
    {


        if (target != null)
        {
            direction = target.position - gameObject.transform.position;
            direction.Normalize();

            rotateAmount = Vector3.Cross(direction, gameObject.transform.right);
            distanace = Vector2.Distance((Vector2)transform.position, (Vector2)target.transform.position);
        }

        if (speed > 0f)
        {
            _rb.angularVelocity = -rotateAmount * rotateSpeed;
            _rb.velocity = transform.right * speed;

            if (originVertical == (int)Directions.UP)
            {
                if (speed > 0f && (direction.z >= 0.1f || isBreaking))
                {
                    target = null;

                    if (speed > 15f)
                    {
                        speed -= 0.4f;
                    }
                    else if (speed <= 15f && speed > 0f)
                    {
                        speed -= 0.5f;
                    }
                    isBreaking = true;
                }
            }
        }
        if (speed <= 0f)
        {
            _rb.constraints = RigidbodyConstraints.None;
        }
    }

    public bool CheckVelocity()
    {
        bool isMoving = false;
        if (_rb.velocity.x > 0.1f || _rb.velocity.y > 0.1f)
        {
            Debug.Log("Speed");
            isMoving = true;   
        }
        return isMoving;
    }
}
