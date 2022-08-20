using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOut : MonoBehaviour
{
    public float FadeOutTime = 0.5f;


    float timer = 0.5f;

    // Start is called before the first frame update
    void Awake()
    {
        SpriteRenderer r = GetComponent<SpriteRenderer>();
        Color c = r.color;
        c.a = 1;
        r.color = c;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        SpriteRenderer r = GetComponent<SpriteRenderer>();
        Color c = r.color;
        c.a = timer / FadeOutTime;
        r.color = c;

        if(timer <= 0.0f)
        {
            Destroy(gameObject);
        }
    }
}
