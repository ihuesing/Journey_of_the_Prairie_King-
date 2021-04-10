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


    //a bool to start or stop the spawning of new Enemies
    public bool spawningOn = true;

    // Start is called before the first frame update
    void Start()
    {
        //the entry position for the Enemies are added
        PosBottomAdd();
        PosLeftAdd();
        PosTopAdd();
        PosRightAdd();
        //The Enemies start spawning
        StartCoroutine(SpawningEnemies());
    }
    private IEnumerator SpawningEnemies()
    {
        // only instantiate Enemies if _spawningOn is true
        while (spawningOn)
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
                        Vector3 Pos = PosBottom[Random.Range(0, PosBottom.Count)];
                        Instantiate(_enemiesPrefab, Pos, Quaternion.identity, this.transform);
                        //The position is removed from list so it cannot be chosen twice
                        PosBottom.Remove(Pos);                       
                    }
                    //the list gets cleared and added again
                    PosBottom.Clear();
                    PosBottomAdd();
                    break;
                //If number == 1, the Enemies will come from the right 
                case 1:
                    for (int i = 0; i < amount; i++)
                    {
                        Vector3 Pos = PosRight[Random.Range(0, PosRight.Count)];
                        Instantiate(_enemiesPrefab, Pos, Quaternion.identity, this.transform);
                        PosRight.Remove(Pos);
                    }
                    PosRight.Clear();
                    PosRightAdd();
                    break;
                //If number == 2, the Enemies will come from the top
                case 2:
                    for (int i = 0; i < amount; i++)
                    {
                        Vector3 Pos = PosTop[Random.Range(0, PosTop.Count)];
                        Instantiate(_enemiesPrefab, Pos, Quaternion.identity, this.transform);
                        PosTop.Remove(Pos);
                    }
                    PosTop.Clear();
                    PosTopAdd();
                    break;
                //If number == 3, the Enemies will come from the left
                case 3:
                    for (int i = 0; i < amount; i++)
                    {
                        Vector3 Pos = PosLeft[Random.Range(0, PosLeft.Count)];
                        Instantiate(_enemiesPrefab, Pos, Quaternion.identity, this.transform);
                        PosLeft.Remove(Pos);
                    }
                    PosLeft.Clear();
                    PosLeftAdd();
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
        spawningOn = false;
    }
    //this will let the spawning restart if needed
    public void StartSpawning()
    {
        spawningOn = true;
        //Coroutine is called again since it is not in the update
        StartCoroutine(SpawningEnemies());
    }
    private void PosBottomAdd()
    {
        //Entry Postions for the Enemies which will come fromm the bottom
        PosBottom.Add(new Vector3(0.8f, -4.7f, 0f));
        PosBottom.Add(new Vector3(0f, -4.7f, 0f));
        PosBottom.Add(new Vector3(-0.8f, -4.7f, 0f));
    }
    private void PosTopAdd()
    {
        //Entry Postions for the Enemies which will come fromm the top
        PosTop.Add(new Vector3(0.7f, 6.7f, 0));
        PosTop.Add(new Vector3(0f, 6.7f, 0));
        PosTop.Add(new Vector3(-0.7f, 6.7f, 0));
    }
    private void PosLeftAdd()
    {
        //Entry Postions for the Enemies which will come fromm the left
        PosLeft.Add(new Vector3(-5.7f, 0.3f, 0));
        PosLeft.Add(new Vector3(-5.7f, 1f, 0));
        PosLeft.Add(new Vector3(-5.7f, 1.7f, 0));
    }
    private void PosRightAdd()
    {
        //Entry Postions for the Enemies which will come fromm the right
        PosRight.Add(new Vector3(5.7f, 0.3f, 0));
        PosRight.Add(new Vector3(5.7f, 1f, 0));
        PosRight.Add(new Vector3(5.7f, 1.7f, 0));
    }
}
