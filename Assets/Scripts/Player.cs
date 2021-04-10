using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //access to all the needed gameobjects
    [SerializeField]
    private GameObject _bulletPrefab;

    [SerializeField]
    private GameObject _wall;

    [SerializeField]
    private Bullets _bullet;

    //access to all the needed classes
    [SerializeField]
    private SpawnManager _spawnmanager;

    //needed variables
    //speed of the Player
    [SerializeField]
    private float _speed = 7;
    
    //int to count all the lives of the Player
    [SerializeField]
    public int _lives = 4;
    
    [SerializeField]
    private float _shootingRate = 0.4f;
    private float _timeToShoot = 0;

    //int to count all the collected coins
    private int coins = 0;

    //int to count all the destroyed Enemies
    public int _enemiesCount;
    
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
        Shooting();
    }

    
    void PlayerMovement()
    {
        //turn the key input (a,w,s,e) into -1 or 1
        float horizontalMoveInput = Input.GetAxis("Horizontal");
        float verticalMoveInput = Input.GetAxis(("Vertical"));

        //Player moves depending on the pressed key
        transform.Translate(new Vector3(horizontalMoveInput, verticalMoveInput, 0f) * _speed * Time.deltaTime);

        //if the player moves outside the screen
        //dependent on the amount of destroyed Enemies, the player is not allowed to leave the screen
        if (_enemiesCount < 10)
        {
            if (transform.position.y < -5.2f)
            {
                transform.position = new Vector3(transform.position.x, -5.2f, 0f);
            }
        }
        // if the amount exceeds a specific limit the Player is allowed to move to the next level
        else
        {
            // if the Player moves to the bottom of the screen he will end up at the top of the "new" level
            if (transform.position.y < -5.2f)
            {
                transform.position = new Vector3(transform.position.x, 6, 0f);
                //The walls for the new level will be instantiated
                StartCoroutine(Wall());
                // the int to count the destroyed Enemies will be reseted
                Reset();
            }
        }
        //Player is not allowed to leave the screen through either ot these sides        
        if (transform.position.y > 6f)
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
            _timeToShoot = Time.time + _shootingRate;
        }

    }
    public void Count()
    {
        //counts the Enemies the Player has destroyed
        _enemiesCount++;
        //if the count exceeds a given limit the spawning of new Enemies will be stopped
        if(_enemiesCount > 10)
        {
            _spawnmanager.StopSpawning();
        }
    }
    public void Damage()
    {
        //if an Enemie hits the Player, the Player will lose a live
        _lives--;

        //if the Player has no lives left the Player and the remaining Enemies will be destroyed
        if (_lives == 0)
        {
            //the spawning of new Enemies will be stopped
            if (_enemiesCount > 10)
                _spawnmanager.StopSpawning();
            foreach (Transform child in _spawnmanager.transform)
            {
                Destroy(child.gameObject);
            }
            Destroy(this.gameObject);
        }

    }
    void OnTriggerEnter(Collider other)
    {
        //if the Player "hits" a coin, it will be "collected" 
        if (other.CompareTag("Coin"))
        {
            coins++;
            Destroy(other.gameObject);
            //returns total amount of coins the Player has collected
            Debug.Log(coins);
        }
        //if the Player hits a Wall the Trigger of the Player is set to false so it cannot move through the wall
        else if (other.CompareTag("Wall"))
        {
            GetComponent<Collider>().isTrigger = false;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        //the Trigger is reset to true after the Player doesn't hit the wall anymore
        if (other.CompareTag("Wall"))
        {
            GetComponent<Collider>().isTrigger = true;
        }
    }
    private IEnumerator Wall()
    {
        //all wall elements are instantiated at a fixed position
        Instantiate(_wall, new Vector3(-4, -1f, 0), Quaternion.identity);
        Instantiate(_wall, new Vector3(-4, -1.8f, 0), Quaternion.identity);
        Instantiate(_wall, new Vector3(-3.2f, -1.8f, 0), Quaternion.identity);
        Instantiate(_wall, new Vector3(-4, 3f, 0), Quaternion.identity);
        Instantiate(_wall, new Vector3(-4, 3.8f, 0), Quaternion.identity);
        Instantiate(_wall, new Vector3(-3.2f, 3.8f, 0), Quaternion.identity);
        Instantiate(_wall, new Vector3(4, -1f, 0), Quaternion.identity);
        Instantiate(_wall, new Vector3(4, -1.8f, 0), Quaternion.identity);
        Instantiate(_wall, new Vector3(3.2f, -1.8f, 0), Quaternion.identity);
        Instantiate(_wall, new Vector3(4, 3f, 0), Quaternion.identity);
        Instantiate(_wall, new Vector3(4, 3.8f, 0), Quaternion.identity);
        Instantiate(_wall, new Vector3(3.2f, 3.8f, 0), Quaternion.identity);

        //waits a few seconds until new Enemies are spawned again
        yield return new WaitForSeconds(1);
        _spawnmanager.StartSpawning();
    }
}
