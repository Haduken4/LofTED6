using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public GameObject FirstDialogueBox = null;
    public bool Untriggerable = false;

    bool triggered = false;
    DialogueManager mgr = null;
    Transform canvas = null;
    Transform boxLoc = null;

    // Start is called before the first frame update
    void Start()
    {
        canvas = GameObject.Find("MainCanvas").transform;
        boxLoc = GameObject.Find("DialogueBoxLocation").transform;
        mgr = GameObject.Find("DialogueManager").GetComponent<DialogueManager>();
        if(!Untriggerable)
        {
            mgr.TotalDialogueTriggers += 1;
        }
    }

    public void TriggerDialogue()
    {
        if(!triggered)
        {
            triggered = true;
            mgr.DialoguesTriggered += 1;
            bool final = mgr.DialoguesTriggered == mgr.TotalDialogueTriggers;
            GameObject d = Instantiate(FirstDialogueBox, boxLoc.position, Quaternion.identity, canvas);
            d.GetComponent<DialogueBox>().FinalDialogue = final;

            GameObject.Find("Player").GetComponent<PlayerController>().Locked = true;
        }
    }
}
