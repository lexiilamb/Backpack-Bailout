using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
    public GameObject Ball;
    BallController ballController;

    public float translationFactor = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        ballController = Ball.GetComponent<BallController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(ballController.TowardsPlayer == 1)
        {
            if (Ball.transform.position.x > transform.position.x)
            {
                transform.position = new Vector3(transform.position.x + translationFactor, transform.position.y, transform.position.z);
            }

            if (Ball.transform.position.x < transform.position.x)
            {
                transform.position = new Vector3(transform.position.x - translationFactor, transform.position.y, transform.position.z);
            }
        }
        else
        {
            if (0 > transform.position.x)
            {
                transform.position = new Vector3(transform.position.x + translationFactor, transform.position.y, transform.position.z);
            }

            if (0 < transform.position.x)
            {
                transform.position = new Vector3(transform.position.x - translationFactor, transform.position.y, transform.position.z);
            }
        }

    }

}
