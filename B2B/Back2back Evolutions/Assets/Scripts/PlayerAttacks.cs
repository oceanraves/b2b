using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class PlayerAttacks : MonoBehaviour
{
    private Collider[] raycastHitCache = new Collider[10];
    private LayerMask pickUpLayer;
    public GameObject _object;

    [SerializeField]
    private float _sphereSize;

    [Header("Attack Colliders")]
    [SerializeField] private GameObject orb;
    private CapsuleCollider orbCollider;
    [SerializeField] private GameObject push;
    private BoxCollider pushCollider;


    [SerializeField] private float orbDuration;
    [SerializeField] private float pushDuration;

    private PlayerController _playerController;

    private VisualEffect _orbEffect;
    private VisualEffect _pushEffect;

    private void Start()
    {
        orbCollider = orb.GetComponent<CapsuleCollider>();
        pushCollider = push.GetComponent<BoxCollider>();


        _playerController = gameObject.GetComponent<PlayerController>();
        _orbEffect = orb.GetComponent<VisualEffect>();
        _pushEffect = push.GetComponent<VisualEffect>();
    }

    public void Orb()
    {
        if (_playerController.canMove)
        {
            orbCollider.enabled = true;
            _orbEffect.SendEvent("Play");
            _playerController.canMove = false;
            Invoke("ResetAttack", orbDuration);
        }
    }

    public void Push()
    {
        if (_playerController.canMove)
        {
            _pushEffect.SendEvent("Play");
            _playerController.canMove = false;
            Invoke("ResetAttack", pushDuration);
        }
    }

    private void ResetAttack()
    {
        orbCollider.enabled = false;
        pushCollider.enabled = false;
        _playerController.canMove = true;
    }


    public void EnablePushCollider()
    {
        pushCollider.enabled = true;
    }



    //private void OnDrawGizmosSelected()
    //{
    //    Gizmos.color = Color.yellow;
    //    Gizmos.DrawSphere(transform.position, _sphereSize);
    //}
}
