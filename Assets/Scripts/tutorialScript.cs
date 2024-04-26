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
    public CharacterControllerScript characterControllerScript;
    public Camera currentCamera;
    public Camera camTwo;
    public Alter alterObj;
    private void Start()
    {
        characterControllerScript.introComplete = true;
        currentCamera = Camera.main;   
    }
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
            // move
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
            {
                popUpIndex++;
            }
        }
        else if (popUpIndex == 1)
        {
            //shoot/aim
           if(Input.GetMouseButtonDown(0)|| Input.GetMouseButtonDown(1)) 
            {
                popUpIndex++;
               // camTwo.enabled = true;
                //currentCamera.enabled = false;
                
            }
        }
        else if (popUpIndex == 2)
        {
            //wall1
           if(Input.GetKeyDown(KeyCode.E))
            {
                // move camera
                
                popUpIndex++;
            }
        }
        else if (popUpIndex == 3)
        {
            //wall2
            if (Input.GetKeyDown(KeyCode.E))
            {
                //currentCamera.enabled = true;
                //camTwo.enabled = false;
                
                popUpIndex++;
            }
        }
        else if (popUpIndex == 4)
        {
            //pick up animal
            if(interactObj.isHolding)
            {
                popUpIndex++;
            }
        }
        else if (popUpIndex == 5)
        {
            //walk to offering pit
            if(turnInScript.inRange)
            {
                popUpIndex++;
            }
        }
        else if (popUpIndex == 6)
        {
            // turnin
            if (Input.GetKeyDown(KeyCode.E))
            {
                popUpIndex++;
            }
        }
        else if (popUpIndex == 7)
        {
            //sleep
            if(Input.GetKeyDown(KeyCode.E))
            {
                popUpIndex++;
            }
        }
        else if (popUpIndex == 8)
        {
            //grab totem
            if (alterObj.tutRef == true)
            {
                popUpIndex++;

            }


        }
        else if(popUpIndex == 9)
        {
           //end
                //popUpIndex++;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            Debug.Log("Cursor set to active");
           
        }
    }
}
