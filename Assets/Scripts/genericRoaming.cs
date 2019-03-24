using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class genericRoaming : MonoBehaviour
{
    Animator animator;
    private bool isWandering = false;
    private bool isRotating = false;
    private bool isWalking = false;
    private bool stayIdle = false;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetInteger("Switch", 1);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isWandering)
        {
            StartCoroutine(Wander());
        }

        if (isRotating)
        {
            if (!stayIdle)
            {
                transform.Rotate(0, 180, 0);
                animator.SetInteger("Switch", 2);
                stayIdle = true;
            }

            else
            {
                animator.SetInteger("Switch", 1);
            }
        }

        if (isWalking)
        {
            transform.Translate(0, 0, Time.deltaTime);
        }
    }

    IEnumerator Wander()
    {
        int rotTime = Random.Range(1, 3);
        int rotateWait = Random.Range(1, 3);
        int walkWait = Random.Range(1, 2);
        int walkTime = Random.Range(1, 5);

        isWandering = true;
        animator.SetInteger("Switch", 1);
        yield return new WaitForSeconds(walkWait);
        isWalking = true;
        animator.SetInteger("Switch", 0);
        yield return new WaitForSeconds(walkTime);
        isWalking = false;
        animator.SetInteger("Switch", 1);
        yield return new WaitForSeconds(rotateWait);


        isRotating = true;
        yield return new WaitForSeconds(rotTime);
        isRotating = false;
        stayIdle = false;

        isWandering = false;
    }
}
