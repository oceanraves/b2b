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
    [SerializeField] private GameObject orbCollider;

    [SerializeField] private float orbDuration;

    private PlayerController _playerController;

    private VisualEffect _orbEffect;
    private void Start()
    {
        _playerController = gameObject.GetComponent<PlayerController>();
        _orbEffect = orbCollider.GetComponent<VisualEffect>();
        _orbEffect.SendEvent("Stop");
    }

    public void Push()
    {
        if (_playerController.canMove)
        {
            orbCollider.SetActive(true);
            _orbEffect.SendEvent("Play");
            _playerController.canMove = false;
            Invoke("ResetAttack", orbDuration);
        }
    }

    private void ResetAttack()
    {
        orbCollider.SetActive(false);
        _playerController.canMove = true;
    }
    //private void OnDrawGizmosSelected()
    //{
    //    Gizmos.color = Color.yellow;
    //    Gizmos.DrawSphere(transform.position, _sphereSize);
    //}
}
