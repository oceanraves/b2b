using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    private Image _blackScreen;
    private float _alpha = 0;

    void Start()
    {
        _blackScreen = gameObject.transform.GetChild(0).GetComponent<Image>();
    }

    void Update()
    {

        if (_alpha >= 0.8)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
        }
        if (_alpha < 1)
        {
            _alpha += 0.005f;
            _blackScreen.color = new Color(0, 0, 0, _alpha);
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
