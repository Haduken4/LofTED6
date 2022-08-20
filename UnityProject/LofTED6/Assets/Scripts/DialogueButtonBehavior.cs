using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueButtonBehavior : MonoBehaviour
{
    public GameObject NextDialogue = null;
    public DialogueBox DialogueOwner = null;

    public void TriggerOption()
    {
        if(DialogueOwner != null)
        {
            DialogueOwner.NextDialogue = NextDialogue;
            DialogueOwner.FinishDialogue();
        }
    }
}
