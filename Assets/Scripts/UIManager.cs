using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _scoreText;
    private int playerScore;

    // Lives array
    [SerializeField]
    private Sprite[] _liveSprites;

    // Lives Image Container
    [SerializeField]
    private Image _liveImage;

    // Game Over Text
    [SerializeField]
    private TextMeshProUGUI _gameOverText;

    // Restart level text
    [SerializeField]
    private TextMeshProUGUI _restartText;
    

    // Update is called once per frame
    void Update()
    {
        _scoreText.text = "Score: " + playerScore.ToString();
    }

    public void UpdateScore(int score)
    {
        playerScore = score;
    }

    public void UpdateLives(int currentLives)
    {
        // display img sprite

        // give it a new one based on the current lives index

        _liveImage.sprite = _liveSprites[currentLives];
    }

    public void GameOver()
    {
        _restartText.gameObject.SetActive(true);

        StartCoroutine(GameOverAnim());
    }

    IEnumerator GameOverAnim()
    {
        bool resetGame = false;

        while (resetGame == false)
        {
            _gameOverText.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            _gameOverText.gameObject.SetActive(false);
            yield return new WaitForSeconds(0.5f);
        }
    }
}
