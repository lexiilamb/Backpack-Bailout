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
        DisplayAmountCollected();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void DisplayAmountCollected()
    {
        totalTablets = GameObject.FindWithTag("Player").GetComponent<PlaceObject>().tabletCounter;
        totalCalculators = GameObject.FindWithTag("Player").GetComponent<PlaceObject>().calculatorCounter;
        totalNotebooks = GameObject.FindWithTag("Player").GetComponent<PlaceObject>().notebookCounter;
        totalLaptops = GameObject.FindWithTag("Player").GetComponent<PlaceObject>().laptopCounter;

        totalCollected = totalTablets + totalCalculators + totalNotebooks + totalLaptops;

        numCollected.text = "Items collected: " + totalCollected + "/8";
    }
}