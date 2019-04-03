using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class InSafeZone : MonoBehaviour
{
    public GameObject safezoneText;

    void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            safezoneText.gameObject.SetActive(true);
        }
    }

    void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            safezoneText.gameObject.SetActive(false);
        }
    }
}