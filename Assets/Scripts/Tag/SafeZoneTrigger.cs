using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeZoneTrigger : MonoBehaviour
{
    public bool canDestroy = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // If game is over = player collected all objects
        if (GameObject.FindWithTag("AJ").GetComponent<standingAJ>().finishedCollecting)
        {
            if(canDestroy)
            {
                canDestroy = false; 

                // Destroy object
                Destroy(this.gameObject);
            }
        }
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameObject.FindWithTag("Chad").GetComponent<TagAI>().safeZone = true;
        }
    }

    void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameObject.FindWithTag("Chad").GetComponent<TagAI>().safeZone = true;
        }
    }

    void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameObject.FindWithTag("Chad").GetComponent<TagAI>().safeZone = false;
        }
    }
}
