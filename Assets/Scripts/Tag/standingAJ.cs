using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class standingAJ : MonoBehaviour
{
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

    private IEnumerator dialogueCoroutine;

    public int typesOfObjects = 4;
    bool[] activeArray;

    // Start is called before the first frame update
    void Start()
    {
        // Indecies for categores
        indexArray = new int[] { tabletsIndex, calculatorsIndex, notebooksIndex, laptopsIndex };
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
