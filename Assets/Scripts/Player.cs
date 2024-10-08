using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Player : MonoBehaviour
{   
    // Shooting vars
    [SerializeField]
    private float _fireRate = 0.15f;
    private float _canFire = -1f;
    [SerializeField]
    private GameObject _laser;

    // Player
    [SerializeField]
    private int _lives = 3;
    [SerializeField]
    private float _speed = 5f;

    // Score
    [SerializeField]
    private int _score;

    //Spawner
    private SpawnManager _spawnManager;

    // Powerups
    //Triple shot
    private bool _tripleShotActive = false;
    [SerializeField]
    private GameObject _tripleShot;

    // Speed
    private int _speedMultiplier = 2;

    // Shield
    private bool _isShieldActive = false;
    [SerializeField]
    private GameObject _shield;

    // UI and Game managers
    private UIManager _uiManager;
    private GameManager _gameManager;

    // Damage Engines
    [SerializeField]
    private GameObject _leftEngine;
    [SerializeField]
    private GameObject _rightEngine;


    // Start is called before the first frame update
    void Start()
    {
        // Take the current position and assign a starting position
        transform.position = new Vector3(0, 0, 0);
        //find the object, get the component
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();

        if (_spawnManager == null)
        {
            Debug.LogError("Error: Spawn manager is NULL");
        }

        // ui manager initialization
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        if (_uiManager == null)
        {
            Debug.Log("UI manager is NULL");
        }
    }



    // Update is called once per frame
    void Update()
    {
        CalculateMovement();

        //on space key
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
        {
            FireLaser();
        }

        if (_isShieldActive)
        {
            _shield.SetActive(true);
        } else
        {
            _shield.SetActive(false);
        }
    }

    void FireLaser()
    {
        // fire rate timeout
        _canFire = Time.time + _fireRate;


        if (_tripleShotActive == false)
        {
            // Instantiates the laser
            // one shot
            Instantiate(_laser, new Vector3(transform.position.x, transform.position.y + 1.05f, 0), Quaternion.identity);
        } // fire three
        else if (_tripleShotActive == true)
        {
            Instantiate(_tripleShot, new Vector3(transform.position.x - 1.0f, transform.position.y + 1.0f, 0), Quaternion.identity);
        }
    }

    void CalculateMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);

        transform.Translate(direction * _speed * Time.deltaTime);

        if (transform.position.y >= 1.5f)
        {
            transform.position = new Vector3(transform.position.x, 1.5f, 0);
        }
        else if (transform.position.x >= 11.3f)
        {
            transform.position = new Vector3(-11.3f, transform.position.y, 0);
        }
        else if (transform.position.y <= -1.6f)
        {
            transform.position = new Vector3(transform.position.x, -1.6f, 0);
        }
        else if (transform.position.x < -11.3f)
        {
            transform.position = new Vector3(11.3f, transform.position.y, 0);
        }
    }

    public void Damage()
    {
        if (_isShieldActive == false)
        {
            _lives -= 1;
        }


        _uiManager.UpdateLives(_lives);
        _gameManager.GameOverFunctionality();

        Debug.Log("Lives: " + _lives);

        if (_lives < 1)
        {
            Destroy(this.gameObject);

            // set player to dead in spawnmanager
            _spawnManager.OnPlayerDeath();

            _uiManager.GameOver();
        }

        if (_lives > 1)
        {
            int engine = UnityEngine.Random.Range(1, 2);

            if (engine == 1)
            {
                StartCoroutine(LeftEngineAnim());
            }
            else if (engine == 2)
            {
                StartCoroutine(RightEngineAnim());
            } else
            {
                Debug.Log("Something wrong: " + engine);
            }
        }
    }

    IEnumerator LeftEngineAnim()
    {
        _leftEngine.SetActive(true);
        yield return new WaitForSeconds(1f);
        _leftEngine.SetActive(false);
    }

    IEnumerator RightEngineAnim() {
        _rightEngine.SetActive(true);
        yield return new WaitForSeconds(1f);
        _rightEngine.SetActive(false);
    }

    public void TripleShotActive()
    {
        // tripleshotactive becomes true
        _tripleShotActive = true;
        // starts coroutine
        StartCoroutine(TripleShotPowerDown());
    }

    public void SpeedActive()
    {
        _speed *= _speedMultiplier;
        StartCoroutine(SetSpeed());
    }

    public void ShieldActive()
    {
        _isShieldActive = true;
        _shield.SetActive(true);
        StartCoroutine(SetShieldTimeout());
    }

    IEnumerator SetShieldTimeout()
    {
        yield return new WaitForSeconds(5);
        _isShieldActive = false;
    }

    IEnumerator SetSpeed()
    {
        yield return new WaitForSeconds(5);
        _speed /= _speedMultiplier;
    }

    IEnumerator TripleShotPowerDown()
    {
        yield return new WaitForSeconds(5);
        _tripleShotActive = false;       
    }

    public void AddScore(int points)
    {
        _score += points;
        
        _uiManager.UpdateScore(_score);
    }

    
}
