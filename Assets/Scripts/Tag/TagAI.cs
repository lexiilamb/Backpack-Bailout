using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TagAI : MonoBehaviour
{
    //walkable dimension of the room
    private float roomXlowerValue = -9.1f;
    private float roomXupperValue = 48f;
    private float roomZlowerValue = -9.9f;
    private float roomZupperValue = 16.5f;

    //parameters for RandomNavSphere for NPC to wander when player is in the safe zone
    public float maxRadius = 40.0f;

    GameObject player;
    NavMeshAgent tagAI;
    Animator animCon;
    public bool safeZone = false;

    //add view cone 
    FieldOfView target;

    //acceleration speeds
    float standardSpeed = 6.0f;
    float acceleratedSpeed = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        animCon = GetComponent<Animator>();

        if (player == null)
        {
            Debug.Log("Player Tag not found");
        }

        tagAI = GetComponent<NavMeshAgent>();
        target = GetComponent<FieldOfView>();
    }

    //new Update with view Cone implementation
    void Update()
    {
        if (player != null)
        {
            //check if the player is in the safezone
            if(safeZone)
            {
                tagAI.acceleration = standardSpeed;
                //if the player is in the safe zone then walk around looking for the player
                animCon.SetBool("isRunning", false);
                animCon.SetBool("isPunchable", false);
                animCon.SetBool("isWalkable", true);
                tagAI.destination = RandomNavSphere(tagAI.transform.position, maxRadius);
            }
            else 
            {
                //if player is not in the safezone then check if the player is viewable
                if(target.viewable)
                {
                    //if the player is viewable and within distance then punch
                    if (tagAI.remainingDistance <= tagAI.stoppingDistance)
                    {
                        tagAI.acceleration = standardSpeed;
                        animCon.SetBool("isRunning", false);
                        animCon.SetBool("isPunchable", true);
                    }
                    //if the player is viewable and not within distance then run with an accelerated speed
                    else
                    {
                        tagAI.acceleration = acceleratedSpeed;

                        animCon.SetBool("isRunning", true);
                        animCon.SetBool("isPunchable", false);
                    }

                    animCon.SetBool("isWalkable", false);
                    tagAI.destination = player.transform.position;
                }
                else
                {
                    //if player is not viewable then walk towards the player and set chad's acceleration to standard acceleration
                    tagAI.acceleration = standardSpeed;

                    animCon.SetBool("isRunning", false);
                    animCon.SetBool("isPunchable", false);
                    animCon.SetBool("isWalkable", true);
                    tagAI.destination = player.transform.position;
                }

            }
        }

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