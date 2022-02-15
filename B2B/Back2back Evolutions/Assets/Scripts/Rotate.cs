using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    public bool z;
    public bool y;

    public float rotateSpeed;

    void Update()
    {
        if (z)
        {
            this.transform.Rotate(new Vector3(0, 0, rotateSpeed), Space.Self);
        }
        if (y)
        {
            this.transform.Rotate(new Vector3(0, rotateSpeed, 0), Space.Self);

        }
    }
}
