using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayHalo : MonoBehaviour
{
    public GameObject halo;

    // Start is called before the first frame update
    void Start()
    {
        halo.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            // Activate halo
            halo.SetActive(true);
        }
    }

    void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            // Activate halo
            halo.SetActive(false);
        }
    }
}
