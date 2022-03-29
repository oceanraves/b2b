using UnityEngine;
using UnityEngine.AI;

public class EnemyAI_Cop : MonoBehaviour
{
    public Transform target;
    GameObject bulletClone;

    public float maximumLookDistance = 30f;
    public float walkDistance = 16f;
    public float minimumDistanceFromPlayer;
    public float maximumAttackDistance = 10f;
    public float rotationSpeed;

    [SerializeField]
    float walkBuffert;
    [SerializeField]
    GameObject firePoint;

    [SerializeField]
    GameObject gunFlash;

    [SerializeField]
    float walkSpeed;

    [SerializeField]
    float runSpeed;

    public float shotInterval = 0.5f;
    float shotTime = 0f;
    private NavMeshAgent _namMeshAgent;

    public bool Moving_Enabled = true;

    private Rigidbody _rb;

    Animator_Swat animatorScript;

    private bool showGunFlash = false;


    private void Start()
    {
        animatorScript = GetComponent<Animator_Swat>();
        _namMeshAgent = gameObject.GetComponent<NavMeshAgent>();
        _rb = GetComponent<Rigidbody>();

    }
    void Update()
    {
        if (showGunFlash && ((shotTime + 0.03f) < Time.time))
        {
            gunFlash.SetActive(false);
            showGunFlash = false;
        }

        if (Moving_Enabled)
        {
            float distance = Vector3.Distance(target.position, transform.position);

            if (distance <= maximumLookDistance)
            {
                //Rotates Towards Player
                LookAtTarget();
                //

                //STANDING STILL WHEN NEAR PLAYER
                if (distance <= minimumDistanceFromPlayer)
                { 
                    //_namMeshAgent.isStopped = true;
                    _namMeshAgent.speed = 0;
                    _namMeshAgent.enabled = false;
                    animatorScript.Animate("Idle");
                }
                /*
                if (distance <= (minimumDistanceFromPlayer + 3))
                {
                    //MOVE THE ENEMY AWAY FROM PLAYER
                }
                */

                //WALKING
                if (distance <= walkDistance && distance > minimumDistanceFromPlayer)
                {
                    _namMeshAgent.speed = walkSpeed;
                    _namMeshAgent.enabled = true;
                    animatorScript.Animate("Walk");
                    _namMeshAgent.SetDestination(target.position);

                }
                //RUNNING
                if (distance <= maximumLookDistance && distance > minimumDistanceFromPlayer && distance > walkDistance + walkBuffert)
                {
                    _namMeshAgent.speed = runSpeed;
                    //_namMeshAgent.isStopped = false;
                    _namMeshAgent.enabled = true;
                    animatorScript.Animate("Run");
                    _namMeshAgent.SetDestination(target.position);

                }
            }
            
            //else
            //{
            //    //IDLE
            //    _namMeshAgent.speed = 0;
            //    _namMeshAgent.enabled = false;
            //    animatorScript.Animate("Idle");
            //}

            if (distance <= maximumAttackDistance && (Time.time - shotTime) > shotInterval)
            {
                Shoot();
            }
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
        //GUNFLASH//-------------
        shotTime = Time.time;
        gunFlash.SetActive(true);
        showGunFlash = true;
        //-----------------------

        //ANIMATION: NEEDS ANIMATION CLIP
        //animatorScript.Animate("Shoot");
        //animatorScript.Animate("Idle");
        //-------------------------------

        //MOVEMENT STOP-------------------
        _namMeshAgent.speed = 0;
        _namMeshAgent.enabled = false;
        Moving_Enabled = false;
        Invoke("EnableMoving", 0.4f);
        //--------------------------------

        //BULLETSPAWN-------------------------------------------------------
        bulletClone = Instantiate(Resources.Load("ShellLow") as GameObject, firePoint.transform.position,
        Quaternion.LookRotation(target.position - transform.position));
        bulletClone.GetComponent<BulletMovement>().GetTarget(target);
        //------------------------------------------------------------------
    }
    public void OnDeath()
    {
        _namMeshAgent.enabled = false;
        Moving_Enabled = false;
        _rb.isKinematic = true;
        _rb.useGravity = false;
    }

    public void DisableMovement(bool disable)
    {
        if (disable)
        {
            _namMeshAgent.enabled = false;
            Moving_Enabled = false;
        }
        else
        {
            //Debug.Log("Enabled Movement");
            _namMeshAgent.enabled = true;
            Moving_Enabled = true;
        }
    }
    private void EnableMoving()
    {
        DisableMovement(false);
    }
    public Transform GetTarget
    {
        get { return target; }
    }

    //private void GetUp()
    //{
    //    _hit = false;
    //    _namMeshAgent.enabled = true;
    //    _rb.isKinematic = true;
    //    _rb.useGravity = false;
    //    Moving_Enabled = true;
    //    power = _ogPower;
    //    yValue = _ogYvalue;
    //    bounceCounter = 0;
    //    thrown = false;
    //}





    //public void LayDown()
    //{
    //    gameObject.transform.localRotation = new Quaternion(-90f, 180f, 0f, 0f);
    //}
}
