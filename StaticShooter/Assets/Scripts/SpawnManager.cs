using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab, _enemyContainer;
    [SerializeField] private GameObject _powerupContainer, _tripleLaserPowerupPrefab, _speedPowerupPrefab, _shieldPowerup;
    private Dictionary<string, GameObject> powerupDictionary = new Dictionary<string, GameObject>();
    [SerializeField] private string[] powerupNames;
    [SerializeField] private float _enemySpawnTime = 5f, _powerupSpawnTime = 7f;
    private float X, Y;
    private bool _isPlayerAlive = true;
    // Start is called before the first frame update
    void Start()
    {
        X = 11f;
        Y = 7f;
        powerupDictionary.Add("TripleLaser", _tripleLaserPowerupPrefab);
        powerupDictionary.Add("Speed", _speedPowerupPrefab);
        powerupDictionary.Add("Shield", _shieldPowerup);
        StartCoroutine(spawnEnemyRoutine(_enemySpawnTime));
        StartCoroutine(spawnPowerupRoutine(_powerupSpawnTime));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator spawnPowerupRoutine(float time) {
        while(_isPlayerAlive) {
            float x = Random.Range(-(X - 2), (X - 2));
            float y = Random.Range(-(Y - 2), (Y - 2));
            Vector3 spawnPosition = new Vector3(x, y, 0);
            int randomInt = (int)Random.Range(0, powerupNames.Length);
            GameObject newPowerup = null;

            newPowerup = Instantiate(powerupDictionary[powerupNames[randomInt]], spawnPosition, Quaternion.identity);

            if(newPowerup != null) newPowerup.transform.SetParent(_powerupContainer.transform);
            yield return new WaitForSeconds(time);
        }
    }   
    IEnumerator spawnEnemyRoutine(float time) {
        while(_isPlayerAlive) {
            float x, y;
            if(Random.Range(0f, 1f) > 0.5) {
                x = X;
            } else {
                x = -X;
            }            
            
            if(Random.Range(0f, 1f) > 0.5) {
                y = Y;
            } else {
                y = -Y;
            }
            GameObject newEnemy = Instantiate(_enemyPrefab, new Vector3(x, y, 0), Quaternion.identity);
            newEnemy.transform.SetParent(_enemyContainer.transform);
            yield return new WaitForSeconds(time);
        }
    }

    public void OnPlayerDeath() {
        _isPlayerAlive = false;
    }
}
