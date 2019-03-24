using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameDialogue : MonoBehaviour
{
    public DialogueTrigger start;
    public DialogueManager dialogueManger;
    private bool dialogueDisplayed = false;
    private bool done = false;

    // Start is called before the first frame update
    void Start()
    {
        // Stop player movement 
        GameObject.FindWithTag("Player").GetComponent<CharacterControl>().changeAltToggle = true;
    }

    // Update is called once per frame
    void Update()
    {
        // Done keeps start dialogue from displaying again
        if (!done)
        {
            // If start dialogue has not been displayed
            if (!dialogueDisplayed)
            {
                // "Fire1" is Left ctrl
                if (Input.GetButtonDown("Fire1"))
                {
                    start.TriggerDialogue();
                    dialogueDisplayed = true;
                }
            }

            // "Fire1" is Left ctrl
            if (Input.GetButtonDown("Fire1"))
            {
                dialogueManger.DisplayNextSentence();
            }


            if(dialogueManger.finished)
            {
                // Resume player movement 
                GameObject.FindWithTag("Player").GetComponent<CharacterControl>().changeAltToggle = true;
                done = true;
                Destroy(this.gameObject);
            }
        }
    }
}
