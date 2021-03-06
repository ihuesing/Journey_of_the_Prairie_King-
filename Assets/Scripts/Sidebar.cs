using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Sidebar : MonoBehaviour
{

    [SerializeField]
    private GameObject _scoreCounter;
    
    [SerializeField]
    private GameObject _livesCounter;

    [SerializeField] 
    private GameObject _gameOver;
    
    [SerializeField] 
    private Player _player;

    [SerializeField] 
    private GameObject _congrats;

    [SerializeField] 
    private GameObject _playAgain;

    [SerializeField] 
    private List<GameObject> _winnerList;

    [SerializeField] 
    private GameObject _move;

    [SerializeField] 
    private GameObject _shoot;


    public int lives = 3;

    public int coins = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        _gameOver.gameObject.SetActive(false);
        _congrats.gameObject.SetActive(false);
        //At the beginning of the scene every Element of the WinnerTextList will be set as inactive
        foreach(GameObject winnerText in _winnerList)
        {
            winnerText.gameObject.SetActive(false);
        }
        _playAgain.gameObject.SetActive(false);
        _move.gameObject.SetActive(true);
        _shoot.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        _scoreCounter.GetComponent<TextMeshPro>().text = _player.score.ToString();
        _livesCounter.transform.GetComponent<TextMeshPro>().text = "x " + lives;
        
        //check for input from movement or shooting keys
        float horizontalMoveInput = Input.GetAxis("Horizontal");
        float verticalMoveInput = Input.GetAxis(("Vertical"));
        float horizontalShootInput = Input.GetAxis("Fire1");
        float verticalShootInput = Input.GetAxis("Fire2");
        
        if (horizontalMoveInput != 0 || verticalMoveInput != 0)
        {
            _move.gameObject.SetActive(false);
        }

        if (horizontalShootInput != 0 || verticalShootInput != 0)
        {
            _shoot.gameObject.SetActive(false);
        }
    }
    
    public void AddLife(int life)
    {
        lives = lives + life;
    }
    
    public void AddCoins(int newCoins)
    {
        coins = coins + newCoins;
    }
    
    public IEnumerator GameOver()
    {
        _gameOver.gameObject.SetActive(true);
        yield return new WaitForSeconds(4);
        _gameOver.gameObject.SetActive(false);
    }
    
    public IEnumerator WinnerText()
    {
        _congrats.gameObject.SetActive(true);
        //a random Text from the list will be chosen and activated
        int element = Random.Range(0, _winnerList.Count);
        _winnerList[element].gameObject.SetActive(true);
        yield return new WaitForSeconds(6);
        _congrats.gameObject.SetActive(false);
        _winnerList[element].gameObject.SetActive(false);

    }
    public IEnumerator RestartScene()
    {
        // the game will restart if the space bar is pressed
        yield return new WaitForSeconds(7);
        while (!Input.GetKeyDown(KeyCode.Space))
        {
            _move.gameObject.SetActive(false);
            _shoot.gameObject.SetActive(false);
            _playAgain.gameObject.SetActive(true);
            yield return null;
        }
        SceneManager.LoadScene("JotPK");
    }
}
