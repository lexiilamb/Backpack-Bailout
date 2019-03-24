using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class standingAJ : MonoBehaviour
{
    public bool canCollectTablets = false;
    public bool canCollectCalculators = false;
    public bool canCollectNotebooks = false;
    public bool canCollectLaptops = false;

    public DialogueTrigger tabletDialogue;
    public DialogueTrigger calculatorDialogue;
    public DialogueTrigger notebookDialogue;
    public DialogueTrigger laptopDialogue;
    public DialogueTrigger finished;

    public bool findTables = false;
    public bool findCalculators = false;
    public bool findNotebooks = false;
    public bool findLaptops = false;
    public bool canPickNextCategory = true;
    public bool finishedCollecting = false;

    public DialogueManager dialogueManger;
    public Text pushToTalk;

    private IEnumerator dialogueCoroutine;

    // Dialogue flags
    private bool startedDialogue = false;
    private bool finishedDialogue = false;
    private bool continueTalking = true;

    public int typesOfObjects = 4;
    bool[] activeArray;

    // Start is called before the first frame update
    void Start()
    {
        activeArray = new bool[typesOfObjects];

        finishedDialogue = dialogueManger.finished;

        pushToTalk.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        finishedDialogue = dialogueManger.finished;
    }

    public void pickObjectCategory()
    {
        bool pickedOne = false;

        // If all categories asked for
        if(activeArray[0] && activeArray[1] && activeArray[2] && activeArray[3])
        {
            pickedOne = true;
            finishedCollecting = true;
        }

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
                    Debug.Log("picked one: " + randNumber);
                    // Save the index of item chosen
                    activeArray[randNumber] = true;
                    // Set boolean to true
                    if (randNumber == 0)
                    {
                        findTables = true;
                        canCollectTablets = true;
                    }
                    if (randNumber == 1)
                    {
                        findCalculators = true;
                        canCollectCalculators = true;
                    }
                    if (randNumber == 2)
                    {
                        findNotebooks = true;
                        canCollectNotebooks = true;
                    }
                    if (randNumber == 3)
                    {
                        findLaptops = true;
                        canCollectLaptops = true;
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
        // Start dialogue if player hits "Fire1" (left ctrl)
        if (collision.gameObject.tag == "Player")
        {
            if (Input.GetButtonDown("Fire1"))
            {
                // Hide push to talk message after beginning conversation
                pushToTalk.gameObject.SetActive(false);

                // Also disable text/button that prompted player to hit button
                if (continueTalking)
                {
                    continueTalking = false;

                    // Display the reset of the messages in NPC dialogue script
                    if (startedDialogue)
                    {
                        dialogueManger.DisplayNextSentence();

                        // If there are no more messages in NPC dialogue script
                        if (finishedDialogue)
                        {
                            startedDialogue = false;
                            dialogueManger.finished = false;

                            // Resume player movement 
                            GameObject.FindWithTag("Player").GetComponent<CharacterControl>().changeAltToggle = true;
                        }
                    }

                    // If this is the first message in NPC dialogue script 
                    else
                    {
                        startedDialogue = true;

                        // Pick category to find
                        pickObjectCategory();

                        // if finding tablets
                        if (findTables == true)
                        {
                            tabletDialogue.TriggerDialogue();
                            //findTables = false;
                        }
                        // if finding calculators
                        if (findCalculators == true)
                        {
                            calculatorDialogue.TriggerDialogue();
                            //findCalculators = false;
                        }
                        // if finding notebooks
                        if (findNotebooks == true)
                        {
                            notebookDialogue.TriggerDialogue();
                            //findNotebooks = false;
                        }
                        // if finding laptops
                        if (findLaptops == true)
                        {
                            laptopDialogue.TriggerDialogue();
                            //findLaptops = false;
                        }
                        // if finding laptops
                        if (finishedCollecting == true)
                        {
                            finished.TriggerDialogue();
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
