using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GenerateObjects : MonoBehaviour
{
    public GameObject tablets; 
    public GameObject calculators;
    public GameObject notebooks;
    public GameObject laptops;

    private int typesOfObjects = 4;
    private int amountOfEachObject = 10;
    private int numToActivate;
    private int leftToActivate;

    private int difficulty = 0;

    // Start is called before the first frame update
    void Start()
    {
        difficulty = GameDifficulty.gameDifficulty;
        // Set amount of items to activate based on game difficulty
        // Easy = activate 2 of each category
        // Medium = 3 of each category 
        // Hard = 4 of each
        numToActivate = difficulty + 2;

        List<GameObject> objectArray = new List<GameObject>();
        objectArray.Add(tablets);
        objectArray.Add(calculators);
        objectArray.Add(notebooks);
        objectArray.Add(laptops);

        for (int i = 0; i < typesOfObjects; i++)
            hideAll(objectArray[i]);

        for (int i =0; i < typesOfObjects; i++)
            generateObjects(objectArray[i]);
    }

    public void generateObjects(GameObject objectGroup)
    {
        bool[] activeArray = new bool[amountOfEachObject];
        leftToActivate = numToActivate;

        while (leftToActivate > 0)
        {
            // Pick random number from 0 and amount to generate
            int randNumber = Random.Range(0, amountOfEachObject);
            if(!activeArray[randNumber])
            {
                // Save the index of item chosen
                activeArray[randNumber] = true;
                // Enable randomly chosen item
                GameObject setThisOneActive = objectGroup.transform.GetChild(randNumber).gameObject;
                setThisOneActive.SetActive(true);
                // Disable collect effect
                setThisOneActive.transform.GetChild(0).gameObject.SetActive(false);
                leftToActivate--;
            }
        }
    }

    public void hideAll(GameObject objectGroup)
    {
        for (int i = 0; i < amountOfEachObject; i++)
        {
            GameObject hideObject = objectGroup.transform.GetChild(i).gameObject;
            hideObject.SetActive(false);
        }

    }
}
