using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionCar : MonoBehaviour
{
    float minVelocity = 1f;
    float maxVelocity = 11f;

    void Start()
    {
        gameObject.GetComponent<Rigidbody>().velocity =
            Random.onUnitSphere * Random.Range(minVelocity, maxVelocity);
        float x = Random.Range(-360, 360);
        float y = Random.Range(-360, 360);
        float z = Random.Range(-360, 360);

        gameObject.transform.rotation = new Quaternion(x, y, z, x);
        Destroy(gameObject.transform.parent.gameObject, 2f);
    }
}
