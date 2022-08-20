using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    [System.NonSerialized]
    public int TotalDialogueTriggers = 0;
    [System.NonSerialized]
    public int DialoguesTriggered = 0;

    public DialogueTrigger FinalMeniraTrigger = null;
}
