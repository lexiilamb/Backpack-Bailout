using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TutorialAJ : MonoBehaviour
{
    public string scene = "Level3";
    public Color loadToColor = Color.white;
    public float speed = 3.0f;

    public DialogueTrigger generalInstructions;
    public DialogueTrigger collectAndPlaceInstructions;
    public DialogueTrigger finalNotes;

    public bool generalDialogueFlag = true;
    public bool collectionDialogueFlag = false;
    public bool finalDialogueFlag = false;

    public bool canCollectLaptop = false;

    public DialogueManager dialogueManger;
    public Button pushToTalk;

    private IEnumerator dialogueCoroutine;

    // Dialogue flags
    private bool startedDialogue = false;
    private bool continueTalking = true;

    // Start is called before the first frame update
    void Start()
    {
        pushToTalk.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator startTalking()
    {
        yield return new WaitForSeconds(1);
        continueTalking = true;
    }

    void OnTriggerEnter(Collider collision)
    {
        // Prompt player to hit "Fire1" (left ctrl) to start dialogue
        if (collision.gameObject.tag == "Player")
        {
            pushToTalk.gameObject.SetActive(true);
            StopAllCoroutines();
        }
    }

    void OnTriggerStay(Collider collision)
    {
        // Start dialogue if player hits "Fire1"
        if (collision.gameObject.tag == "Player")
        {
            if (Input.GetButtonDown("Fire1"))
            {
                // Hide push to talk message after beginning conversation
                pushToTalk.gameObject.SetActive(false);

                // Prevent dialogue from restarting before finished
                if (continueTalking)
                {
                    continueTalking = false;

                    // Display the reset of the messages in NPC dialogue script
                    if (startedDialogue)
                    {
                        dialogueManger.DisplayNextSentence();

                        // If there are no more messages in NPC dialogue script
                        if (dialogueManger.finished)
                        {
                            Debug.Log("Finished dialogue");
                            startedDialogue = false;
                            dialogueManger.finished = false;

                            // Load Level3 if tutorial complete 
                            if(finalDialogueFlag)
                            {
                                Debug.Log("TIME TO PLAY");
                                Initiate.Fade(scene, loadToColor, speed);
                            }
                            if (generalDialogueFlag)
                            {
                                Debug.Log("Finished general");
                                generalDialogueFlag = false;
                                collectionDialogueFlag = true;
                            }
                        }
                    }

                    // If this is the first message in NPC dialogue script 
                    else
                    {
                        startedDialogue = true;

                        // Intro dialogue  
                        if (generalDialogueFlag == true)
                        {
                            generalInstructions.TriggerDialogue();
                        }
                        // Collection dialogue after intro has been played through
                        // Allows laptop to be collected
                        if (collectionDialogueFlag == true)
                        {
                            canCollectLaptop = true;
                            collectAndPlaceInstructions.TriggerDialogue();
                        }
                        // Final remarks dialogue after object has been placed in storage
                        if (finalDialogueFlag == true)
                        {
                            finalNotes.TriggerDialogue();
                        }

                        dialogueCoroutine = startTalking();
                        StartCoroutine(dialogueCoroutine);
                        dialogueManger.DisplayNextSentence();
                    }
                }
            }

            dialogueCoroutine = startTalking();
            StartCoroutine(dialogueCoroutine);
        }
    }

    void OnTriggerExit(Collider collision)
    {
        // End dialogue upon player exit
        if (collision.gameObject.tag == "Player")
        {
            pushToTalk.gameObject.SetActive(false);
            FindObjectOfType<DialogueManager>().EndDialogue();
            continueTalking = true;
            startedDialogue = false;
            dialogueManger.finished = false;
            StopAllCoroutines();
        }
    }
}
