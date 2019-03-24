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
    float maxRadius = 40.0f;

    GameObject player;
    NavMeshAgent tagAI;
    Animator animCon;
    public bool safeZone = false;


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
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            //if safe zone then wander
            if (safeZone)
            { 
                Vector3 newPosition = RandomNavSphere(tagAI.transform.position, maxRadius);
                
                tagAI.destination = newPosition;

                animCon.SetBool("isRunning", false);
                animCon.SetBool("isPunchable", false);
                animCon.SetBool("isWalkable", true);
            }
            else if (tagAI.remainingDistance != Mathf.Infinity && tagAI.remainingDistance <= tagAI.stoppingDistance)
            {
                tagAI.destination = player.transform.position;
                animCon.SetBool("isRunning", false);
                animCon.SetBool("isPunchable", true);
                animCon.SetBool("isWalkable", false);
            }
            else
            {
                animCon.SetBool("isRunning", true);
                animCon.SetBool("isPunchable", false);
                animCon.SetBool("isWalkable", false);
                tagAI.destination = player.transform.position;
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