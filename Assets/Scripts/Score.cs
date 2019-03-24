using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public GUISkin scoreSkin;
    public Text pScore;
    public Text eScore;

    public int PlayerScore = 0;
    public int EnemyScore = 0;

    void Start()
    {
        pScore.gameObject.SetActive(true);
        eScore.gameObject.SetActive(true);
        //PlayerScore = GameObject.FindWithTag("PongBall").GetComponent<BallController>().playerScore;
        //EnemyScore = GameObject.FindWithTag("PongBall").GetComponent<BallController>().enemyScore;
    }

    void Update()
    {
        //PlayerScore = GameObject.FindWithTag("PongBall").GetComponent<BallController>().playerScore;
        //EnemyScore = GameObject.FindWithTag("PongBall").GetComponent<BallController>().enemyScore;

        pScore.text = "Player Score: " + PlayerScore;
        eScore.text = "Ron Score: " + EnemyScore;
    }

    /*

    public void IncreaseScore (int playerType)
    {
        if (playerType == 1)
        {
            PlayerScore++;
        }
        else if (playerType == 2)
        {
            EnemyScore++;
        }
        else
        {
            print("Increasing score of random object");
        }
    }

    void OnGUI()
    {
        if(GUI.skin != scoreSkin)
        {
            GUI.skin = scoreSkin;
        }

        GUI.Label(new Rect(645, 10, 300, 30), "Player Score " + PlayerScore.ToString());
        GUI.Label(new Rect(20, 10, 300, 30), "Enemy Score " + EnemyScore.ToString());
    }
    */

}
