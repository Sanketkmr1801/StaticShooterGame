using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreText, _gameOverText, _restartText;
    [SerializeField] private Scrollbar _healthBar; 
    [SerializeField] GameManager _gameManager;
    [SerializeField] DatabaseManager _databaseManager;
    private string _baseScoreText = "Score: ";
    void Start()
    {
        _gameOverText.enabled = false;
        _restartText.enabled = false;
        if(Cache.Contains("GameManager")) {
            _gameManager = Cache.Get("GameManager").GetComponent<GameManager>();
        } else {
            _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
            Cache.Put("GameManager", _gameManager.transform);
        }
        if(Cache.Contains("DatabaseManager")) {
            _databaseManager = Cache.Get("DatabaseManager").GetComponent<DatabaseManager>();
        } else {
            _databaseManager = GameObject.Find("DatabaseManager").GetComponent<DatabaseManager>();
            Cache.Put("DatabaseManager", _databaseManager.transform);
        }

    }

    void Update()
    {

    }

    public void gameOver() {
        _gameOverText.enabled = true;
        _restartText.enabled = true;
        StartCoroutine(flickeringText(_gameOverText, 0.1f));
        _databaseManager.displayScores();
        _gameManager.gameOver();
    }
    IEnumerator flickeringText(TextMeshProUGUI text, float flickerDuration) {
        while(true) {
            _gameOverText.enabled = false;
            yield return new WaitForSeconds(flickerDuration);
            _gameOverText.enabled = true;
            yield return new WaitForSeconds(flickerDuration);
        }
    }

    public void healthSystem(float health, float maxHealth) {
        _healthBar.size = health / maxHealth;
    }
    public void scoreSystem(float score) {
        _scoreText.text = _baseScoreText + score.ToString();
    }
}
