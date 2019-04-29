using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class NPCdialogue : MonoBehaviour
{

    //player and AJ
    GameObject player;


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

    //NPC Rotation Params
    float rotationSpeed = 0.5f;
    Quaternion originalRotation;
    bool triggerExit = false;

    // Start is called before the first frame update
    void Start()
    {
		//store NPC original rotation
        originalRotation = transform.rotation;
		
        // Get karma score from main player
        karma = GameObject.FindWithTag("Player").GetComponent<PlayerBehavior>().count;
        difficulty = GameDifficulty.gameDifficulty;
        finishedDialogue = dialogueManger.finished;

        pushToTalk.gameObject.SetActive(false);

        // Set karmaNeededToProceed based on game difficulty
        karmaNeededToProceed = 5;

        //initiate player
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
		if(triggerExit)
        {
            StartCoroutine(TurnToOriginalPosition());
        }
        else
        {
            StopCoroutine(TurnToOriginalPosition());
        }
		
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
	
	IEnumerator TurnTowardsPlayer()
    {
        Vector3 playerPosition = player.transform.position;
        Vector3 npcCurrentPosition = transform.position;

        Quaternion targetRotation = Quaternion.LookRotation(playerPosition - npcCurrentPosition);

        //keep rotating while the NPC is not facing the player
        while(transform.rotation != targetRotation)
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
                    // Initiate object rotation to face the player
					StartCoroutine(TurnTowardsPlayer());
					triggerExit = false;

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
            triggerExit = true;
			
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
