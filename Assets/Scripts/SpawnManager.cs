using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemiesPrefab;

    //a bool to start or stop the spawning of new Enemies
    public bool _spawningOn = true;

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

            switch (number)
            {
                //If number == 0, the Enemies will come from the bottom
                case 0:
                    for (int i = 0; i < amount; i++)
                    {
                        Instantiate(_enemiesPrefab, new Vector3(Random.Range(-1f, 1), -6f, 0f), Quaternion.identity, this.transform);
                    }
                    break;
                //If number == 1, the Enemies will come from the right 
                case 1:
                    for (int i = 0; i < amount; i++)
                    {
                        Instantiate(_enemiesPrefab, new Vector3(10, Random.Range(-1f, 1f), 0f), Quaternion.identity, this.transform);
                    }
                    break;
                //If number == 2, the Enemies will come from the top
                case 2:
                    for (int i = 0; i < amount; i++)
                    {
                        Instantiate(_enemiesPrefab, new Vector3(Random.Range(-1f, 1f), 6f, 0f), Quaternion.identity, this.transform);
                    }
                    break;
                //If number == 3, the Enemies will come from the left
                case 3:
                    for (int i = 0; i < amount; i++)
                    {
                        Instantiate(_enemiesPrefab, new Vector3(-10, Random.Range(-1f, 1f), 0f), Quaternion.identity, this.transform);
                    }
                    break;
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
