using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TutorialAJ : MonoBehaviour
{

    GameObject player;

    public string scene = "Level3";
    public Color loadToColor = Color.white;
    public float speed = 3.0f;

    //AJ Rotation Params
    float rotationSpeed = 0.5f;
    Quaternion originalRotation;
    bool triggerExit = false;

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
        //Get AJ's original Rotation Params
        originalRotation = transform.rotation;
        pushToTalk.gameObject.SetActive(false);
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (triggerExit)
        {
            StartCoroutine(TurnToOriginalPosition());
        }
        else
        {
            StopCoroutine(TurnToOriginalPosition());
        }
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
                //initiale object rotation to face the player
                StartCoroutine(TurnTowardsPlayer());
                triggerExit = false;

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
                            startedDialogue = false;
                            dialogueManger.finished = false;

                            // Load Level 3 if tutorial complete 
                            if (finalDialogueFlag)
                            {
                                Initiate.Fade(scene, loadToColor, speed);
                            }
                            else if (generalDialogueFlag)
                            {
                                generalDialogueFlag = false;
                                collectionDialogueFlag = true;
                            }
                            else if (collectionDialogueFlag)
                            {
                                canCollectLaptop = true;
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
            //used to initiate TurnToOriginalPosition Coroutine
            triggerExit = true;

            pushToTalk.gameObject.SetActive(false);
            FindObjectOfType<DialogueManager>().EndDialogue();
            continueTalking = true;
            startedDialogue = false;
            dialogueManger.finished = false;
            StopAllCoroutines();
        }
    }

    IEnumerator TurnTowardsPlayer()
    {
        Vector3 playerPosition = player.transform.position;
        Vector3 npcCurrentPosition = transform.position;

        Quaternion targetRotation = Quaternion.LookRotation(playerPosition - npcCurrentPosition);

        //keep rotating while the NPC is not facing the player
        while (transform.rotation != targetRotation)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            transform.localEulerAngles = new Vector3(0, transform.localEulerAngles.y, 0);

            yield return null;
        }

        yield return new WaitForSeconds(3.0f);
    }

    IEnumerator TurnToOriginalPosition()
    {
        //keep rotating while the NPC is not facing the player
        while (transform.rotation != originalRotation)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, originalRotation, rotationSpeed * Time.deltaTime);
            transform.localEulerAngles = new Vector3(0, transform.localEulerAngles.y, 0);

            yield return null;
        }

        yield return new WaitForSeconds(3.0f);
    }
}