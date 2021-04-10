using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //access to all the needed gameobjects
    [SerializeField]
    private GameObject _bulletPrefab;

    private GameObject _wallHit;
    
    [SerializeField]
    private GameObject _arrow;
    private GameObject clone;

    [SerializeField]
    private Bullets _bullet;

    //access to all the needed classes
    [SerializeField]
    private SpawnManager _spawnmanager;

    [SerializeField]
    private WallManager _wallManager;
    
    [SerializeField]
    private UIManager _uiManager;

    //needed variables
    //speed of the Player
    [SerializeField]
    private float _speed = 7;
    
    //int to count all the lives of the Player
    //[SerializeField]
    //public int _lives = 4;
    
    [SerializeField]
    private float _shootingRate = 0.4f;
    private float _timeToShoot = 0;

    //int to count all the collected coins
    //private int coins = 0;

    //int to count all the destroyed Enemies
    public int enemiesCount;

    private int _levelCount;

    private int _nextLevelCount;

    // bool to control if shotgun is active
    private bool _shotgun = false;

    //bool to to see in which level the player is
    public bool level_0 = true;
    public bool level_1 = false;
    public bool level_2 = false;
    public bool level_3 = false;
    public bool nextLevel = false;

    private bool _theGo = false;

    //bool to control the collision between Player and Wall
    private bool _hitWall = false;

    [SerializeField] 
    private HighscoreTable _highscoreTable;

    // count the score
    public int score;


    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0, 0, 0);
        //the Arrow is only active when the next Level is activated
;       _arrow.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (nextLevel == true && level_3 == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(0, 0, 0), 2 * Time.deltaTime);
            _hitWall = false;
        }
        else if (_hitWall)
        {
            //The Player moves away from the wall when he hits it
            transform.position = Vector3.MoveTowards(transform.position, _wallHit.gameObject.transform.position, -2f * Time.deltaTime);
        }
        else
        {
            PlayerMovement();
        }
        Shooting();
        //if the next level is activated and all Enemies are destroyed, the Arrow gets activated
        //in the last Level it will start the coroutine
        if (_spawnmanager.transform.childCount == 0 && _theGo == true)
        {
            nextLevel = true;
            if(level_3 == false)
            {
                _arrow.SetActive(true);
            }
            else if(_theGo == true)
            {
                StartCoroutine(Won());
            }
        }

        // calculate the score
        score = (enemiesCount * 20) + (_uiManager._coins * 10) + _nextLevelCount;
    }

    void PlayerMovement()
    {
        //turn the key input (a,w,s,e) into -1 or 1
        float horizontalMoveInput = Input.GetAxis("Horizontal");
        float verticalMoveInput = Input.GetAxis(("Vertical"));

        //Player moves depending on the pressed key
        transform.Translate(new Vector3(horizontalMoveInput, verticalMoveInput, 0f) * _speed * Time.deltaTime);

        TransitionNextLevel();
    }

    void Shooting()
    {
        //turns the key input (up, down, right, left) to 1 or -1
        float horizontalShootInput = Input.GetAxis("Fire1");
        float verticalShootInput = Input.GetAxis("Fire2");

        //as lang as a key is pressed and enough time has passed after the previous bullet, the bullets will shoot in the direction of pressed key
        if ((horizontalShootInput != 0f || verticalShootInput != 0f) && Time.time > _timeToShoot)
        {
            //access the script of the bullet and sets the vector for the direction of the movement to the direction of the presssed key
            _bullet._shootingDirection = new Vector3(horizontalShootInput, verticalShootInput, 0f);
            Instantiate(_bulletPrefab, transform.position, Quaternion.identity);
            
            // if the shotgun power up is active, 2 additional bullets will be instantiated with different angles
            if (_shotgun)
            {
                Instantiate(_bulletPrefab, transform.position, Quaternion.AngleAxis(30, Vector3.forward));
                Instantiate(_bulletPrefab, transform.position, Quaternion.AngleAxis(30, Vector3.back));
            }
            
            _timeToShoot = Time.time + _shootingRate;
        }

    }
    public void Count()
    {
        //counts the Enemies the Player has destroyed
        enemiesCount++;
        _levelCount++;
        //if the count exceeds a given limit the spawning of new Enemies will be stopped
        NextLevel();
    }
    
    public void Damage()
    {
        //if an Enemy hits the Player, the Player will lose a live
        _uiManager.AddLife(-1);

        //if the Player has no lives left the Player and the remaining Enemies will be destroyed
        if (UIManager.lives < 0)
        {
            //the spawning of new Enemies will be stopped
            _spawnmanager.StopSpawning();
            foreach (Transform child in _spawnmanager.transform)
            {
                Destroy(child.gameObject);
            }
            Destroy(this.gameObject);
            _uiManager.StartCoroutine(_uiManager.GameOver());

            _uiManager.StartCoroutine(_highscoreTable.MakeScoreboard(score));
        }
    }
    
    IEnumerator EnableSpeedBonus(int speed)
    {
        _speed = speed;
        yield return new WaitForSeconds(10);
        _speed = 7;
    }
    
    IEnumerator EnableShootBonus()
    {
        _shotgun = true;
        yield return new WaitForSecondsRealtime(10);
        _shotgun = false;
    }
     
    public void RelayScore(int score)
    {
        _uiManager.AddCoins(score);
    }
    
    void OnTriggerEnter(Collider other)
    {
        // depending on the power up type, the corresponding functions will be executed and the power ups destroyed
        string type = other.tag;
        
        switch (type)
        {
            case "Coin":
                RelayScore(1);
                Destroy(other.gameObject);
                break;
            
            case "Bag":
                RelayScore(5);
                Destroy(other.gameObject);
                break;
            
            case "Coffee":
                StartCoroutine(EnableSpeedBonus(12));
                Destroy(other.gameObject);
                break;
            
            case "Life":
                _uiManager.AddLife(1);
                Destroy(other.gameObject);
                break;
            
            case "Bomb":
                foreach (Transform child in _spawnmanager.transform)
                {
                    Destroy(child.gameObject);
                    enemiesCount++;
                    _levelCount++;
                }
                Destroy(other.gameObject);
                break;
            
            case "Shotgun":
                StartCoroutine(EnableShootBonus());
                Destroy(other.gameObject);
                break;
            
            case "Wall":
                _hitWall = true;
                _wallHit = other.gameObject;
                break;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        //the bool is reset to false after the Player doesn't hit the wall anymore
        if (other.CompareTag("Wall"))
        {
            _hitWall = false;
        }
    }
    void TransitionInBounds()
    {
        //Player is not allowed to leave the screen through either ot these sides
        if (transform.position.y < -4.3f)
        {
            transform.position = new Vector3(transform.position.x, -4.3f, 0f);
        }      
        else if (transform.position.y > 6.3f)
        {
            transform.position = new Vector3(transform.position.x, 6.3f, 0f);
        }

        else if (transform.position.x > 5.3f)
        {
            transform.position = new Vector3(5.3f, transform.position.y, 0f);
        }
        else if (transform.position.x < -5.3f)
        {
            transform.position = new Vector3(-5.3f, transform.position.y, 0f);
        }
    }
    void TransitionNextLevel()
    {
        //dependent on the amount of destroyed Enemies, the player is allowed to leave the bottom screen if the next level is activated
        if (nextLevel == true && transform.position.y < -4.3f && transform.position.x < 1.5f && transform.position.x > -1.5f)
        {
            //if the Player transition into the next level, the next level will be set as true and points are added to the next score
            if (level_0 == true)
            {
                level_0 = false;
                level_1 = true;
                _nextLevelCount += 30;
            }
            else if (level_1 == true)
            {
                level_1 = false;
                level_2 = true;
                _nextLevelCount += 40;
            }
            else if (level_2 == true)
            {
                level_2 = false;
                level_3 = true;
                _nextLevelCount += 50;
            }
            //the arrow disappears again
            _arrow.SetActive(false);
            transform.position = new Vector3(transform.position.x, 6.3f, 0f);
            //the player cannot move on to the next level without finishing the current one
            nextLevel = false;
            _theGo = false;
            //The walls for the new level will be instantiated
            _wallManager.DestroyWall();
            StartCoroutine(_wallManager.Wall());
            // the int to count the destroyed Enemies will be reseted
            _levelCount = 0;
        }
        else
        {
            TransitionInBounds();
        }
    }
    
    void NextLevel()
    {
        //The Player needs to defeat a specific number of Enemies
        if (level_0 == true && _levelCount > 10)
        {
            _spawnmanager.StopSpawning();
            _theGo = true;
        }
        else if (level_1 == true && _levelCount > 20)
        {
            _spawnmanager.StopSpawning();
            _theGo = true;
        }
        else if (level_2 == true && _levelCount > 30)
        {
            _spawnmanager.StopSpawning();
            _theGo = true;
        }
        else if (level_3 == true && _levelCount > 40)
        {
            _spawnmanager.StopSpawning();
            _theGo = true;
        }
    }
    private IEnumerator Won()
    {
        //when the Player defeats all the Enemies in the last level, he has won
        _theGo = false;
        StartCoroutine(_uiManager.WinnerText());
        yield return new WaitForSeconds(4);
        _highscoreTable.MakeScoreboard(score);
        Destroy(this.gameObject);
    }    
}
