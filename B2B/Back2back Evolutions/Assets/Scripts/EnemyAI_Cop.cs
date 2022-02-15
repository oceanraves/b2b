using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI_Cop : MonoBehaviour
{
    public Transform target;
    GameObject bulletClone;

    float maximumLookDistance = 30f;
    public float maximumAttackDistance = 10f;
    public float minimumDistanceFromPlayer;
    public float rotationSpeed;

    public float shotInterval = 0.5f;
    float shotTime = 0f;
    private NavMeshAgent _namMeshAgent;

    public bool Moving_Enabled = true;

    private Rigidbody _rb;

    public Vector3 _direction;
    public float power;
    private float _ogPower;
    public float yValue;
    private float _ogYvalue;

    private bool _hit = false;

    private float _timeSinceKick = 0;
    private bool _getUp = false;
    int bounceCounter = 0;

    [SerializeField]
    int _playerDamage;

    [SerializeField]
    int maxHealth;

    private int _health;

    private bool _alreadyHit = false;

    private bool _pickedUp = false;

    public bool thrown = false;
    private void Start()
    {
        _namMeshAgent = gameObject.GetComponent<NavMeshAgent>();
        _rb = gameObject.GetComponent<Rigidbody>();
        _ogPower = power;
        _ogYvalue = yValue;

        _health = maxHealth;
    }
    void Update()
    {
        _alreadyHit = false;

        if (Moving_Enabled && _namMeshAgent.enabled == true)
        {
            float distance = Vector3.Distance(target.position, transform.position);
            if (distance <= maximumLookDistance)
            {
                LookAtTarget();

                    if (distance <= minimumDistanceFromPlayer)
                    {
                        _namMeshAgent.isStopped = true;
                    }
                    else
                    {
                        _namMeshAgent.isStopped = false;
                        _namMeshAgent.SetDestination(target.position);
                    }
                

                if (distance <= maximumAttackDistance && (Time.time - shotTime) > shotInterval && !target.GetComponent<LookForPickup>().IsPickedUp())
                {
                    Shoot();
                }
            }
        }

        if (Time.time > _timeSinceKick + 4 && (_hit || thrown))
        {
            GetUp();
        }
    }

    private void LookAtTarget()
    {
        Vector3 dir = target.position - transform.position;
        dir.y = 0;
        Quaternion rotation = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotationSpeed);
    }

    private void Shoot()
    {
        shotTime = Time.time;

        bulletClone = Instantiate(Resources.Load("Bullet") as GameObject, gameObject.transform.Find("GunPoint").transform.position,
            Quaternion.LookRotation(target.position - transform.position));
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 8)
        {
            if (_hit || thrown)
            {
                if (bounceCounter < 3)
                {
                    bounceCounter++;
                    power = power * 0.8f;
                    yValue = yValue * 0.6f;

                    _direction += new Vector3(0f, yValue, 0f);
                    _rb.AddForce(_direction * power, ForceMode.Impulse);
                }
                else
                {
                    _alreadyHit = false;
                    TakeDamage();
                    GetUp();
                }
            }
        }
        //if (collision.gameObject.tag == "InanimateObject" && collision.gameObject.GetComponent<ObjectPickUp>() != null) 
        //{
        //        if (collision.gameObject.GetComponent<ObjectPickUp>().copCar)
        //        {
        //            GameObject flattened = Instantiate(Resources.Load("Enemy_Flattened") as GameObject, gameObject.transform.position, Quaternion.identity);
        //            Vector3 fPos = flattened.transform.position;
        //            fPos.y = 5f;
        //            flattened.transform.position = fPos;
        //            Destroy(gameObject);
        //        }
        //}

        if(collision.gameObject.tag == "Enemy")
        {
            PushAway();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PlayerHitBox")
        {
            PushAway();
            if (_alreadyHit) return;
            _alreadyHit = true;
            _health -= _playerDamage;

            if (_health <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

    private void PushAway()
    {
        SetFlyProperties();
        _direction = (target.transform.forward + new Vector3(0f, yValue, 0f));

        float xRot = Random.Range(-30f, 30f);
        float yRot = Random.Range(-40f, 40f);

        _rb.AddForce(_direction * power, ForceMode.Impulse);
        _rb.AddTorque(new Vector3(xRot, yRot, 0), ForceMode.Impulse);
        _hit = true;

    }
    private void GetUp()
    {
        _hit = false;
        _namMeshAgent.enabled = true;
        _rb.isKinematic = true;
        _rb.useGravity = false;
        Moving_Enabled = true;
        power = _ogPower;
        yValue = _ogYvalue;
        bounceCounter = 0;
        thrown = false;
    }
    private void TakeDamage()
    {
        if (_alreadyHit) return;
        _alreadyHit = true;
        _health -= _playerDamage;

        if (_health <= 0)
        {
            //ENEMY DEATH//
            //Play death animation
            _namMeshAgent.enabled = false;
            Moving_Enabled = false;
            _rb.isKinematic = true;
            _rb.useGravity = false;
            LayDown();

            //Destroy(gameObject);
        }
    }

    public void SetFlyProperties()
    {
        _namMeshAgent.enabled = false;
        Moving_Enabled = false;
        _rb.isKinematic = false;
        _rb.useGravity = true;
        _timeSinceKick = Time.time;
    }

    public void LayDown()
    {
        gameObject.transform.localRotation = new Quaternion(-90f, 180f, 0f, 0f);
    }
}
