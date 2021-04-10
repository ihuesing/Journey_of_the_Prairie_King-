using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemiesPrefab;

    //List for the position in which thr Enemies can be instantiated
    List<Vector3> PosBottom = new List<Vector3>();
    List<Vector3> PosTop = new List<Vector3>();
    List<Vector3> PosLeft= new List<Vector3>();
    List<Vector3> PosRight = new List<Vector3>();

    //List to check if the position is already taken
    List<Vector3> PosAlready = new List<Vector3>();

    //a bool to start or stop the spawning of new Enemies
    public bool _spawningOn = true;

    // Start is called before the first frame update
    void Start()
    {
        AddLists();
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
                        //The position is chosen randomly from one of the position of the given list
                        Vector3 Pos = PosBottom[Random.Range(0, 3)];
                        for (int j = 0; j < PosAlready.Count; j++)
                        {
                            //if a position is aready taken, a new position needs to be randomly chosen
                            while(Pos == PosAlready[j])
                            {
                                Pos = PosBottom[Random.Range(0, 3)];
                            }                            
                        }
                        //the chosen position is added to the list, so it can't be chosen twice
                        PosAlready.Add(Pos);
                        //the enemy is instantiated at the chosen position
                        Instantiate(_enemiesPrefab, Pos, Quaternion.identity, this.transform);
                    }
                    //after every enemy for this case is instantiated, the list will be cleared
                    PosAlready.Clear();
                    break;
                //If number == 1, the Enemies will come from the right 
                case 1:
                    for (int i = 0; i < amount; i++)
                    {
                        Vector3 Pos = PosRight[Random.Range(0, 3)];
                        for (int j = 0; j < PosAlready.Count; j++)
                        {

                            while (Pos == PosAlready[j])
                            {
                                Pos = PosRight[Random.Range(0, 3)];
                            }                            
                        }
                        PosAlready.Add(Pos);
                        Instantiate(_enemiesPrefab, Pos, Quaternion.identity, this.transform);
                    }
                    PosAlready.Clear();
                    break;
                //If number == 2, the Enemies will come from the top
                case 2:
                    for (int i = 0; i < amount; i++)
                    {
                        Vector3 Pos = PosTop[Random.Range(0, 3)];
                        for (int j = 0; j < PosAlready.Count; j++)
                        {

                            while (Pos == PosAlready[j])
                            {
                                Pos = PosTop[Random.Range(0, 3)];
                            }
                        }
                        PosAlready.Add(Pos);
                        Instantiate(_enemiesPrefab, Pos, Quaternion.identity, this.transform);
                    }
                    PosAlready.Clear();
                    break;
                //If number == 3, the Enemies will come from the left
                case 3:
                    for (int i = 0; i < amount; i++)
                    {
                        Vector3 Pos = PosLeft[Random.Range(0, 3)];
                        for (int j = 0; j < PosAlready.Count; j++)
                        {

                            while (Pos == PosAlready[j])
                            {
                                Pos = PosLeft[Random.Range(0, 3)];
                            }
                        }
                        PosAlready.Add(Pos);
                        Instantiate(_enemiesPrefab, Pos, Quaternion.identity, this.transform);
                    }
                    PosAlready.Clear();
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
    private void AddLists()
    {
        //specific vectors are added to specific lists
        PosBottom.Add(new Vector3(0.7f, -6f, 0f));
        PosBottom.Add(new Vector3(0f, -6f, 0f));
        PosBottom.Add(new Vector3(-0.7f, -6f, 0f));

        PosTop.Add(new Vector3(0.7f, 6f, 0));
        PosTop.Add(new Vector3(0f, 6f, 0));
        PosTop.Add(new Vector3(-0.7f, 6f, 0));

        PosLeft.Add(new Vector3(-10, 0.3f, 0));
        PosLeft.Add(new Vector3(-10, 1f, 0));
        PosLeft.Add(new Vector3(-10, 1.7f, 0));
        
        PosRight.Add(new Vector3(10, 0.3f, 0));
        PosRight.Add(new Vector3(10, 1f, 0));
        PosRight.Add(new Vector3(10, 1.7f, 0));
    }
}
