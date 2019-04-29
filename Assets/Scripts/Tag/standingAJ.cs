using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class standingAJ : MonoBehaviour
{
    //player and AJ
    GameObject player;
    GameObject AJ;

    // Indecies for categories 
    private int tabletsIndex = 0;
    private int calculatorsIndex = 1;
    private int notebooksIndex = 2;
    private int laptopsIndex = 3;
    private int[] indexArray;

    // Boolean array to indicate items can be collected
    public bool[] canCollectFlags = new bool[] { false, false, false, false };
    public bool[] ajDialogueCategoriesFlags = new bool[] { false, false, false, false};

    public DialogueTrigger tabletDialogue;
    public DialogueTrigger calculatorDialogue;
    public DialogueTrigger notebookDialogue;
    public DialogueTrigger laptopDialogue;
    public DialogueTrigger finished;

    public bool canPickNextCategory = true;
    public bool finishedCollecting = false;

    public DialogueManager dialogueManger;
    public Button pushToTalk;

    // Dialogue flags
    private bool startedDialogue = false;
    private bool finishedDialogue = false;
    private bool continueTalking = true;
    // Check if AJ has gone through all categories
    private bool allTrue = true;

    private IEnumerator dialogueCoroutine;

    public int typesOfObjects = 4;
    bool[] activeArray;

    //AJ Rotation Params
    float rotationSpeed = 0.5f;
    Quaternion originalRotation;
    bool triggerExit = false;

    // Start is called before the first frame update
    void Start()
    {
		//Get AJ's original Rotation Params
        originalRotation = transform.rotation;
		
        // Indecies for categores
        indexArray = new int[] { tabletsIndex, calculatorsIndex, notebooksIndex, laptopsIndex };
        activeArray = new bool[typesOfObjects];
        finishedDialogue = dialogueManger.finished;
        pushToTalk.gameObject.SetActive(false);

        //initiate player
        player = GameObject.FindGameObjectWithTag("Player");
        AJ = GameObject.FindGameObjectWithTag("AJ");
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

    public bool checkIfAllCategoriesCollected()
    {
        allTrue = true;
        for(int i = 0; i < activeArray.Length; i++)
        {
            if(!activeArray[i])
            {
                allTrue = false;
                break;
            }
        }

        return allTrue;
    }

    public void pickObjectCategory()
    {
        bool pickedOne = false;

        if(canPickNextCategory)
        {
            canPickNextCategory = false;
            while (!pickedOne)
            {
                // Pick random number
                int randNumber = Random.Range(0, typesOfObjects);
                if (!activeArray[randNumber])
                {
                    pickedOne = true;
                    // Save the index of item chosen
                    activeArray[randNumber] = true;
                    foreach (int categoryIndex in indexArray)
                    {
                        // Set booleans to true
                        if (randNumber == categoryIndex)
                        {
                            ajDialogueCategoriesFlags[categoryIndex] = true;
                            canCollectFlags[categoryIndex] = true;
                        }
                    }
                }
            }
        }
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
                // Initiate object rotation to face the player
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
                           
                            // Display win screen after finishing game and talking to AJ
                            if (finishedCollecting)
                            {
                              
                                GameObject.FindWithTag("Player").GetComponent<PlayerBehavior>().wonGame = true;
                            }

                            startedDialogue = false;
                            dialogueManger.finished = false;
                        }
                    }

                    // If this is the first message in NPC dialogue script 
                    else
                    {
                        startedDialogue = true;

                        // Check if collection is finished or if new category should be picked
                        if(finishedCollecting)
                        {
                            finished.TriggerDialogue();
                        }
                        else
                        {
                            // Pick category to find
                            pickObjectCategory();

                            // if finding tablets
                            if (ajDialogueCategoriesFlags[tabletsIndex] == true)
                            {
                                tabletDialogue.TriggerDialogue();
                            }
                            // if finding calculators
                            if (ajDialogueCategoriesFlags[calculatorsIndex] == true)
                            {
                                calculatorDialogue.TriggerDialogue();
                            }
                            // if finding notebooks
                            if (ajDialogueCategoriesFlags[notebooksIndex] == true)
                            {
                                notebookDialogue.TriggerDialogue();
                            }
                            // if finding laptops
                            if (ajDialogueCategoriesFlags[laptopsIndex] == true)
                            {
                                laptopDialogue.TriggerDialogue();
                            }
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
			// Used to initiate TurnToOriginalPosition Coroutine
            triggerExit = true;
			
            pushToTalk.gameObject.SetActive(false);
            FindObjectOfType<DialogueManager>().EndDialogue();
            continueTalking = true;
            startedDialogue = false;
            dialogueManger.finished = false;
            StopAllCoroutines();
        }
    }
}
