using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitGame : MonoBehaviour
{
    

    // Update is called once per frame
    void Update()
    {
        //Game can be exited by pressing the escape Key
        if(Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }
}
