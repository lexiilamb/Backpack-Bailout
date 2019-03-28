using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinCanvas : MonoBehaviour
{
    public Text setTime;
    public static float finalTime = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        finalTime = GameObject.FindWithTag("Player").GetComponent<PlaceObject>().currentTime;

        setTime.text = "Your Time: " + finalTime;
    }

    // Update is called once per frame
    void Update()
    {
        //finalTime = GameObject.FindWithTag("Player").GetComponent<PlaceObject>().currentTime;
        //setTime.text = "Your Time: " + finalTime;
    }
}
