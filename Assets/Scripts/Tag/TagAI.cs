using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TagAI : MonoBehaviour
{
    private IEnumerator changeDirectionFacing;
    public bool checkDirectionChange = true;
    public float waitTime = 4.0f;
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

    GameObject player;
    NavMeshAgent tagAI;
    Animator ChadAnimationController;
    public bool safeZone = false;

    //add view cone 
    FieldOfView target;

    //acceleration speeds
    private float stopSpeed = 0.0f;
    private float standardSpeed = 6.0f;
    private float acceleratedSpeed = 10.0f;

    //NPC rotations
    private bool turnedRight = false;

    //win-lose state
    public bool chadCaughtPlayer = false;


    // Start is called before the first frame update
    void Start()
    {
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
            //If the target is in the safezone then peruse (look around) the room
            //If the target is not in the safezone then check if it is within the viewcone
            //  If the target is within the viewcone then check if NPC is within the stopping distance
            //      If NPC is within the stopping distance then stop and punch the target
            //      else run towards the target at an accelerated speed
            //  If the target is not within the viewcone then walk around the room looking for the target
            if (safeZone)
                Peruse();
            else
                if (target.viewable)
                    if (tagAI.remainingDistance <= tagAI.stoppingDistance)
                        Punch();
                else
                    Chase();
            else
                Pursue();

        }

    }

    IEnumerator TurnChad(int directionToTurn)
    {
        ChadAnimationController.SetInteger("Movement", directionToTurn);
        Debug.Log("waiting");
        yield return new WaitForSeconds(3.0f);

        // Negate the current value of turnedRight
        // Switches to the opposite direction to turn
        turnedRight = !turnedRight;
        Debug.Log(turnedRight);
        checkDirectionChange = true;
    }

    private void Pursue()
    {
        //Move slow when player not visible
        tagAI.acceleration = standardSpeed;

        tagAI.transform.LookAt(new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z));

        ChadAnimationController.SetInteger("Movement", (int)Animation.ANGRY_WALK);

        tagAI.destination = player.transform.position;
    }

    private void Punch()
    {
        //Move fast when player is visible
        tagAI.acceleration = acceleratedSpeed;

        ChadAnimationController.SetInteger("Movement", (int)Animation.PUNCH);

        tagAI.destination = player.transform.position;

        chadCaughtPlayer = true;
    }

    private void Chase()
    {
        //Move fast when player is visible
        tagAI.acceleration = acceleratedSpeed;

        ChadAnimationController.SetInteger("Movement", (int)Animation.RUN);

        tagAI.destination = player.transform.position;
    }

    private void Peruse()
    {
        //stop chad from moving
        tagAI.acceleration = stopSpeed;

        if (checkDirectionChange)
        {
            checkDirectionChange = false;
            //if the player is in the safe zone then look for the player left and right
            if (turnedRight)
            {
                changeDirectionFacing = TurnChad((int)Animation.TURN_LEFT);
                StartCoroutine(changeDirectionFacing);
            }
            else
            {
                changeDirectionFacing = TurnChad((int)Animation.TURN_RIGHT);
                StartCoroutine(changeDirectionFacing);
            }
        }

        //tagAI.destination = tagAI.transform.position;
    }

    //generate random point in a sphere and move to that point
    public static Vector3 RandomNavSphere(Vector3 origin, float distance)
    {
        Vector3 randomDirection = UnityEngine.Random.insideUnitSphere * distance + origin;

        //mask to the walkable layer only
        int layermask = 1 << NavMesh.GetAreaFromName("Walkable");

        NavMeshHit navHit;

        NavMesh.SamplePosition(randomDirection, out navHit, distance, layermask);

        return navHit.position;
    }
}