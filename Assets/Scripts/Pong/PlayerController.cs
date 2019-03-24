using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //public float speed;
    public float speed;
	
    float xPos;
    float yPos;
    float zPos;
	
    //left and right boundary positions
    float leftBoundary = -26.5f;
    float rightBoundary = 26.5f;
	
    // Start is called before the first frame update
    void Start()
    {
        xPos = 0.0f;
        yPos = 0.0f;
        zPos = 0.0f;
    }
	
    // Update is called once per frame
    void Update()
    {
        //get arrow input
        xPos = Input.GetAxis("Horizontal");
        //if the player is within the boundaries then add to x position
        if (transform.position.x > leftBoundary && transform.position.x < rightBoundary)
        {
            transform.position += new Vector3(xPos, yPos, zPos) * speed * Time.deltaTime;
        }
        //if the player is on right boundary then add twice of left boundary position so that it rolls over (buggy)
        else if (transform.position.x > rightBoundary)
        {
            transform.position += new Vector3(2.0f * leftBoundary, yPos, zPos) * speed * Time.deltaTime;
        }
        //same as above but add twice of right boundary position
        else if (transform.position.x < leftBoundary)
        {
            transform.position += new Vector3(2.0f * rightBoundary, yPos, zPos) * speed * Time.deltaTime;
        }
    }
}
