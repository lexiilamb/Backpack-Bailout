using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChadEndgame : MonoBehaviour
{
    public DialogueManager dialogueManger;
    public DialogueTrigger ChadEnd;

    public int waitTime;
    private bool endgame;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        endgame = GameObject.FindWithTag("AJ").GetComponent<standingAJ>().finishedCollecting;

        if (endgame)
        {
            endgameDialogue();
        }
    }

    IEnumerator endTalking()
    {
        yield return new WaitForSeconds(waitTime);
        dialogueManger.EndDialogue();
    }

    void endgameDialogue()
    {
        ChadEnd.TriggerDialogue();
        dialogueManger.DisplayNextSentence();
        StartCoroutine(endTalking());
    }
}
