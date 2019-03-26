using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLives : MonoBehaviour
{
    Transform target;

    public int currentLife = 0;
    public int waitTime = 1;
    public bool resetPlayer = false;
    public bool endGame = false;
    public GameObject canvasHearts;
    public Canvas loseCanvas;
    private IEnumerator coroutine;
    private IEnumerator EndCroutine;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindWithTag("Player").transform;
        loseCanvas.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(resetPlayer)
        {
            coroutine = RemoveLife();
            StartCoroutine(coroutine);
            resetPlayer = false;
        }

        if (endGame)
        {
            EndCroutine = EndGame();
            StartCoroutine(EndCroutine);
            endGame = false;
        }

        // Game over
        if (currentLife == 3)
        {
            endGame = true;
        }
    }

    IEnumerator RemoveLife()
    {
        // Remove life
        canvasHearts.transform.GetChild(currentLife).gameObject.SetActive(false);
        currentLife++;

        // Reset player position
        yield return new WaitForSeconds(waitTime);
        var pos = transform.position;
        pos.x = 18.84f;
        pos.y = 0.942f;
        pos.z = 1.03f;
        target.transform.position = pos;
        GameObject.FindWithTag("Chad").GetComponent<TagAI>().checkReset = true;
    }

    IEnumerator EndGame()
    {
        // Reset player position
        yield return new WaitForSeconds(waitTime);
        loseCanvas.gameObject.SetActive(true);
        // Freeze game and display lose canvas
        Time.timeScale = 0f;
    }

    public void ResetPlayer()
    {
        coroutine = RemoveLife();
        StartCoroutine(coroutine);
    }
}
