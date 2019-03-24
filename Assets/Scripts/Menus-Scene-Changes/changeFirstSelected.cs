using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class changeFirstSelected : MonoBehaviour
{

    public EventSystem myEventSystem;
    public GameObject backToMenuButton;
    // Start is called before the first frame update
    void Start()
    {
        //Gameobject m_MyGameObject = backToMenuButton.Gameobject;
    }

    // Update is called once per frame
    public void updateFirstSelected()
    {
        myEventSystem.SetSelectedGameObject(backToMenuButton);
    }
}
