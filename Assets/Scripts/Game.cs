using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] Player _player;
    [SerializeField] private StartScreen _startScreen;
    [SerializeField] private GameOverScreen _gameOverScreen;

    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
        
    }

    private void OnPlayButtonClick()
    {

    }

    private void OnRestartButtonClick()
    {
        _gameOverScreen.Close();
    }
}
