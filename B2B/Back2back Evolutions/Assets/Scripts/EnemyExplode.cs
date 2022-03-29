using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyExplode : MonoBehaviour
{
    [SerializeField]
    List<GameObject> bodyParts;

    [SerializeField]
    float explosionForce;

    Vector3 explosionCenter;

    [SerializeField]
    float explosionRadius;

    [SerializeField]
    GameObject bloodObject;

    private void Start()
    {
        explosionCenter = transform.GetChild(0).transform.position;
        GameObject blood = Resources.Load("VFX_BloodExplosion") as GameObject;
        Instantiate(blood, explosionCenter, Quaternion.identity);         
        Explode();
    }

    private void Explode()
    {
        foreach (GameObject part in bodyParts)
        {
            Rigidbody rb = part.GetComponent<Rigidbody>();
            rb.isKinematic = false;
            rb.useGravity = true;
            rb.AddExplosionForce(explosionForce, explosionCenter, explosionRadius, 3f, ForceMode.Impulse);
            rb.AddForce(new Vector3((Random.Range(-20, 20)), (Random.Range(-20, 20)), (Random.Range(-20, 20))), ForceMode.Impulse);
            rb.AddTorque(new Vector3(200f, 100f, 300f));  
        }
        Destroy(gameObject, 4f);
    }
}
