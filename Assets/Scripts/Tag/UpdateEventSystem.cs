using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UpdateEventSystem : MonoBehaviour
{
    public EventSystem myEventSystem;
    public GameObject startButton;
    // Start is called before the first frame update
    void Start()
    {
        myEventSystem.SetSelectedGameObject(startButton);
    }
}
