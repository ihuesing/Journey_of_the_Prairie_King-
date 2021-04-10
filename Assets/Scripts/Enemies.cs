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
   
    private bool _hitWall = false;
    // Update is called once per frame
    void Update()
    {
        if (_hitWall)
        {
            //Enemies will move away from the wall
            transform.position = Vector3.MoveTowards(transform.position, _wallHit.gameObject.transform.position, -10f * Time.deltaTime);
        }
        else
        {
            if (GameObject.Find("Player") != null)
            {
                //Enemies will move towards the player
                transform.position = Vector3.MoveTowards(transform.position,
                    GameObject.FindWithTag("Player").GetComponent<Player>().transform.position, 2f * Time.deltaTime);
            }
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
            //After destruction of an Enieme possible Power-Ips can be instantiated
            StartCoroutine(Power_Ups());
        }
        //if Enemies "hit" a wall the bool will be activated to move away from the wall
        else if (other.CompareTag("Wall"))
        {
            _hitWall = true;
            _wallHit = other.gameObject;
        }
    }

    void OnTriggerExit(Collider other)
    {
        //if Enemies don't "hit" the wall anymore, the enemies will move towards the player again
        if (other.CompareTag("Wall"))
        {
            _hitWall = false;
        }
    }
    private IEnumerator Power_Ups()
    {
        //after each start a random number will be generated
        int number = UnityEngine.Random.Range(0, 30);
        //if the number is a 6 or 7, a coin will be instantiated
        if (number > 20)
        {
            Instantiate(_coin, transform.position, Quaternion.identity);
        } else if (number == 8)
        {
            Instantiate(_bag, transform.position, Quaternion.identity);
        } else if (number == 9)
        {
            Instantiate(_coffee, transform.position, Quaternion.identity);
        } else if (number == 1)
        {
            Instantiate(_life, transform.position, Quaternion.identity);
        } else if (number == 2)
        {
            Instantiate(_bomb, transform.position, Quaternion.identity);
        } else if (number == 3)
        {
            Instantiate(_shotgun, transform.position, Quaternion.identity);
        }
        //else nothing happens
        yield return null;
    }
    

}
