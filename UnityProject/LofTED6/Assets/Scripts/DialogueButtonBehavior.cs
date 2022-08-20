using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueButtonBehavior : MonoBehaviour
{
    public GameObject NextDialogue = null;
    public DialogueBox DialogueOwner = null;
    public bool ForceUnlockPlayer = false;
    public bool SpawnOnPlayer = false;

    public void TriggerOption()
    {
        if(DialogueOwner != null)
        {
            DialogueOwner.NextDialogue = NextDialogue;
            DialogueOwner.ForceUnlockPlayer = ForceUnlockPlayer;
            DialogueOwner.SpawnOnPlayer = SpawnOnPlayer;
            DialogueOwner.FinishDialogue();
        }
    }
}
