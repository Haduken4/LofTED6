using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneButton : MonoBehaviour
{
    public string SceneToLoad = "";

    public void ButtonPressed()
    {
        SceneManager.LoadScene(SceneToLoad);
    }
}
