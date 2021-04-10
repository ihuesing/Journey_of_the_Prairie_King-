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
    // Update is called once per frame
    void Update()
    {

        if (GameObject.Find("Player") != null || transform.position == Vector3.MoveTowards(transform.position,
                GameObject.FindWithTag("Player").GetComponent<Player>().transform.position, _speed * Time.deltaTime))
        {
            //Enemies will move towards and rotate the player
            Vector3 newDirection = Vector3.RotateTowards(transform.position,
                GameObject.FindWithTag("Player").GetComponent<Player>().transform.position, _speed * Time.deltaTime,0f);
            transform.rotation = Quaternion.LookRotation(newDirection);
            transform.position = Vector3.MoveTowards(transform.position,
                GameObject.FindWithTag("Player").GetComponent<Player>().transform.position, _speed * Time.deltaTime);
        }
        //if the Enemies move outside of the screen, they'll be destroyed
        if (transform.position.y > 6.2f || transform.position.y < -5.4f || transform.position.x > 10.2f || transform.position.x < -10.2f)
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
            //After destruction of an Enieme possible Power-Ups can be instantiated
            StartCoroutine(Power_Ups());
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
            this.transform.RotateAround(_wallHit.gameObject.transform.position, Vector3.right, 25 * Time.deltaTime);
        }
    }
    
    private IEnumerator Power_Ups()
    {
        //after each start a random number will be generated
        int number = UnityEngine.Random.Range(0, 50);
        if (number > 42)
        {
            Instantiate(_coin, transform.position, Quaternion.identity);
        } 
        else if (number < 5)
        {
            Instantiate(_bag, transform.position, Quaternion.identity);
        } 
        else if (number == 8 || number == 14)
        {
            Instantiate(_coffee, transform.position, Quaternion.identity);
        } 
        else if (number == 12 || number == 13)
        {
            Instantiate(_life, transform.position, Quaternion.identity);
        } 
        else if (number == 7)
        {
            Instantiate(_bomb, transform.position, Quaternion.identity);
        } 
        else if (number == 9 || number == 10 || number == 11)
        {
            Instantiate(_shotgun, transform.position, Quaternion.identity);
        }
        //else nothing happens
        yield return null;
    }
}
