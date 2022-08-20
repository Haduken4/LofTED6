using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDialogueOnButton : MonoBehaviour
{
    public DialogueTrigger DialogueToTrigger = null;

    bool collidingWithPlayer = false;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && collidingWithPlayer)
        {
            DialogueToTrigger?.TriggerDialogue();
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            collidingWithPlayer = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collidingWithPlayer = false;
        }
    }
}
