using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLives : MonoBehaviour
{
    Transform target;
    public AudioSource _audiosource;

    public int currentLife = 0;
    public int waitTime = 3;
    public bool resetPlayer = false;
    public bool endGame = false;
    public bool checkEndGame = true;
    public bool canRemoveHeart = true;
    public GameObject canvasHearts;
    public GameObject caughtMessage;
    public Canvas loseCanvas;
    private IEnumerator coroutine;
    private IEnumerator EndCroutine;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindWithTag("Player").transform;
        loseCanvas.gameObject.SetActive(false);
        caughtMessage.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(resetPlayer)
        {
            resetPlayer = false;
            coroutine = RemoveLife();
            StartCoroutine(coroutine);
        }

        if (endGame)
        {
            endGame = false;
            EndCroutine = EndGame();
            StartCoroutine(EndCroutine);
        }

        // Check if game over
        if (checkEndGame)
        {
            if (currentLife == 3)
            {
                checkEndGame = false;
                endGame = true;
            }
        }
    }

    IEnumerator RemoveLife()
    {
        // Remove life
        caughtMessage.gameObject.SetActive(true);
        canvasHearts.transform.GetChild(currentLife).gameObject.SetActive(false);
        currentLife++;

        // Reset player position
        yield return new WaitForSeconds(waitTime);
        caughtMessage.gameObject.SetActive(false);
        var pos = transform.position;
        pos.x = 18.84f;
        pos.y = 0.942f;
        pos.z = 1.03f;
        target.transform.position = pos;

        // Let player move again
        GameObject.FindWithTag("Player").GetComponent<CharacterControl>().changeAltToggle = true;
        GameObject.FindWithTag("Chad").GetComponent<TagAI>().chadCaughtPlayer = false;
        canRemoveHeart = true;
    }

    IEnumerator EndGame()
    {
        yield return new WaitForSeconds(waitTime);
        _audiosource.Play();
        loseCanvas.gameObject.SetActive(true);
    }

    public void ResetPlayer()
    {
        coroutine = RemoveLife();
        StartCoroutine(coroutine);
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            // If Chad caught player and a heart hasn't been removed for this instance
            if(canRemoveHeart && GameObject.FindWithTag("Chad").GetComponent<TagAI>().chadCaughtPlayer)
            {
                // Stop player movement
                // Remove a heart and reset player
                canRemoveHeart = false;
                GameObject.FindWithTag("Player").GetComponent<CharacterControl>().changeAltToggle = true;
                ResetPlayer();
            }
        }
    }

    void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            // If Chad caught player and a heart hasn't been removed for this instance
            if (canRemoveHeart && GameObject.FindWithTag("Chad").GetComponent<TagAI>().chadCaughtPlayer)
            {
                // Stop player movement
                // Remove a heart and reset player  
                canRemoveHeart = false;
                GameObject.FindWithTag("Player").GetComponent<CharacterControl>().changeAltToggle = true;
                ResetPlayer();
            }
        }
    }
}
