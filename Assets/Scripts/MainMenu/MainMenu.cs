using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{

    // New game button 
    [SerializeField]
    private Button _newGameButton;

    public void LoadGame()
    {
        SceneManager.LoadScene(1);
    }
}
