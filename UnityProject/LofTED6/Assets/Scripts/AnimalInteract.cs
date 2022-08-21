using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalInteract : MonoBehaviour
{
    public VariationAudioPlayer SoundSource = null;
    public GameObject ParticleEffect = null;

    bool playerColliding = false;

    // Update is called once per frame
    void Update()
    {
        if(playerColliding && Input.GetKeyUp(KeyCode.Space))
        {
            if(ParticleEffect != null)
            {
                GameObject p = Instantiate(ParticleEffect, transform.position - (Vector3.forward * 0.1f), Quaternion.identity);
                p.GetComponent<Renderer>().sortingLayerID = GetComponent<Renderer>().sortingLayerID;
            }
            SoundSource.PlaySound();
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerColliding = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerColliding = false;
        }
    }
}
