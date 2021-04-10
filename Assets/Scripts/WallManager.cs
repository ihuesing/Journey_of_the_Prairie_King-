using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallManager : MonoBehaviour
{
    [SerializeField]
    private Player _player;

    [SerializeField]
    private SpawnManager _spawnmanager;

    [SerializeField]
    private GameObject _wall;

    public IEnumerator Wall()
    {
        if (_player.level_1 == true)
        {
            //all wall elements are instantiated at a fixed position
            Instantiate(_wall, new Vector3(-3, -1f, 0), Quaternion.identity, this.transform);
            Instantiate(_wall, new Vector3(-3, -1.8f, 0), Quaternion.identity, this.transform);
            Instantiate(_wall, new Vector3(-2.2f, -1.8f, 0), Quaternion.identity, this.transform);
            Instantiate(_wall, new Vector3(-3, 3f, 0), Quaternion.identity, this.transform);
            Instantiate(_wall, new Vector3(-3, 3.8f, 0), Quaternion.identity, this.transform);
            Instantiate(_wall, new Vector3(-2.2f, 3.8f, 0), Quaternion.identity, this.transform);
            Instantiate(_wall, new Vector3(3, -1f, 0), Quaternion.identity, this.transform);
            Instantiate(_wall, new Vector3(3, -1.8f, 0), Quaternion.identity, this.transform);
            Instantiate(_wall, new Vector3(2.2f, -1.8f, 0), Quaternion.identity, this.transform);
            Instantiate(_wall, new Vector3(3, 3f, 0), Quaternion.identity, this.transform);
            Instantiate(_wall, new Vector3(3, 3.8f, 0), Quaternion.identity, this.transform);
            Instantiate(_wall, new Vector3(2.2f, 3.8f, 0), Quaternion.identity, this.transform);
        }
        else if (_player.level_2 == true)
        {
            //all wall elements are instantiated at a fixed position
            Instantiate(_wall, new Vector3(-3, 1, 0), Quaternion.identity, this.transform);
            Instantiate(_wall, new Vector3(3, 1, 0), Quaternion.identity, this.transform);
            Instantiate(_wall, new Vector3(0, -1.75f, 0), Quaternion.identity, this.transform);
            Instantiate(_wall, new Vector3(0, 3.75f, 0), Quaternion.identity, this.transform);
        }
        else if (_player.level_3 == true)
        {
            //all wall elements are instantiated at a fixed position
            Instantiate(_wall, new Vector3(-3, -0.2f, 0), Quaternion.identity, this.transform);
            Instantiate(_wall, new Vector3(-3, -1f, 0), Quaternion.identity, this.transform);
            Instantiate(_wall, new Vector3(-3, -1.8f, 0), Quaternion.identity, this.transform);
            Instantiate(_wall, new Vector3(-2.2f, -1.8f, 0), Quaternion.identity, this.transform);
            Instantiate(_wall, new Vector3(-1.4f, -1.8f, 0), Quaternion.identity, this.transform);
            Instantiate(_wall, new Vector3(-3, 2.2f, 0), Quaternion.identity, this.transform);
            Instantiate(_wall, new Vector3(-3, 3f, 0), Quaternion.identity, this.transform);
            Instantiate(_wall, new Vector3(-3, 3.8f, 0), Quaternion.identity, this.transform);
            Instantiate(_wall, new Vector3(-2.2f, 3.8f, 0), Quaternion.identity, this.transform);
            Instantiate(_wall, new Vector3(-1.4f, 3.8f, 0), Quaternion.identity, this.transform);
            Instantiate(_wall, new Vector3(3, -1f, 0), Quaternion.identity, this.transform);
            Instantiate(_wall, new Vector3(3, -0.2f, 0), Quaternion.identity, this.transform);
            Instantiate(_wall, new Vector3(3, -1.8f, 0), Quaternion.identity, this.transform);
            Instantiate(_wall, new Vector3(2.2f, -1.8f, 0), Quaternion.identity, this.transform);
            Instantiate(_wall, new Vector3(1.4f, -1.8f, 0), Quaternion.identity, this.transform);
            Instantiate(_wall, new Vector3(3, 2.2f, 0), Quaternion.identity, this.transform);
            Instantiate(_wall, new Vector3(3, 3f, 0), Quaternion.identity, this.transform);
            Instantiate(_wall, new Vector3(3, 3.8f, 0), Quaternion.identity, this.transform);
            Instantiate(_wall, new Vector3(2.2f, 3.8f, 0), Quaternion.identity, this.transform);
            Instantiate(_wall, new Vector3(1.4f, 3.8f, 0), Quaternion.identity, this.transform);
        }


        //waits a few seconds until new Enemies are spawned again
        yield return new WaitForSeconds(2);
        _spawnmanager.StartSpawning();
    }
    public void DestroyWall()
    {
        //after each level the walls can be destroyed again, so they won't overlap
        foreach (Transform child in this.transform)
        {
            Destroy(child.gameObject);
        }
    }
}
