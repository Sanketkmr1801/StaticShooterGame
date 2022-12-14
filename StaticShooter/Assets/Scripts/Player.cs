using System.Collections;
using System.Text;
using UnityEngine.Networking;
using UnityEngine;
using System;

public class Player : MonoBehaviour
{
    [SerializeField] private float _speed = 10.0f, _fireRate = 0.1f;
    private float  _nextFire = 0, _health, _maxHealth = 100f, _shieldDamageAbsorbMultiplier = 0, _score = 0;
    public float horizontalInput, verticalInput;
    [SerializeField] private GameObject _laserPrefab, _tripleLaserPrefab;
    private UIManager _uiManager;
    private SpawnManager _spawnManager;
    private bool _isTripleLaserActive = false, _isSpeedActive = false, _isShieldActive = false;
    private DatabaseManager _databaseManager;
    void Start()
    {


        _health = _maxHealth;
        transform.position = new Vector3(0, 0, 0);
        if(Cache.Contains("SpawnManager")) {
            _spawnManager = Cache.Get("SpawnManager").GetComponent<SpawnManager>();
        } else {
            _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
            Cache.Put("SpawnManager", _spawnManager.transform);
        }
        if(Cache.Contains("UIManager")) {
            _uiManager = Cache.Get("UIManager").GetComponent<UIManager>();
        } else {
            _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
            Cache.Put("UIManager", _uiManager.transform);
        }
        if(Cache.Contains("DatabaseManager")) {
            _databaseManager = Cache.Get("DatabaseManager").GetComponent<DatabaseManager>();
        } else {
            _databaseManager = GameObject.Find("DatabaseManager").GetComponent<DatabaseManager>();
            Cache.Put("DatabaseManager", _databaseManager.transform);
        }

        //ui startups
        _uiManager.healthSystem(_health, _maxHealth);

    }

    void Update()
    {
        move();
        fireLaser();

    }

    public void addScore(float score) {
        _score += score;
        _uiManager.scoreSystem(_score);
    }

    public float getScore() {
        return _score;
    }
    public void activateShield(float time, float damageaAsorbMultiplier) {
        _isShieldActive = true;
        _shieldDamageAbsorbMultiplier = damageaAsorbMultiplier;
        Debug.Log("Shield Active!");
        StartCoroutine(shieldDeactivationRoutine(time));
    }

    IEnumerator shieldDeactivationRoutine(float time) {
        yield return new WaitForSeconds(time);
        _isShieldActive = false;
        _shieldDamageAbsorbMultiplier = 0;
    }

    public void damagePlayer(float damage) {
        damage = damage * (1 - _shieldDamageAbsorbMultiplier);
        if(damage < _health) {
            _health -= damage;
        } else {
            _health = 0f;
            if(_spawnManager != null) {
                _spawnManager.OnPlayerDeath();
            }
            _databaseManager.addScore("anonymus", _score);
            _uiManager.gameOver();
        }  

        _uiManager.healthSystem(_health, _maxHealth);
        Debug.Log(_health);
    }
    void fireLaser() {
        if(Input.GetMouseButton(0) && Time.time > _nextFire) {   
            //Get the mouse position
            //Offset it by difference between object and camera z
            //Get mouse world position
            Vector3 mouseWorldPosition = getMouseWorldPosition(this.gameObject);
            
            GameObject newLaser;

            if(_isTripleLaserActive) {
                newLaser = Instantiate(_tripleLaserPrefab, transform.position, transform.rotation);
            } else {
                newLaser = Instantiate(_laserPrefab, transform.position, transform.rotation);
            }
            //Get angle between the x axis of object and direction between the object and mouse world position
            if(newLaser != null) {
                float angle = Vector3.SignedAngle(new Vector3(0, Math.Abs(transform.position.y), 0), mouseWorldPosition - transform.position, new Vector3(0, 0, 1));
                newLaser.transform.Rotate(new Vector3(0, 0, angle));
            } 
            //cool down system for laser
            _nextFire = Time.time + _fireRate;
        }
    }
    Vector3 getMouseWorldPosition(GameObject gameObject) {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = gameObject.transform.position.z - Camera.main.transform.position.z;
            return Camera.main.ScreenToWorldPoint(mousePos);
    }

    void move() {
        verticalInput = Input.GetAxis("Vertical");
        horizontalInput = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.right * Time.deltaTime * _speed * horizontalInput);
        transform.Translate(new Vector3(0, 1, 0) * Time.deltaTime * _speed * verticalInput);

        if(transform.position.y > 7f) {
            transform.position = new Vector3(transform.position.x, -7f, transform.position.z);
        }
        if(transform.position.y < -7f) {
            transform.position = new Vector3(transform.position.x, 7f, transform.position.z);
        }
        if(transform.position.x > 11f) {
            transform.position = new Vector3(-11f, transform.position.y, transform.position.z);
        }
        if(transform.position.x < -11f) {
            transform.position = new Vector3(11f, transform.position.y, transform.position.z);
        }
    }

    public void activateSpeed(float duration, float multiplier) {
        _isSpeedActive = true;
        StartCoroutine(speedDeactivationRoutine(duration, multiplier, _speed));
    }

    IEnumerator speedDeactivationRoutine(float duration, float multiplier, float initialSpeed) {
        _speed *= multiplier;
        yield return new WaitForSeconds(duration);
        _speed = initialSpeed;
        _isSpeedActive = false;
    }

    public void activateTripleLaser(float duration) {
        _isTripleLaserActive = true;
        StartCoroutine(tripleLaserDeactivateRoutine(duration));
    }

    IEnumerator tripleLaserDeactivateRoutine(float duration) {
        yield return new WaitForSeconds(duration);
        _isTripleLaserActive = false;
    }

    public bool isTripleLaserActive() {
        return _isTripleLaserActive;
    }

    public bool isSpeedActive() {
        return _isSpeedActive;
    }

    public bool isShieldActive() {
        return _isShieldActive;
    }

}
