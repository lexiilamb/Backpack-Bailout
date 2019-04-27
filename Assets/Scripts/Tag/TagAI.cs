﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TagAI : MonoBehaviour
{
    GameObject player;
    Transform destinationObject;
    public GameObject items;
    NavMeshAgent tagAI;
    Animator ChadAnimationController;
    public int randNumber = 0;
    public bool newDestination = true;
    public bool wandering = true;
    public bool ChadLeftDestination = true;
    public bool safeZone = false;

    //win-lose state
    public bool chadCaughtPlayer = false;

    private int difficulty = 0;

    enum Animation
    {
        ANGRY_WALK = 0,
        PUNCH = 1,
        RUN = 2,
        TURN_LEFT = 3,
        TURN_RIGHT = 4
    }

    //parameters for RandomNavSphere for NPC to wander when player is in the safe zone
    public float maxRadius = 40.0f;

    //add view cone 
    FieldOfView target;

    //NavAgent Speed Parameters
    enum Speed
    {
        ZERO = 0,
        NORMAL = 3,
        FAST = 5
    }

    // Start is called before the first frame update
    void Start()
    {
        difficulty = GameDifficulty.gameDifficulty;
        /*
        // Easy
        if (difficulty == 0)
        {
            Speed.NORMAL = 2;
            Speed.FAST = 3;
        }
        // Medium
        if (difficulty == 1)
        {
            Speed.NORMAL = 3;
            Speed.FAST = 5;
        }
        // Hard
        if (difficulty == 2)
        {
            Speed.NORMAL = 4;
            Speed.FAST = 7;
        }

        Debug.Log("NORMAL: " + Speed.NORMAL);
        Debug.Log("FAST: " + Speed.FAST);
        */

        player = GameObject.FindGameObjectWithTag("Player");


        ChadAnimationController = GetComponent<Animator>();

        if (player == null)
        {
            Debug.Log("Player Tag not found");
        }

        tagAI = GetComponent<NavMeshAgent>();
        target = GetComponent<FieldOfView>();
    }


    //Runs every frame
    void Update()
    {
        if (player != null)
        {
            //If the target is in the safezone and not viewable then peruse (look around) the room
            //If the target is not in the safezone then check if it is within the viewcone
            //  If the target is within the viewcone then check if NPC is within the stopping distance
            //      If NPC is within the stopping distance then stop and punch the target
            //      else run towards the target at a faster speed
            //  If the target is not within the viewcone and not in the safezone then walk around the room looking for the target
            if (safeZone)
            {
                if (wandering)
                {
                    Wander();
                }
            }
            else
            {
                // Can find a new object to wander to next time Wander is executed
                newDestination = true;

                if (target.viewable)
                {
                    if (tagAI.remainingDistance <= tagAI.stoppingDistance)
                    {
                        // Let PlayerLives script know a heart needs to be removed
                        chadCaughtPlayer = true;
                        Punch();
                    }
                    else
                    {
                        Chase();
                    }
                }
                else if (!target.viewable)
                {
                    //pursue the target
                    Pursue();
                }
            }
        }
    }

    IEnumerator TurnChad(int directionToTurn)
    {
        ChadLeftDestination = false;

        //stop chad from moving
        tagAI.speed = (float)Speed.ZERO;

        ChadAnimationController.SetInteger("Movement", directionToTurn);

        yield return new WaitForSeconds(3.0f);

        newDestination = true;
        wandering = true;
    }

    private void Wander()
    {

        if (newDestination)
        {
            newDestination = false;
            randNumber = UnityEngine.Random.Range(0, 8);
        }

        destinationObject = items.transform.GetChild(randNumber);
        tagAI.speed = (float)Speed.NORMAL;
        tagAI.transform.LookAt(new Vector3(destinationObject.position.x, transform.position.y, destinationObject.position.z));
        ChadAnimationController.SetInteger("Movement", (int)Animation.ANGRY_WALK);
        tagAI.destination = destinationObject.position;
    }

    private void Punch()
    {
        //Move fast when player is visible
        tagAI.speed = (float)Speed.FAST;
        ChadAnimationController.SetInteger("Movement", (int)Animation.PUNCH);
        tagAI.destination = player.transform.position;
    }

    private void Chase()
    {
        //Move fast when player is visible
        tagAI.speed = (float)Speed.FAST;
        ChadAnimationController.SetInteger("Movement", (int)Animation.RUN);
        tagAI.destination = player.transform.position;
    }

    private void Pursue()
    {
        //Move slow when player not visible
        tagAI.speed = (float)Speed.NORMAL;
        tagAI.transform.LookAt(new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z));
        ChadAnimationController.SetInteger("Movement", (int)Animation.ANGRY_WALK);
        tagAI.destination = player.transform.position;
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "ChadWanderObject" && ChadLeftDestination)
        {
            wandering = false;
            StartCoroutine(TurnChad((int)Animation.TURN_RIGHT));
        }
    }

    void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag == "ChadWanderObject")
        {
            ChadLeftDestination = true;
        }
    }
}