using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerImpact : MonoBehaviour
{
    private CinemachineImpulseSource source;

    void Awake()
    {
        source = GetComponent<CinemachineImpulseSource>();
    }

    public void CameraShake()
    {
        source.GenerateImpulse();
    }
}
