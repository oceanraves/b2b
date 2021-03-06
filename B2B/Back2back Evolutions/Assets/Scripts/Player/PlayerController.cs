using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerAttacks _pAttacks;

    private CharacterController _characterController;
    private float _speed;

    [SerializeField]
    private float _walkSpeed = 5;

    [SerializeField]
    float runSpeed;

    float ogSpeed;

    [SerializeField]
    private float rotationSpeed = 2f;

    private Transform cameraMain;
     
    public GameObject playerModel;
    private Animator _animator;

    private bool _runAnim = false;

    public bool canMove = true;

    private LayerMask environment;

    private Collider[] raycastHitCache = new Collider[4];

    private bool grounded;

    private float groundCheckTimer = 0f;

    public GameObject hitBox_hand;
    public GameObject hitBox_foot;

    float tAngle;

    //private ObjectPickUp _objectPickup;

    private bool _canThrow = false;
    //private bool thrown = true;

    private Quaternion _cameraDirection;

    private Vector3 _dir;

    private bool _rolling = false;
    private LookForPickup _looForPickup;

    public bool liftingCar = false;

    private PlayerHealth _playerHealth;

    private PlayerAnimation _playerAnimation;
    private void Awake()
    {
        environment = 1 << LayerMask.NameToLayer("Environment");
    }

    void Start()
    {
        _pAttacks = gameObject.GetComponent<PlayerAttacks>();
        _speed = _walkSpeed;
        _characterController = gameObject.GetComponent<CharacterController>();
        cameraMain = Camera.main.transform;
        //_objectPickup = gameObject.GetComponent<ObjectPickUp>();
        _looForPickup = gameObject.GetComponent<LookForPickup>();
        _animator = playerModel.GetComponent<Animator>();

        hitBox_hand.gameObject.SetActive(false);
        hitBox_foot.gameObject.SetActive(false);

        _playerAnimation = gameObject.GetComponent<PlayerAnimation>();
        _playerHealth = gameObject.GetComponent<PlayerHealth>();
    }

    void Update()
    {
        if (Input.anyKeyDown)
        {
            _playerAnimation.Animate(false, "Squat");
        }


        groundCheckTimer += Time.deltaTime;
        if (groundCheckTimer > 0.25f)
        {
            grounded = IsGrounded();
            groundCheckTimer = 0;
        }
            //RUN-------------------------------------------
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {   //_speed = runSpeed;
                _runAnim = true;
            }
            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                //_speed = _walkSpeed;
                _runAnim = false;
            }
            //----------------------------------------------
        PlayerMovement();

        //INPUT-----------------------------------------
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (canMove)
            {
                _pAttacks.Push();
                //hitBox_hand.gameObject.SetActive(true);
                _animator.SetTrigger("Attack_Push");
            }
        }
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            if (canMove)
            {
                _pAttacks.Orb();
                _animator.SetTrigger("Attack_Orb");
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (canMove && _dir.magnitude >= 0.1f)
            {
                ogSpeed = _speed;
                _rolling = true;
                //_canMove = false;
                _animator.SetTrigger("Roll");
            }
        }

        if (_rolling)
        {
            _characterController.Move(_dir * 0.15f);
        }
        //----------------------------------------------



        if (Input.GetKeyDown(KeyCode.F))
        {
            //_looForPickup.GetNearObjects();
        }


        if (Input.GetKeyDown(KeyCode.G) && _canThrow)
        {
            _looForPickup.Shield();
        }      
        
        if (Input.GetKeyDown(KeyCode.Q))
        {
            _playerHealth.AddHealth();
            //if (_looForPickup._pickedUp)
            //{
            //    gameObject.GetComponent<EatEnemy>().Eat();
            //
            //enemy eaten animation
            //drop enemy
        }
        //----------------------------------------------
    }

    public void RollComplete()
    {
        _speed = ogSpeed;
        _rolling = false;
        CanMove();
    }

    private void PlayerMovement()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float Vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0, Vertical);

        if (canMove)
        {
            direction = cameraMain.forward * direction.z + cameraMain.right * direction.x;
        }
        else
        {
            direction = new Vector3(0, 0, 0);
        }

        if (_runAnim)
        {
            _speed = runSpeed;
        }else _speed = _walkSpeed;

        if (direction.magnitude >= 0.1f)
        {
            direction.y -= 5f;

            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.Euler(0f, targetAngle, 0f);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * rotationSpeed);

            Vector3 movement = direction;
            movement.y = 0;
            _characterController.Move( movement.normalized * _speed * Time.deltaTime);

            _dir = direction;
            tAngle = targetAngle;
            _cameraDirection = rotation;
        }
        _playerAnimation.MoveAnimation(direction, _runAnim);
    }

    public Quaternion CameraDirection()
    {
        return _cameraDirection;
    }

    private bool IsGrounded()
    {
        int contacts = Physics.OverlapSphereNonAlloc(transform.position, 0.3f, raycastHitCache, environment);
        return contacts != 0;
    }

    public void AttackDone()
    {
        hitBox_hand.gameObject.SetActive(false);
        hitBox_foot.gameObject.SetActive(false);
    }

    public float GiveSpeed()
    {return _speed;}

    public void HitByCar()
    {
        _animator.SetTrigger("HitByCar");
        canMove = false;
    }

    public void PlayIdle()
    {
        gameObject.transform.rotation = new Quaternion(0, 180f, 0f, 0f);
        _animator.Play("Idle");
        CanMove();
    }

    private void CanMove()
    {
        canMove = true;
    }
}