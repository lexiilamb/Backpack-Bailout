using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoseCanvas : MonoBehaviour
{
    // Indecies for categories 
    public int tabletsIndex = 0;
    public int calculatorsIndex = 1;
    public int notebooksIndex = 2;
    public int laptopsIndex = 3;
    private int[] indexArray;

    public Text numCollected;
    public int totalCollected = 0;

    public int[] amountOfEachCategoryCollected = new int[] { 0, 0, 0, 0 }; 

    // Start is called before the first frame update
    void Start()
    {
        DisplayAmountCollected();

        // Indecies for categores
        indexArray = new int[] {tabletsIndex, calculatorsIndex, notebooksIndex, laptopsIndex };
    }

    // Update is called once per frame
    void Update()
    {

    }

    void DisplayAmountCollected()
    {
        foreach (int item in indexArray)
        {
            amountOfEachCategoryCollected[item] = GameObject.FindWithTag("Player").GetComponent<PlaceObject>().categoryCounters[item];
        }

        foreach (int item in amountOfEachCategoryCollected)
        {
            totalCollected += item;
        }

        numCollected.text = "Items collected: " + totalCollected + "/8";
    }
}