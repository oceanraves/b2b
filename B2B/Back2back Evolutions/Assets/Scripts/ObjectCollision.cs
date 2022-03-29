using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectCollision : MonoBehaviour
{
    private Vector3 _direction;
    private Transform _playerTransform;
    public float _power;
    public float _secondaryPower;
    float torque;
    float turn;

    //private int _lives = 5;
    bool canBeHitAgain = true;

    [SerializeField]
    bool copCar;
    [SerializeField]
    bool fireHydrant;

    private bool hit = false;

    private ObjectPickUp _objectPickup;

    public bool hasSpawnedWater = false;

    private GameObject _collisionPos;
    private PlayerController _playerController;


    void Start()
    {
        _playerTransform = GameObject.Find("PlayerMover").transform.GetChild(0).transform;
        _objectPickup = gameObject.GetComponent<ObjectPickUp>();
        _playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PlayerHitBox" && canBeHitAgain)
        {
            float torque = Random.Range(150f, 300f);
            turn = Random.Range(20, 50f);

            if (fireHydrant && !_objectPickup.pickedUp)
            {
                SpawnWater();
            }

            if (!fireHydrant)
            {
                _direction = _playerTransform.forward;
                _direction.y = 0.2f;
                gameObject.GetComponent<Rigidbody>().isKinematic = false;
                gameObject.GetComponent<Rigidbody>().AddForce(_direction * _power, ForceMode.Impulse);
                gameObject.GetComponent<Rigidbody>().AddTorque(new Vector3(100f, 1f, 0f) * torque * turn);

            }

            if (copCar)
            {
                if (gameObject.GetComponent<Animator>() != null)
                {
                    gameObject.GetComponent<Animator>().enabled = false;
                }
                //if (_lives > 1)
                //{
                //    _lives -= 1;
                //    canBeHitAgain = false;
                //}
                //if (_lives <= 1)
                //{
                //    ExplodeCar(gameObject.transform.position);
                //}
            }
        }

        if (other.tag == "Player")
        {
            if (copCar && gameObject.GetComponent<CarDriving>() != null)
            {

                //if (gameObject.GetComponent<CarDriving>().CheckVelocity() == true)
                other.GetComponent<BoxCollider>().isTrigger = false;
                Rigidbody rb = other.GetComponent<Rigidbody>();
                rb.isKinematic = false;
                rb.useGravity = true;
                rb.AddForce(gameObject.transform.up * 5, ForceMode.Impulse);
                _playerController.HitByCar();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "PlayerHitBox" && !hit)
        {
            canBeHitAgain = true;
            hit = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "InanimateObject")
        {
            _direction = collision.gameObject.transform.forward;
            gameObject.GetComponent<Rigidbody>().isKinematic = false;
            gameObject.GetComponent<Rigidbody>().AddForce(_direction * _secondaryPower, ForceMode.Impulse);

            if (copCar)
            {
                SpawnExplosion(collision.transform.position);
            }
        }
    }

    private void ExplodeCar(Vector3 position)
    {
        SpawnExplosion(position);
        GameObject brokenCar = Instantiate(Resources.Load("CopCar_Pieces") as GameObject, position, Quaternion.identity);
        Destroy(gameObject);
    }

    private void SpawnExplosion(Vector3 position)
    {
        //Vector3 position = _collisionPos.transform.position;
        GameObject explosionClone = Instantiate(Resources.Load("Explosion_0") as GameObject, position, Quaternion.identity);
        Destroy(explosionClone, 2f);
    }

    public void SpawnWater()
    {
        if (!hasSpawnedWater)
        {
            Vector3 waterPos = gameObject.transform.position;
            Quaternion rotation = new Quaternion();
            rotation.z = -90f;

            _direction = _playerTransform.forward;

            GameObject waterStream = Instantiate(Resources.Load("VFX_FireHydrant_Water") as GameObject, waterPos, Quaternion.identity);
            gameObject.GetComponent<Rigidbody>().isKinematic = false;
            gameObject.GetComponent<Rigidbody>().AddForce(_direction * _power, ForceMode.Impulse);
            hit = true;
            hasSpawnedWater = true;

            GameObject waterCollider = new GameObject();
            waterCollider.name = "WaterCollider";
            waterCollider.tag = "Water";
            waterCollider.transform.SetParent(transform.parent);
            waterCollider.transform.localPosition = Vector3.zero;
            BoxCollider collider = waterCollider.AddComponent<BoxCollider>();
            collider.center = new Vector3(0f, 4, 0f);
            collider.size = new Vector3(3f, 8f, 3f);
            collider.isTrigger = true;
        }
    }
}
