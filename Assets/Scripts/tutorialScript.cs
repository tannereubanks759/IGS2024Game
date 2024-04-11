using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutorialScript : MonoBehaviour
{
    public GameObject[] popups;
    private int popUpIndex;
    public Interact interactObj;
    public turnInScript turnInScript;
    public DayNightControl dayNightControl;
    private void Update()
    {
        for (int i = 0; i < popups.Length; i++)
        {
            if (i == popUpIndex)
            {
                popups[i].SetActive(true);
            }
            else
            {
                popups[i].SetActive(false);
            }
        }
        if (popUpIndex == 0)
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
            {
                popUpIndex++;
            }
        }
        else if (popUpIndex == 1)
        {
           if(Input.GetMouseButtonDown(0)|| Input.GetMouseButtonDown(1)) 
            {
                popUpIndex++;
            }
        }
        else if (popUpIndex == 2)
        {
           if(Input.GetKeyDown(KeyCode.E))
            {
                popUpIndex++;
            }
        }
        else if (popUpIndex == 3)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                popUpIndex++;
            }
        }
        else if (popUpIndex == 4)
        {
            if(interactObj.isHolding)
            {
                popUpIndex++;
            }
        }
        else if (popUpIndex == 5)
        {
            if(turnInScript.inRange)
            {
                popUpIndex++;
            }
        }
        else if (popUpIndex == 6)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                popUpIndex++;
            }
        }
        else if (popUpIndex == 7)
        {
            if(dayNightControl.isSleep)
            {
                popUpIndex++;
            }
        }
    }
}
