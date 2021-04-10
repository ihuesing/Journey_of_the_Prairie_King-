using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemiesPrefab;

    //a bool to start or stop the spawning of new Enemies
    public bool _spawningOn = true;

    private Vector3 _vec;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawningEnemies());
    }
    private IEnumerator SpawningEnemies()
    {
        // only instantiate Enemies if _spawningOn is true
        while (_spawningOn)
        {
            // generates a random number which determines the amount of Enemies it should instantiate at once
            int amount = UnityEngine.Random.Range(1, 4);

            //generates a random number which determines on which side the Enemies should be instantiated
            int number = UnityEngine.Random.Range(0, 4);
            
            
            for (int i = 0; i < amount; i++)
            {
                switch (number)
                {
                    case 0: // Enemies will come from the bottom
                        _vec = new Vector3(Random.Range(-3f, 3), -6f, 0f);
                        break;
                    case 1: // Enemies will come from the right
                        _vec = new Vector3(10, Random.Range(-3f, 3f), 0f);
                        break;
                    case 2: // Enemies will come from the top
                        _vec = new Vector3(Random.Range(-3f, 3f), 6f, 0f);
                        break;
                    case 3: // Enemies will come from the left
                        _vec = new Vector3(-10, Random.Range(-3f, 3f), 0f);
                        break;
                    
                }
                Instantiate(_enemiesPrefab, _vec, Quaternion.identity, this.transform);
            }
            
            // every 2 seconds Enemies will be instantiate
            // possible option to make it a variable so the seconds can be change throughout the game
            yield return new WaitForSeconds(2f);
        }
    }
    //this will stop the spawning if needed
    public void StopSpawning()
    {
        _spawningOn = false;
    }
    //this will let the spawning restart if needed
    public void StartSpawning()
    {
        _spawningOn = true;
        //Coroutine is called again since it is not in the update
        StartCoroutine(SpawningEnemies());
    }
}
