using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MenuFirstSelected : MonoBehaviour
{
    public EventSystem myEventSystem;
    public GameObject backToMenuButton;

    void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2))
        {
            updateFirstSelected();
        }

        if (myEventSystem.currentSelectedGameObject == null)
        {
            updateFirstSelected();
        }
    }

    // Update is called once per frame
    public void updateFirstSelected()
    {
        myEventSystem.SetSelectedGameObject(backToMenuButton);
    }
}
