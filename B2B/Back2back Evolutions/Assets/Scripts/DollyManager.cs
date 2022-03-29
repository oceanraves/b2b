using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class DollyManager : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera vCam0;
    [SerializeField] CinemachineVirtualCamera vCam1;
    [SerializeField] CinemachineVirtualCamera vCam2;

    private bool _firstCamera = false;

    [SerializeField]
    GameObject dollyCameras;

    void Start()
    {

        //string name = "Camera #";
        //int number = 0;
        //foreach (GameObject camera in cameras)
        //{
        //    camera.name = name + number.ToString();
        //    Debug.Log(camera.name);
        //    number++;
        //}
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            SwitchPriority();
        }
    }

    public void IntroIsFinished()
    {
        vCam0.Priority = 0;
        SwitchPriority();
    }

    private void SwitchPriority()
    {
        if (_firstCamera)
        {
            vCam1.Priority = 0;
            vCam2.Priority = 1;
        }
        else
        {
            vCam1.Priority = 1;
            vCam2.Priority = 0;
        }
        Invoke("DelayTrigger", 0.1f);
    }

    private void DelayTrigger()
    {
        _firstCamera = !_firstCamera;
    }
}
