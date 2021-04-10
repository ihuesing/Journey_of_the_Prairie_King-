using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies : MonoBehaviour
{
    [SerializeField]
    private GameObject _coin;
    
    [SerializeField]
    private GameObject _bag;
    
    [SerializeField]
    private GameObject _coffee;
    
    [SerializeField]
    private GameObject _life;
    
    [SerializeField]
    private GameObject _bomb;
    
    [SerializeField]
    private GameObject _shotgun;
    
    private GameObject _wallHit;

    [SerializeField] 
    private float _speed = 1f;

    [SerializeField] 
    private int _powerUpTime = 7;
    
    
    // Update is called once per frame
    void Update()
    {

        if (GameObject.Find("Player") != null || transform.position != Vector3.MoveTowards(transform.position,
                GameObject.FindWithTag("Player").GetComponent<Player>().transform.position, _speed * Time.deltaTime))
        {
            //Enemies will move towards and rotate the player
            // Vector3 newDirection = Vector3.RotateTowards(transform.position,
            //GameObject.FindWithTag("Player").GetComponent<Player>().transform.position, _speed * Time.deltaTime, 0f);
            //transform.rotation = Quaternion.LookRotation(newDirection);
            transform.position = Vector3.MoveTowards(transform.position,
                GameObject.FindWithTag("Player").GetComponent<Player>().transform.position + new Vector3(0,0,-0.6f), _speed * Time.deltaTime);
        }
        //if the Enemies move outside of the screen, they'll be destroyed
        if (transform.position.y > 6.7 || transform.position.y < -4.7f || transform.position.x > 5.7f || transform.position.x < -5.7f)
        {
            Destroy(this.gameObject);
        }

    }
    void OnTriggerEnter(Collider other)
    {
        //if an Enemie hits the Player, it will be destroyed and the Player will lose a life
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Player>().Damage();
            Destroy(this.gameObject);
        }
        //if a Bullet hits an Enemie both gameobjects will be destroyed
        else if (other.CompareTag("Bullet"))
        {
            //Player counts how many Enemies he has destroyed
            GameObject.FindWithTag("Player").GetComponent<Player>().Count();
            Destroy(other.gameObject);
            Destroy(this.gameObject);
            //After destruction of an Enemy possible Power-Ups can be instantiated
            Power_Ups();
        }
    }
    void OnTriggerStay(Collider other)
    {
        //Enemies will rotate around each other while they are triggered
        if (other.CompareTag("Enemies"))
        {
            this.transform.RotateAround(other.gameObject.transform.position, Vector3.right, 25 * Time.deltaTime);
        }
        //Enemies will rotate around wall while they are triggered
        else if (other.CompareTag("Wall"))
        {
            _wallHit = other.gameObject;
            this. transform.position = Vector3.MoveTowards(this.transform.position, _wallHit.transform.position, -0.5f * Time.deltaTime);
            //this.transform.RotateAround(_wallHit.gameObject.transform.position, Vector3.right, 25 * Time.deltaTime);
        }
    }
    
    void Power_Ups()
    {
        //after each start a random number will be generated
        int number = UnityEngine.Random.Range(0, 50);
        
        // for different numbers, different power ups will be instantiated
        // if the power ups won't be collected, they will be destroyed after time
        if (number > 42)
        {
            _coin = (GameObject) Instantiate(_coin, transform.position + new Vector3(0, 0, 0.6f), Quaternion.identity);
            Destroy(_coin.gameObject,_powerUpTime);
        } 
        else if (number < 5)
        {
            _bag = (GameObject) Instantiate(_bag, transform.position + new Vector3(0, 0, 0.6f), Quaternion.identity);
            Destroy(_bag.gameObject,_powerUpTime);
        } 
        else if (number == 8 || number == 14)
        {
            _coffee = (GameObject) Instantiate(_coffee, transform.position + new Vector3(0, 0, 0.6f), Quaternion.Euler(-42f, 0,0));
            Destroy(_coffee.gameObject,_powerUpTime);
        } 
        else if (number == 12 || number == 13)
        {
            _life = (GameObject) Instantiate(_life, transform.position + new Vector3(0, 0, 0.6f), Quaternion.identity);
            Destroy(_life.gameObject,_powerUpTime);
        } 
        else if (number == 7 || number > 20 || number < 40)
        {
            _bomb = (GameObject) Instantiate(_bomb, transform.position + new Vector3(0,0,0.35f), Quaternion.Euler(-30,0,90));
            Destroy(_bomb.gameObject,_powerUpTime);
        } 
        else if (number == 9 || number == 10 || number == 11)
        {
            _shotgun = (GameObject) Instantiate(_shotgun, transform.position + new Vector3(0, 0, 0.6f), Quaternion.identity);
            Destroy(_shotgun.gameObject,_powerUpTime);
        }
        
    }
}
