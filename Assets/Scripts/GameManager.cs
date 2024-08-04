using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private bool _isGameOver = false;

    private void Update()
    {
        // if the R key was pressed: restart game
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (_isGameOver)
            {
                SceneManager.LoadScene(1);
            }
        }
    }

    public void GameOverFunctionality()
    {
        _isGameOver = true;
    }
}
