using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreText, _gameOverText;
    [SerializeField] private Scrollbar _healthBar; 
    private string _baseScoreText = "Score: ";
    void Start()
    {
        _gameOverText.enabled = false;

    }

    void Update()
    {

    }

    public void gameOver() {
        _gameOverText.enabled = true;
        StartCoroutine(flickeringText(_gameOverText, 0.1f));
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
