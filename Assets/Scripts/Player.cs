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
    public int _enemiesCount;
    
    // bool to control if shotgun is active
    private bool _shotgun = false;

    //bool to to see in which level the player is
    public bool _level_0 = true;
    public bool _level_1 = false;
    public bool _level_2 = false;
    public bool _level_3 = false;
    public bool _nextLevel = false;

    //bool to control the collision between Player and Wall
    private bool _hitWall = false;
    
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
        if(_hitWall)
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
        if (_spawnmanager.transform.childCount == 0 && _nextLevel == true)
        {
            _arrow.SetActive(true);
        }
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
    private void Reset()
    {
        //resets the _enemiesCount to 0
         _enemiesCount = 0;
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
        _enemiesCount++;
        //if the count exceeds a given limit the spawning of new Enemies will be stopped
        NextLevel();
    }
    public void Damage()
    {
        //if an Enemie hits the Player, the Player will lose a live
        _uiManager.AddLife(-1);
        //_lives--;
        

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
            _uiManager.GameOver();
        }

    }
    
    IEnumerator EnableSpeedBonus(int speed)
    {
        _speed = speed;
        yield return new WaitForSeconds(20);
        _speed = 7;
    }
    
    IEnumerator EnableShootBonus()
    {
        _shotgun = true;
        yield return new WaitForSecondsRealtime(20);
        _shotgun = false;
    }
    
    void DestroyAllEnemies()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag ("Enemies");
     
        for (var i = 0 ; i < enemies.Length ; i ++)
        {   
            _enemiesCount++;
            Destroy(enemies[i]);
        }
    }
    

    public void RelayScore(int score)
    {
        _uiManager.AddCoins(score);
    }
    
    void OnTriggerEnter(Collider other)
    {
       
        string type = other.tag;
        
        switch (type)
        {
            case "Coin":
                RelayScore(1);
                //coins++;
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
                DestroyAllEnemies();
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
        //the bool is reset to falsw after the Player doesn't hit the wall anymore
        if (other.CompareTag("Wall"))
        {
            _hitWall = false;
        }
    }
    void TransitionInBounds()
    {
        //Player is not allowed to leave the screen through either ot these sides
        if (transform.position.y < -5.2f)
        {
            transform.position = new Vector3(transform.position.x, -5.2f, 0f);
        }      
        else if (transform.position.y > 6f)
        {
            transform.position = new Vector3(transform.position.x, 6f, 0f);
        }

        else if (transform.position.x > 10f)
        {
            transform.position = new Vector3(10f, transform.position.y, 0f);
        }
        else if (transform.position.x < -10f)
        {
            transform.position = new Vector3(-10f, transform.position.y, 0f);
        }
    }
    void TransitionNextLevel()
    {
        //dependent on the amount of destroyed Enemies, the player is allowed to leave the bottom screen if the next level is activated
        if (_nextLevel == true && transform.position.y < -5.2f && transform.position.x < 1.5f && transform.position.x > -1.5f)
        {
            //the arrow disappears again
            _arrow.SetActive(false);
            transform.position = new Vector3(transform.position.x, 6, 0f);
            _nextLevel = false;
            //The walls for the new level will be instantiated
            _wallManager.DestroyWall();
            StartCoroutine(_wallManager.Wall());
            // the int to count the destroyed Enemies will be reseted
            Reset();
        }
        else
        {
            TransitionInBounds();
        }
    }
    void NextLevel()
    {
        switch (_enemiesCount)
        {
            //Switch case for each Level
            //The player has to defeat more enemies in each level
            case 10:
                if(_level_0 == true)
                {
                    _spawnmanager.StopSpawning();
                    _nextLevel = true;
                    _level_0 = false;
                    _level_1 = true;
                }
                break;
            case 20:
                if (_level_1 == true)
                {
                    _spawnmanager.StopSpawning();
                    _nextLevel = true;
                    _level_1 = false;
                    _level_2 = true;
                }
                break;
            case 30:
                if (_level_2 == true)
                {
                    _spawnmanager.StopSpawning();
                    _nextLevel = true;
                    _level_2 = false;
                    _level_3 = true;
                }
                break;
        }
    }
}
