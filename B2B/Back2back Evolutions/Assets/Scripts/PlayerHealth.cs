using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField]
    private float _maxHealth = 100;
    [SerializeField]
    private float _playerHealth;

    private GameObject _healthBarUI;
    private HealthBar _healthBar;
    private HealthBar _healthUpBar;

    int number = 1;

    private bool _hasDied = false;

    [SerializeField]
    private float _staggeredTime;

    private PlayerAnimation _pAnimation;

    public bool invincible;

    public int healthUp = 10;
    float healthBeforeHeal;
    float currentHealth;
    private bool _healing = false;

    private Color _red = Color.red;
    private Color _ogColor;
    private Color _currentColor;

    //private HealthBar _healthBar;
    void Start()
    {
        //_playerHealth = _maxHealth;
        _healthBarUI = GameObject.Find("Canvas").gameObject.transform.Find("Healthbar").gameObject;
        _healthBar = _healthBarUI.GetComponent<HealthBar>();

        _ogColor = _healthBarUI.transform.Find("Fill").GetComponent<Image>().color;
        _currentColor = _ogColor;
        _healthBarUI.transform.Find("Fill").GetComponent<Image>().color = _currentColor;



        _healthBar.SetMaxHealth(_maxHealth);
        _healthBar.SetHealth(_playerHealth);
        _healthUpBar = _healthBar.gameObject.transform.Find("Restore").gameObject.GetComponent<HealthBar>();
        _healthUpBar.SetMaxHealth(_maxHealth);
        _healthUpBar.SetHealth(_playerHealth);


        _pAnimation = gameObject.GetComponent<PlayerAnimation>();
    }

    void Update()
    {


        if (_healing)
        {
            if (currentHealth < _playerHealth)
            {
                currentHealth += 0.4f;
                _healthBar.SetHealth(currentHealth);

                if (_currentColor.r < 1 && _currentColor.b < 1)
                {
                    _currentColor.r += 0.01f;
                    _currentColor.b += 0.01f;

                    _healthBarUI.transform.Find("Fill").GetComponent<Image>().color += new Color(_currentColor.r, 0, _currentColor.b, 0);
                }
            }

            if (currentHealth > _playerHealth)
            {
                currentHealth -= 0.1f;
                _healthUpBar.SetHealth(currentHealth);

                if(_currentColor.g < 1 && _currentColor.b < 1)
                {
                    _currentColor.g += 0.01f;
                    _currentColor.b += 0.01f;

                    _healthBarUI.transform.Find("Fill").GetComponent<Image>().color += new Color(0, _currentColor.g, _currentColor.b, 0);
                }
            }
            else if (currentHealth == _playerHealth)
            {
                _healing = false;
            }
        }
    }

    public void HitByBullet(string enemyType)
    {
        if(enemyType == "Cop")
        {
            if (!_hasDied && !invincible)
            {
                SubtractHealth();

                gameObject.GetComponent<PlayerController>().canMove = false;

                if(_playerHealth > 0)
                {
                    //_pAnimation.Animate(true, "Player_Shot");
                    Invoke("UnStagger", _staggeredTime);
                }
            }
            else
            {
                //SHOT WHILE DEAD ANIMATION
            }
        }
    }

    private void SubtractHealth()
    {
        currentHealth = _playerHealth;
        _playerHealth -= 5;

        if (_playerHealth <= 0 && !_hasDied)
        {
            GameOver();
        }

        _currentColor = _red;
        _healthBarUI.transform.Find("Fill").GetComponent<Image>().color = _currentColor;

        _healthBar.SetHealth(_playerHealth);
        _healing = true;
    }

    public void AddHealth()
    {
        if(_playerHealth < _maxHealth)
        {
            currentHealth = _playerHealth;
            _playerHealth += healthUp;

            _currentColor = new Color((131 / 255), (255 / 255), (81 / 255), (255 / 255));
            _healthBarUI.transform.Find("Fill").GetComponent<Image>().color = _currentColor;

            if (_playerHealth > 100)
            { _playerHealth = 100; }

            _healthUpBar.SetHealth(_playerHealth);
            _healing = true;
        }
    }

    private void GameOver()
    {
        _hasDied = true;
        gameObject.GetComponent<PlayerController>().canMove = false;
        _pAnimation.Animate(true, "Dying");

        GameObject.Find("Canvas").gameObject.transform.Find("GameOver").gameObject.SetActive(true);
        Debug.Log("Player is dead");
    }

    private void UnStagger()
    {
        gameObject.GetComponent<PlayerController>().canMove = true;
    }
}
