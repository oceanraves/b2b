using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatEnemy : MonoBehaviour
{
    private LookForPickup _lookForPickup;
    private ObjectPickUp _objectPickup;
    private GameObject _enemy;
    private EnemyAI_Cop _enemyAI;
    private PlayerHealth _playerHealth;
    void Start()
    {
        _lookForPickup = gameObject.GetComponent<LookForPickup>();
        _playerHealth = gameObject.GetComponent<PlayerHealth>();
    }

    public void Eat()
    {
        _enemy = _lookForPickup._object;
        _objectPickup = _enemy.GetComponent<ObjectPickUp>();
        _enemyAI = _enemy.GetComponent<EnemyAI_Cop>();

        _objectPickup.pickedUp = false;
        _lookForPickup._pickedUp = false;
        _enemyAI.SetFlyProperties();

        _playerHealth.AddHealth();
        _enemyAI.LayDown();

        Invoke("DestroyEnemy", 3f);
    }

    private void DestroyEnemy()
    {
        Destroy(_enemy);
    }
}
