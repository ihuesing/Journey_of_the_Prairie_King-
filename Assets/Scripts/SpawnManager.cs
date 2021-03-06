using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemiesPrefab;

    private float z = -0.6f;

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
                        Instantiate(_enemiesPrefab, Pos, Quaternion.Euler(0,178,1.5f), this.transform);
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
                        //The position is chosen randomly from one of the position of the given list
                        Vector3 Pos = PosRight[Random.Range(0, PosRight.Count)];
                        Instantiate(_enemiesPrefab, Pos, Quaternion.Euler(0, 178, 1.5f), this.transform);
                        //The position is removed from list so it cannot be chosen twice
                        PosRight.Remove(Pos);
                    }
                    //the list gets cleared and added again
                    PosRight.Clear();
                    PosRightAdd();
                    break;
                //If number == 2, the Enemies will come from the top
                case 2:
                    for (int i = 0; i < amount; i++)
                    {
                        //The position is chosen randomly from one of the position of the given list
                        Vector3 Pos = PosTop[Random.Range(0, PosTop.Count)];
                        Instantiate(_enemiesPrefab, Pos, Quaternion.Euler(0, 178, 1.5f), this.transform);
                        //The position is removed from list so it cannot be chosen twice
                        PosTop.Remove(Pos);
                    }
                    //the list gets cleared and added again
                    PosTop.Clear();
                    PosTopAdd();
                    break;
                //If number == 3, the Enemies will come from the left
                case 3:
                    for (int i = 0; i < amount; i++)
                    {
                        //The position is chosen randomly from one of the position of the given list
                        Vector3 Pos = PosLeft[Random.Range(0, PosLeft.Count)];
                        Instantiate(_enemiesPrefab, Pos, Quaternion.Euler(0, 178, 1.5f), this.transform);
                        //The position is removed from list so it cannot be chosen twice
                        PosLeft.Remove(Pos);
                    }
                    //the list gets cleared and added again
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
        //Entry Postions for the Enemies which will come from the bottom
        PosBottom.Add(new Vector3(-1f, -4.3f, z));
        PosBottom.Add(new Vector3(-0.2f, -4.3f, z));
        PosBottom.Add(new Vector3(0.6f, -4.3f, 7));
    }
    private void PosTopAdd()
    {
        //Entry Postions for the Enemies which will come from the top
        PosTop.Add(new Vector3(-1f, 6.2f, z));
        PosTop.Add(new Vector3(-0.2f, 6.2f, z));
        PosTop.Add(new Vector3(0.6f, 6.2f, z));
    }
    private void PosLeftAdd()
    {
        //Entry Postions for the Enemies which will come from the left
        PosLeft.Add(new Vector3(-5.5f, -0.5f, z));
        PosLeft.Add(new Vector3(-5.5f, 0.3f, z));
        PosLeft.Add(new Vector3(-5.5f, 1.1f, z));
    }
    private void PosRightAdd()
    {
        //Entry Postions for the Enemies which will come from the right
        PosRight.Add(new Vector3(5.5f, 1.1f, z));
        PosRight.Add(new Vector3(5.5f, 0.3f, z));
        PosRight.Add(new Vector3(5.5f, -0.5f, z));
    }
}
