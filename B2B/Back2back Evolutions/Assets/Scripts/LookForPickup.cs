using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookForPickup : MonoBehaviour
{
    private Collider[] raycastHitCache = new Collider[1];
    private LayerMask pickUpLayer;
    public GameObject _object;
    ObjectPickUp _objectPickup;
    public bool _pickedUp = false;
    Animator _animator;

    private void Awake()
    {
        pickUpLayer = 1 << LayerMask.NameToLayer("PickUp");
        _animator = gameObject.GetComponent<Animator>();
    }
    public void GetNearObjects()
    {
        if (!_pickedUp)
        {
            int contacts = Physics.OverlapSphereNonAlloc(transform.position, 2.2f, raycastHitCache, pickUpLayer);
            
            foreach (Collider col in raycastHitCache)
            {
                if(col != null)
                {
                    _pickedUp = true;

                    _object = col.gameObject;

                    _objectPickup = _object.GetComponent<ObjectPickUp>();
                    _objectPickup.StartPickUp();

                    //if (col.gameObject.GetComponent<ObjectPickUp>() != null && col.gameObject.GetComponent<ObjectPickUp>().copCar)
                    //{
                    //    _animator.SetTrigger("Lift");
                    //}

                    if (col.gameObject.GetComponent<ObjectPickUp>().fireHydrant)
                    {
                        col.gameObject.GetComponent<ObjectCollision>().SpawnWater();
                    }
                }
            }
        }
        else Throw();
    }

    public void Throw()
    {
        if (_objectPickup != null)
        {
            _objectPickup.Throw();
            raycastHitCache = new Collider[1];
            _pickedUp = false;
        }
    }

    public void Shield()
    {
        _objectPickup.Shield();
    }

    public void DoneLifting()
    {
        //Destroy(_object.gameObject.GetComponent<Animator>());
        //_object.GetComponent<ObjectPickUp>().pickedUp = true;
    }

    public bool IsPickedUp()
    {
        return _pickedUp;
    }
}