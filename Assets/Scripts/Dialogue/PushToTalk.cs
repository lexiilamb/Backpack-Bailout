using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushToTalk : MonoBehaviour
{
    public GameObject promptTalk;
    public bool activeText = false;

    // Start is called before the first frame update
    void Start()
    {
        promptTalk.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (activeText)
        {
            promptTalk.SetActive(true);
        }

        if (!activeText)
        {
            promptTalk.SetActive(false);
        }
    }
}
