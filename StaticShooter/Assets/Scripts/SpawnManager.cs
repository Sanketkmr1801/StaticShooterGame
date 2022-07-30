using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab, _enemyContainer;
    [SerializeField] private float _enemySpawnTime = 5f;
    private bool _isPlayerAlive = true;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(spawnEnemy(_enemySpawnTime));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator spawnEnemy(float time) {
        while(_isPlayerAlive) {
            float x, y;
            if(Random.Range(0f, 1f) > 0.5) {
                x = 11f;
            } else {
                x = -11f;
            }            
            
            if(Random.Range(0f, 1f) > 0.5) {
                y = 7f;
            } else {
                y = -7f;
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
