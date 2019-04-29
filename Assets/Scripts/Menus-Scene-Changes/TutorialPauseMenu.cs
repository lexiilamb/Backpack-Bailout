using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TutorialPauseMenu : MonoBehaviour
{
    public EventSystem myEventSystem;
    public GameObject backToMenuButton;
    public GameObject skipButton;

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

    void OnDisable()
    {
        updateFirstSelectedToSkip();
    }

    void OnEnable()
    {
        updateFirstSelected();
    }

    public void updateFirstSelectedToSkip()
    {
        myEventSystem.SetSelectedGameObject(skipButton);
    }

    // Update is called once per frame
    public void updateFirstSelected()
    {
        myEventSystem.SetSelectedGameObject(backToMenuButton);
    }
}
