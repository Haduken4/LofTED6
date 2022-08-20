using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WhiteboardRoomExit : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.LeftControl))
        {
            if(Input.GetKey(KeyCode.LeftShift))
            {
                if (Input.GetKeyDown(KeyCode.D))
                {
                    GetComponent<SceneTransitionOnCollide>().StartSceneTransition();
                }
            }
        }
    }
}
