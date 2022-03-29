using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class EnemyHealth : MonoBehaviour
{

    private EnemyAI_Cop _aiCop;
    private Transform target;

    private int _health;


    [SerializeField]
    int _playerDamage;

    [SerializeField]
    int maxHealth;


    private Rigidbody _rb;

    //private float _timeSinceKick = 0;

    //int bounceCounter = 0;

    private bool _alreadyHit = false;
    //private bool _pickedUp = false;
    public bool thrown = false;
    //private bool _getUp = false;

    public float yValue;
    private float _ogYvalue;

    public float power;
    private float _ogPower;

    [SerializeField]
    Quaternion bloodRotation;

    private GameObject bloodClone;
    VisualEffect bloodVFX;
    private void Start()
    {
        _aiCop = GetComponent<EnemyAI_Cop>();
        _health = maxHealth;
        _rb = GetComponent<Rigidbody>();
        _ogPower = power;
        _ogYvalue = yValue;
    }

    void Update()
    {
        _alreadyHit = false;

        if(bloodClone != null)
        {
            if(bloodVFX.aliveParticleCount <= 0)
            {
                //Destroy(bloodClone);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 8)
        {
            /*
            if (_hit || thrown)
            {
                if (bounceCounter < 3)
                {
                    _direction = _aiCop.GetDirection;
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
                }
            }*/
        }
        /*
        if (collision.gameObject.tag == "Enemy")
        {
            PushAway();
        }
        */
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PlayerHitBox")
        {
            if (_alreadyHit) return;

            _alreadyHit = true;

            PushAway();            
            TakeDamage();
        }
    }
    private void TakeDamage()
    {
        _alreadyHit = true;
        _health -= _playerDamage;

        if (_health <= 0)
        {
            Debug.Log("Ded");

            GameObject exploded = Resources.Load("Swat Separated") as GameObject;
            Instantiate(exploded, gameObject.transform.position, gameObject.transform.rotation);

            Destroy(gameObject);
        }
        else
        {
            GameObject bloodObject = Resources.Load("VFX_BloodSplatter") as GameObject;
            bloodClone = Instantiate(bloodObject, gameObject.transform);
            bloodClone.transform.localPosition = new Vector3(0, 1.7f, 0);
            bloodClone.transform.localRotation = new Quaternion(0f, 180, 0f, 0f);
            bloodClone.GetComponent<VisualEffect>().SendEvent("Play");
            bloodClone.transform.parent = null;
            bloodVFX = bloodClone.GetComponent<VisualEffect>();

            Destroy(bloodClone, 1.5f);
        }
    }
    private void EnemyDeath()
    {
        _aiCop.OnDeath();
    }
    
    private void PushAway()
    {
        //SetFlyProperties();
        target = _aiCop.GetTarget;
        Vector3 _direction = (target.transform.forward + new Vector3(0f, yValue, 0f));

        _rb.AddForce(_direction * power, ForceMode.Impulse);

        //float xRot = Random.Range(-30f, 30f);
        //float yRot = Random.Range(-40f, 40f);
        //_rb.AddTorque(new Vector3(xRot, yRot, 0), ForceMode.Impulse);

    }
    /*
    private void SetFlyProperties()
    {
        //_aiCop.DisableMovement(true);
        //_rb.isKinematic = false;
        //_rb.useGravity = true;

        //Invoke("ResetFlyProperties", 10f);
    }     

    public void ResetFlyProperties()
    {
        _aiCop.DisableMovement(false);
        _rb.isKinematic = true;
        _rb.useGravity = false;
    }
    */
}
