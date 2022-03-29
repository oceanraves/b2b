using UnityEngine;
using UnityEngine.AI;

public class ObjectPickUp : MonoBehaviour
{
    public Vector3 grabOffsetShield;

    [SerializeField]
    float _throwPower;

    public bool pickedUp = false;

    private Vector3 _direction;

    [SerializeField]
    private float upDirection;

    private PlayerController _pController;

    private bool _useAsShield = false;

    [SerializeField]
    Quaternion carGrabRotation;

    public bool fireHydrant;
    public bool copCar;
    [SerializeField]
    bool tree;
    [SerializeField]
    bool pole;
    [SerializeField]
    bool sign_0;    
    
    public bool enemyCop;

    private GameObject _player;


    private Transform _target;

    public Vector3 objectOffset;

    //private bool _gotOffset = false;

    LookForPickup lookForPickup;

    Quaternion rotation;
    void Awake()
    {
        //pickUpLayer = 1 << LayerMask.NameToLayer("PickUp");
        _player = GameObject.Find("Player");
        _target = _player.transform.Find("Ch36").transform;
        _pController = _player.GetComponent<PlayerController>();
        lookForPickup = _player.GetComponent<LookForPickup>();
    }

    void Update()
    {
        if (pickedUp)
        {
            //Pickup();

            rotation = _target.transform.rotation;

            gameObject.transform.position = (_target.transform.position + objectOffset);

            //if (!copCar)
            //{
            //    Pickup();
            //} else
            //{
            //    gameObject.transform.localRotation = carGrabRotation;
            //    Pickup();
            //}

            //gameObject.transform.localRotation = carGrabRotation;
            gameObject.transform.rotation = rotation;

            if (_useAsShield)
            {
                //gameObject.transform.localRotation = carGrabRotation;
                gameObject.transform.position += grabOffsetShield;
            }
        }
    }


    private void Pickup()
    {
        //Vector3 objectPosition = _player.transform.position;
        //objectPosition = objectOffset;

        rotation = _player.transform.rotation;

        gameObject.transform.position = (_player.transform.position + objectOffset);
    }



    public void Throw()
    {
        if (gameObject.GetComponent<NavMeshObstacle>() != null)
        {
            gameObject.GetComponent<NavMeshObstacle>().enabled = false;
        }

        if (pickedUp && lookForPickup._object != null)
        {
            float torque;
            float turn;
            float runSpeed;

            if (_useAsShield)
            {
                torque = Random.Range(150f, 300f);
                turn = Random.Range(20, 50f);
                _direction = gameObject.transform.forward + new Vector3(0f, 0f, 0f);
            }

            if (tree)
            {
                torque = Random.Range(150f, 300f);
                turn = Random.Range(20, 50f);
            }

            if (copCar)
            {
                _direction = gameObject.transform.forward + new Vector3(0f, upDirection, 0f);
                //_player.GetComponent<PlayerAnimation>().CancelHoldAnimation();
            }

            if (fireHydrant)
            {
                gameObject.GetComponent<ObjectCollision>().hasSpawnedWater = true;
                torque = Random.Range(150f, 300f);
                turn = Random.Range(20, 50f);
            }

            if (pole)
            {
                torque = Random.Range(150f, 300f);
                turn = Random.Range(20, 50f);
            }

            if (sign_0)
            {
                torque = Random.Range(150f, 300f);
                turn = Random.Range(20, 50f);
            }

            if (enemyCop)
            {
                
                //gameObject.GetComponent<EnemyHealth>().thrown = true;
                //gameObject.GetComponent<EnemyHealth>().yValue = 2f;

                //gameObject.GetComponent<EnemyHealth>().SetFlyProperties();
                torque = Random.Range(-150f, 300f);
                turn = Random.Range(-100f, 200f);
                
            }

            else
            {
                torque = Random.Range(150f, 300f);
                turn = Random.Range(100f, 200f);
            }

            _direction = gameObject.transform.forward + new Vector3(0f, upDirection, 0f);

            runSpeed = _pController.GiveSpeed();
            gameObject.GetComponent<Rigidbody>().isKinematic = false;
            gameObject.GetComponent<Rigidbody>().AddForce(_direction * _throwPower * (runSpeed * 0.8f), ForceMode.Impulse);
            gameObject.GetComponent<Rigidbody>().AddTorque(new Vector3(100f, 1f, 0f) * torque * turn); 
        }
        pickedUp = false;
        _useAsShield = false;
    }

    public void Shield()
    {
        if (copCar)
        {
            _useAsShield = true;
        }
    }

    public void PickedUp()
    {
        pickedUp = true;
        //Pickup();
    }

    public void StartPickUp()
    {
        pickedUp = true;

        //if (!copCar)
        //{
        //    pickedUp = true;
        //}
        //else
        //{
        //    _player.GetComponent<Animator>().enabled = true;

        //    _player.GetComponent<PlayerAnimation>().LiftCarAnimation();
        //}
    }
}
