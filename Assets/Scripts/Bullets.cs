using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullets : MonoBehaviour
{
    //Vector in which direction the bullet should move
    public Vector3 _shootingDirection;

    [SerializeField]
    private float _shootingSpeed = 7;

    // Update is called once per frame
    void Update()
    {
        BulletMovement();
    }
    void BulletMovement()
    {
        //The bullet moves in an iniated direction in given speed
        transform.Translate(_shootingDirection * _shootingSpeed * Time.deltaTime);

        //if the bullet is outside of the frame, it will be destroyed
        if (transform.position.y > 6f || transform.position.y < -5f || transform.position.x > 5.2f || transform.position.x < -5.2f)
        {
            Destroy(this.gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        //If the bullet hits the wall, it will be destroyed
        if (other.CompareTag("Wall"))
        {
            Destroy(this.gameObject);
        }
        //If the bullet hits a bush, it will be destroyed
        else if (other.CompareTag("Bush"))
        {
            Destroy(this.gameObject);
        }
    }
    
}
