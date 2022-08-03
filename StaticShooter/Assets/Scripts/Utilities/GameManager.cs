using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private bool _isGameOver = false;
    void Start()
    {
        
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R) && _isGameOver == true) {
            SceneManager.LoadScene(0); // Game Scene
            _isGameOver = false;
        }
    }

    public void gameOver() {
        _isGameOver = true;
        Cache.resetCache();
    }
}
