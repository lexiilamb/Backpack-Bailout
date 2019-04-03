using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class roaming : MonoBehaviour
{
    Transform target;

    Animator animator;
    public AudioSource _AudioSource;

    private bool isWandering = false;
    private bool isRotating = false;
    private bool isWalking = false;
    private bool isStanding = false;
    private bool pathIsBlocked = false;
    private IEnumerator wanderCoroutine;
    private IEnumerator avoidCoroutine;

    public float rotSpeed = 1.0f;
    private float rotationsPerMinute = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        // Used to make NPC face player on collision 
        target = GameObject.FindWithTag("Player").transform;

        // NPC initially starts in an idle state
        animator.SetInteger("Switch", 1);

        // Initialize coroutines for later use
        wanderCoroutine = Wander();
        avoidCoroutine = avoidObstacles();
    }

    // Update is called once per frame
    void Update()
    {
        // Begin wandering coroutine if not already wandering (isWandering = false)
        if (!isWandering)
        {
            wanderCoroutine = Wander();
            StartCoroutine(wanderCoroutine);
        }

        // Idle NPC
        if (isStanding)
        {
            animator.SetInteger("Switch", 1);
        }

        // Rotate until isRotating = false
        if (isRotating)
        {
            animator.SetInteger("Switch", 2);
            transform.Rotate(0, 6.0f * rotationsPerMinute * Time.deltaTime, 0);  
        }

        // Walk forward until isWalking = false
        if (isWalking)
        {
            transform.Translate(0, 0, Time.deltaTime);
        }

        // Start avoid coroutine if path is blocked 
        if (pathIsBlocked)
        {
            avoidCoroutine = avoidObstacles();
            StartCoroutine(avoidCoroutine);
        }
    }

    // Avoidance coroutine
    IEnumerator avoidObstacles()
    {
        pathIsBlocked = false;
        // Random amount of time to turn for
        int rotTime = Random.Range(1, 2);

        // Stop other actions before turning
        isWalking = isRotating = false;

        // Turn; switch = 2 is turn animation
        isRotating = true;
        yield return new WaitForSeconds(rotTime);
        isRotating = false;

    }

    // Wandering coroutine
    IEnumerator Wander()
    {
        int rotTime = Random.Range(1, 2);
        int rotateWait = Random.Range(1, 3);
        int walkWait = Random.Range(1, 2);
        int walkTime = Random.Range(1, 5);

        // Set NPC to idle; switch = 1 is idle animation
        isWandering = true;
        animator.SetInteger("Switch", 1);
        yield return new WaitForSeconds(walkWait);

        // Make NPC walk; switch = 0 is walk animation
        animator.SetInteger("Switch", 0);
        isWalking = true;
        yield return new WaitForSeconds(walkTime);
        isWalking = false;

        // Wait; switch = 1 is idle animation
        animator.SetInteger("Switch", 1);
        yield return new WaitForSeconds(rotateWait);

        // Turn; switch = 2 is turn animation
        isRotating = true;
        yield return new WaitForSeconds(rotTime);

        isRotating = false;
        isWandering = false;
    }

    void OnTriggerEnter(Collider collision)
    {
        // Stop all other movements upon player collision
        // Activate dialogue or audio accordingly 
        if (collision.gameObject.tag == "Player")
        {
            _AudioSource.Play();

            isWalking = isRotating = false;
            isStanding = true;
            StopCoroutine(wanderCoroutine);
        }

        else
        {
            // Turn for random amount if running into something
            pathIsBlocked = true;
        }
    }

    void OnTriggerStay(Collider collision)
    {
        // Face player if collided with player
        if (collision.gameObject.tag == "Player")
        {
            isWalking = isRotating = false;
            isStanding = true;
            StopCoroutine(wanderCoroutine);

            var step = rotSpeed * Time.deltaTime;
            Vector3 direction = (target.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, step);
        }
    }

    void OnTriggerExit(Collider collision)
    {
        // Return to wandering corounte upon player exit
        if (collision.gameObject.tag == "Player")
        {
            isStanding = isWandering = false;
        }
    }
}
