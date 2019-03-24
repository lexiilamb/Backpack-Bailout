using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    int Invert;
    public int TowardsPlayer = 1;
    public float xVel = 25.0f;

    public int playerScore = 0;
    public int enemyScore = 0;

    // Start is called before the first frame update
    void Start()
    {
        Invert = 1;
        TowardsPlayer = 1;

        GetComponent<Rigidbody>().velocity = new Vector3(xVel, 0.0f, 5.0f);
    }

    // Update is called once per frame
    void Update()
    {
        //if enemy won
        if(transform.position.z < -12f)
        {
            enemyScore += 1;
            GameObject.FindWithTag("PongCamera").GetComponent<Score>().EnemyScore = enemyScore;
            //GameObject.Find("Main Camera").GetComponent<Score>().IncreaseScore(2);
            Start();
            transform.position = Vector3.zero;
        }
        //if player won
        if (transform.position.z > 12f)
        {
            playerScore += 1;
            GameObject.FindWithTag("PongCamera").GetComponent<Score>().PlayerScore = playerScore;
            //GameObject.Find("Main Camera").GetComponent<Score>().IncreaseScore(1);
            Start();
            transform.position = Vector3.zero;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name == "wall")
        {
            Invert = Invert * -1;
            GetComponent<Rigidbody>().velocity = new Vector3(xVel * Invert, 0.0f, 5.0f*TowardsPlayer);
        }
        else if (collision.gameObject.name == "Player")
        {
            TowardsPlayer = 1;
            GetComponent<Rigidbody>().velocity = new Vector3(xVel * Invert, 0.0f, 5.0f);
        }
        else if (collision.gameObject.name == "AI")
        {
            TowardsPlayer = -1;
            GetComponent<Rigidbody>().velocity = new Vector3(xVel * Invert, 0.0f, -5.0f);
        }
    }
}
