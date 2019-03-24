using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RandomShelves : MonoBehaviour
{
    public GameObject shelves;
    private int amountOfShelves = 6;
    public int numToActivate = 3;

    //public NavMeshSurface surface;

    // Start is called before the first frame update
    void Start()
    {
        hideAll(shelves);
        generateObjects(shelves);

        // Update navmesh
        //surface.BuildNavMesh();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void generateObjects(GameObject objectGroup)
    {
        bool[] activeArray = new bool[amountOfShelves];
        int leftToActivate = numToActivate;

        while (leftToActivate > 0)
        {
            // Pick random number from 0 and 4 (5 is exclusive) 
            int randNumber = Random.Range(0, amountOfShelves);
            if (!activeArray[randNumber])
            {
                // Save the index of item chosen
                activeArray[randNumber] = true;
                // Enable randomly chosen item
                GameObject setThisOneActive = objectGroup.transform.GetChild(randNumber).gameObject;
                setThisOneActive.SetActive(true);
                leftToActivate--;
            }
        }
    }

    public void hideAll(GameObject objectGroup)
    {
        for (int i = 0; i < amountOfShelves; i++)
        {
            GameObject hideObject = objectGroup.transform.GetChild(i).gameObject;
            hideObject.SetActive(false);
        }

    }
}
