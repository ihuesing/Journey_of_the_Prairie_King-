using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public int _coins = 0;
    private int _score = 0;
    
    public static int lives = 3;

    
    [SerializeField]
    private TextMeshProUGUI _lifeText;
    
    [SerializeField]
    private TextMeshProUGUI _scoreText;
    
    [SerializeField]
    private TextMeshProUGUI _gameOverText;

    [SerializeField] 
    private Player _player;

    [SerializeField]
    private List<Text> WinnerTextList;
    

    private void Start()
    {
        
        _lifeText.text = "Lives:" + lives;
        _scoreText.text = "Score:" + _score;
        _gameOverText.text = " ";
        //At the beginning of the scene every Element of the WinnerTextList will be set as inactive
        foreach(Text winnerText in WinnerTextList)
        {
            winnerText.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        _score = _player.score;
        _scoreText.text = "Score:" + _score;
    }

    public void AddCoins(int coins)
    {
        _coins = _coins + coins;
    }

    public void AddLife(int life)
    {
        lives = lives + life;
        _lifeText.text = "Lives:" + lives;
    }
    
    
    public IEnumerator GameOver()
    {
        _gameOverText.text = "Game Over";
        yield return new WaitForSeconds(4);
        _gameOverText.text = " ";
    }
    public IEnumerator WinnerText()
    {
        //a random Text from the list will be chosen and activated
        int element = UnityEngine.Random.Range(0, WinnerTextList.Count);
        WinnerTextList[element].gameObject.SetActive(true);
        yield return new WaitForSeconds(4);
        WinnerTextList[element].gameObject.SetActive(false);
    }
}
