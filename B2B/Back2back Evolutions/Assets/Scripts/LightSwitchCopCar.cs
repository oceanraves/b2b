using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitchCopCar : MonoBehaviour
{
    // 5 & 6

    private Renderer _renderer;
    Material[] _allMats;

    Material _redMat;
    Material _blueMat;

    Color _ogColorRed;
    Color _ogColorBlue;

    Color brightBlue = new Color(38, 21, 255);

    //Color blue = Color.blue;
    //Color red = Color.red;

    private Light _red;
    private Light _blue;

    //private float _counter = 0;

    public float switchRate = 0.1f;
    void Start()
    {
        //_renderer = gameObject.GetComponent<MeshRenderer>();
        //_allMats = _renderer.materials;
        //_redMat = _allMats[4];
        //_blueMat = _allMats[5];
        //_ogColorRed = _allMats[4].color;
        //_ogColorBlue = _allMats[5].color;

        _red = gameObject.transform.Find("Red").GetComponent<Light>();
        _blue = gameObject.transform.Find("Blue").GetComponent<Light>();
        _red.intensity = 20;
        _blue.intensity = 0;
    }

    bool switcher = false;

    void Update()
    {
        if (_red.intensity > 0 && switcher == false)
        {
            _red.intensity -= switchRate;
            _blue.intensity += switchRate;
        }
        else if (_red.intensity <= 0)
        {
            switcher = true;
        }

        if (_blue.intensity > 0 && switcher == true)
        {
            _red.intensity += switchRate;
            _blue.intensity -= switchRate;
        }
        else if (_blue.intensity <= 0)
        {
            switcher = false;
        }

        //BlueFlick();
        //RedFlick();
    }

    //private void BlueFlick()
    //{
    //    float emission = Mathf.PingPong(Time.time, 1f);
    //    Color finalColor = blue * Mathf.LinearToGammaSpace(emission);
    //    _blueMat.SetColor("_EmissionColor", finalColor);
    //}
    //private void RedFlick()
    //{
    //    float emission = Mathf.PingPong(Time.time, 1f);
    //    Color finalColor = red * Mathf.LinearToGammaSpace(emission);
    //    _redMat.SetColor("_EmissionColor", (finalColor * -0.5f));
    //}
}
