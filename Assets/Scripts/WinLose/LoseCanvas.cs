using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoseCanvas : MonoBehaviour
{
    public Text numCollected;
    public int totalCollected = 0;

    public int totalTablets = 0;
    public int totalCalculators = 0;
    public int totalNotebooks = 0;
    public int totalLaptops = 0;

    // Start is called before the first frame update
    void Start()
    {
        totalTablets = GameObject.FindWithTag("Player").GetComponent<PlaceObject>().numOfTablets;
        totalCalculators = GameObject.FindWithTag("Player").GetComponent<PlaceObject>().numOfCalculators;
        totalNotebooks = GameObject.FindWithTag("Player").GetComponent<PlaceObject>().numOfNotebooks;
        totalLaptops = GameObject.FindWithTag("Player").GetComponent<PlaceObject>().numOfLaptops;

        totalCollected = totalTablets + totalCalculators + totalNotebooks + totalLaptops;

        numCollected.text = "You collected: " + totalCollected + " out of 12 items";
    }

    // Update is called once per frame
    void Update()
    {

    }
}
