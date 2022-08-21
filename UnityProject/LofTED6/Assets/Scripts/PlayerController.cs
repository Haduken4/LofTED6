using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [System.NonSerialized]
    public bool Locked = false;

    public float Movespeed = 10;
    
    Rigidbody2D rb2d;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Locked)
        {
            rb2d.velocity = Vector2.zero;
            return;
        }

        Vector2 dir = Vector2.zero;

        if(Input.GetKey(KeyCode.A))
        {
            dir += Vector2.left;
            GetComponent<SpriteRenderer>().flipX = false;
        }
        if (Input.GetKey(KeyCode.D))
        {
            dir += Vector2.right;
            GetComponent<SpriteRenderer>().flipX = true;
        }
        if (Input.GetKey(KeyCode.W))
        {
            dir += Vector2.up;
        }
        if (Input.GetKey(KeyCode.S))
        {
            dir += Vector2.down;
        }

        rb2d.velocity = dir.normalized * Movespeed;
    }
}
