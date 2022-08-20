using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueBox : MonoBehaviour
{
    public static float TextSpeed = 0.05f;
    
    [System.NonSerialized]
    public bool FinalDialogue = false;
    public bool MeniraTrigger = false;
    public bool HasOptions = false;
    public bool ForceUnlockPlayer = false;

    public TextMeshProUGUI TMText = null;
    [TextArea]
    public string DialogueText = "";

    public GameObject NextDialogue = null;

    public VariationAudioPlayer SoundPlayer = null;

    int textIndex = -1;
    bool finished = false;
    float timer = 0.0f;

    void Awake()
    {
        SetButtonsActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(!finished)
        {
            if(Input.GetMouseButtonUp(0))
            {
                finished = true;
                SetButtonsActive(true);
                TMText.text = DialogueText;
                return;
            }

            timer += Time.deltaTime;
            if (timer >= TextSpeed)
            {
                ++textIndex;
                if (textIndex >= DialogueText.Length)
                {
                    finished = true;
                    SetButtonsActive(true);
                }
                else
                {
                    TMText.text += DialogueText[textIndex];
                    if(SoundPlayer)
                    {
                        SoundPlayer.PlaySound();
                    }
                }
                timer = 0.0f;
            }
        }
        else
        {
            if(Input.GetMouseButtonUp(0) && !HasOptions)
            {
                FinishDialogue();
            }
        }
        
    }

    public void FinishDialogue()
    {
        if (NextDialogue == null)
        {
            if (FinalDialogue)
            {
                GameObject.Find("DialogueManager").GetComponent<DialogueManager>().FinalMeniraTrigger.TriggerDialogue();
            }
            if (MeniraTrigger)
            {
                //GameObject.Find("Menira") - make him move
            }
        }
        else
        {
            GameObject next = Instantiate(NextDialogue, transform.position, Quaternion.identity, transform.parent);
            next.GetComponent<DialogueBox>().FinalDialogue = FinalDialogue;
            next.GetComponent<DialogueBox>().MeniraTrigger = MeniraTrigger;
        }

        if(NextDialogue == null || ForceUnlockPlayer)
        {
            GameObject.Find("Player").GetComponent<PlayerController>().Locked = false;
        }

        Destroy(gameObject);
    }

    void SetButtonsActive(bool active)
    {
        if (HasOptions)
        {
            for(int i = 0; i < transform.childCount; ++i)
            {
                DialogueButtonBehavior dbb = transform.GetChild(i).GetComponent<DialogueButtonBehavior>();
                if(dbb)
                {
                    dbb.gameObject.SetActive(active);
                }
            }
        }
    }
}
