using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionOnCollide : MonoBehaviour
{
    public GameObject FadeArchetype = null;
    public float TimeToTransition = 1.0f;
    public string SceneToLoad = "";

    float timer = 0.0f;
    GameObject fadeObj = null;

    // Update is called once per frame
    void Update()
    {
        if(fadeObj != null)
        {
            timer += Time.deltaTime;
            SpriteRenderer r = fadeObj.GetComponent<SpriteRenderer>();
            Color c = r.color;
            c.a = timer / TimeToTransition;
            r.color = c;

            if(timer >= TimeToTransition)
            {
                SceneManager.LoadScene(SceneToLoad);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        StartSceneTransition();
    }

    public void StartSceneTransition()
    {
        if (fadeObj == null)
        {
            timer = 0.0f;
            fadeObj = Instantiate(FadeArchetype);
            fadeObj.transform.position = Vector3.forward * -5;
        }
    }
}
