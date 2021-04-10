using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies : MonoBehaviour
{
    [SerializeField]
    private GameObject _coin;
   
    // Update is called once per frame
    void Update()
    {
        //Enemies will move towards the player
        transform.position = Vector3.MoveTowards(transform.position, GameObject.FindWithTag("Player").GetComponent<Player>().transform.position, 2f * Time.deltaTime);
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
    }
    
    private IEnumerator Power_Ups()
    {
        //after each start a random number will be generated
        int number = UnityEngine.Random.Range(0, 10);
        //if the number is a 6 or 7, a coin will be instantiated
        if (number == 6 || number == 7)
        {
            Instantiate(_coin, transform.position, Quaternion.identity);
        }
        //else nothing happends
        yield return null;
    }

}
