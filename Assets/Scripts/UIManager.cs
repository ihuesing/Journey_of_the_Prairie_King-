using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private int _coins = 0;
    private int _score = 0;
    
    public static int lives = 3;

    [SerializeField]
    private TextMeshProUGUI _coinText;
    
    [SerializeField]
    private TextMeshProUGUI _lifeText;
    
    [SerializeField]
    private TextMeshProUGUI _scoreText;
    
    [SerializeField]
    private TextMeshProUGUI _gameOverText;

    

    private void Start()
    {
        _coinText.text = "Coins:" + _coins;
        _lifeText.text = "Life:" + lives;
        _scoreText.text = "Score:" + _score;
        _gameOverText.text = " ";
    }

    private void Update()
    {
        _score = _coins;
        _scoreText.text = "Score:" + _score;
    }

    public void AddCoins(int coins)
    {
        _coins = _coins + coins;
        _coinText.text = "Coins:" + _coins;
    }

    public void AddLife(int life)
    {
        lives = lives + life;
    
        _lifeText.text = "Life:" + lives;
    }
    
    
    public void GameOver()
    {
        _gameOverText.text = "Game Over";
    }
    
}
