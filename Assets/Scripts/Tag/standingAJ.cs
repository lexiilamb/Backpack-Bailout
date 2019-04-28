﻿using System.Collections;
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

    // Start is called before the first frame update
    void Start()
    {
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
        finishedDialogue = dialogueManger.finished;
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
                //look at the player 
                AJ.transform.LookAt(new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z));


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
                            // Display win screen after finishing game and talking to AJ
                            if (finishedCollecting)
                            {
                                GameObject.FindWithTag("Player").GetComponent<PlayerBehavior>().wonGame = true;
                            }

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
            pushToTalk.gameObject.SetActive(false);
            FindObjectOfType<DialogueManager>().EndDialogue();
            continueTalking = true;
            startedDialogue = false;
            dialogueManger.finished = false;
            StopAllCoroutines();
        }
    }
}
