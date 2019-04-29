using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class NPCdialogue : MonoBehaviour
{

    //player and AJ
    GameObject player;
    GameObject NPC;
    GameObject NoticeBoard;


    public DialogueTrigger low;
    public DialogueTrigger high;
    public DialogueManager dialogueManger;
    public Button pushToTalk;
    private IEnumerator dialogueCoroutine;

    private int karma = 0;
    private int difficulty = 1;
    public int karmaNeededToProceed;

    // Dialogue flags
    private bool startedDialogue = false;
    private bool finishedDialogue = false;
    private bool continueTalking = true;
    private bool playerMovementStopped = false;
    private bool pleaseDontRepeatDialogue = true;


    // Start is called before the first frame update
    void Start()
    {
        // Get karma score from main player
        karma = GameObject.FindWithTag("Player").GetComponent<PlayerBehavior>().count;
        difficulty = GameDifficulty.gameDifficulty;
        finishedDialogue = dialogueManger.finished;

        pushToTalk.gameObject.SetActive(false);

        // Set karmaNeededToProceed based on game difficulty
        // Easy
        if(difficulty == 0)
        {
            karmaNeededToProceed = 2;
        }
        // Medium
        if (difficulty == 1)
        {
            karmaNeededToProceed = 4;
        }
        // Hard
        if (difficulty == 2)
        {
            karmaNeededToProceed = 7;
        }

        //initiate player
        player = GameObject.FindGameObjectWithTag("Player");
        NPC = GameObject.FindGameObjectWithTag("NPC");
        NoticeBoard = GameObject.FindGameObjectWithTag("NoticeBoard");
    }

    // Update is called once per frame
    void Update()
    {
        // Update karma score from main player
        karma = GameObject.FindWithTag("Player").GetComponent<PlayerBehavior>().count;
        finishedDialogue = dialogueManger.finished;
        //Debug.Log("update : " + startedDialogue);
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
        // Start dialogue if player hits "Fire1" (F)
        if (collision.gameObject.tag == "Player")
        {
            if(pleaseDontRepeatDialogue)
            {
                if (Input.GetButtonDown("Fire1"))
                {
                    //look at the player 
                    NPC.transform.LookAt(new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z));

                    // Hide push to talk message after beginning conversation
                    pushToTalk.gameObject.SetActive(false);

                    if (!playerMovementStopped)
                    {
                        // Stop player movement 
                        GameObject.FindWithTag("Player").GetComponent<CharacterControl>().changeAltToggle = true;
                        playerMovementStopped = true;
                    }

                    // Also disable text/button that prompted player to hit button
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

                                // Resume player movement 
                                GameObject.FindWithTag("Player").GetComponent<CharacterControl>().changeAltToggle = true;
                                playerMovementStopped = false;

                                GameObject.FindWithTag("Player").GetComponent<PlayerBehavior>().SetCountText();
                                pleaseDontRepeatDialogue = false;
                            }
                        }

                        // If this is the first message in NPC dialogue script 
                        else
                        {
                            startedDialogue = true;

                            // if collosion.karam < karmaNeededToProceed
                            if (karma > karmaNeededToProceed)
                            {

                                if (gameObject.tag == "Claire")
                                {
                                    GameObject.FindWithTag("Player").GetComponent<PlayerBehavior>().claireAskedForHelp = true;
                                }

                                high.TriggerDialogue();
                                dialogueCoroutine = startTalking();
                                StartCoroutine(dialogueCoroutine);
                                dialogueManger.DisplayNextSentence();
                            }
                            // if collosion.karam >= karmaNeededToProceed
                            else
                            {
                                low.TriggerDialogue();
                                dialogueCoroutine = startTalking();
                                StartCoroutine(dialogueCoroutine);
                                dialogueManger.DisplayNextSentence();
                            }
                        }
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
            //go back to original position
            NPC.transform.LookAt(new Vector3(NoticeBoard.transform.position.x, transform.position.y, NoticeBoard.transform.position.z));


            pushToTalk.gameObject.SetActive(false);
            FindObjectOfType<DialogueManager>().EndDialogue();
            continueTalking = true;
            startedDialogue = false;
            dialogueManger.finished = false;
            StopAllCoroutines();
            pleaseDontRepeatDialogue = true;
        }
    }
}
