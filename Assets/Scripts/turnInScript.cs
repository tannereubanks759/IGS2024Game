using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class turnInScript : MonoBehaviour
{
    // Start is called before the first frame update
    public BoxCollider boxCol;
    public GameObject interactImage;
    public bool inRange = false;
    public bool hasAnimal = false;
    public GameObject noAnimalText;
    //public string[] quotaAnimals;
    private bool isQuota = false;
    string nameOfAnimal;
    public List<string> quotaAnimalsList;
    public GameObject rifle;
    public GameObject animalBeingCarried;
    public GameObject oldArms;
    private int dayCounter = 0;
    /*public GameObject lookObj;*/
    void Start()
    {
        boxCol = GetComponent<BoxCollider>();
        interactImage.SetActive(false);
        noAnimalText.SetActive(false);
        //lookObj = null;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (inRange) 
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                //Debug.Log("Tried to interact");
                if(hasAnimal && isQuota)
                {
                    //Debug.Log("Player sacraficed animal");
                    quotaAnimalsList.Remove(nameOfAnimal);
                    animalBeingCarried.SetActive(false);
                    oldArms.SetActive(false);
                    rifle.SetActive(true);
                    Debug.Log("Animal removed from quota");
                    if(quotaAnimalsList.Count == 0)
                    {
                        Debug.Log("QUOTA FINISHED");
                        //dayCounter++; set this in sleep script
                        if(dayCounter == 1)
                        {
                            Debug.Log("Day 2 quota");

                        }
                        else if (dayCounter == 2) 
                        {
                            Debug.Log("Day 3 quota");
                        }
                        else if (dayCounter == 3)
                        {
                            Debug.Log("Day 4 quota");
                        }
                        else if (dayCounter == 4)
                        {
                            Debug.Log("Day 5 quota");
                        }
                        else if (dayCounter == 5)
                        {
                            Debug.Log("Day 6 quota");
                        }
                        else if (dayCounter == 6)
                        {
                            Debug.Log("Day 7 quota");
                        }
                    }

                }
                else
                {
                    //Debug.Log("Player tried to interact without an animal");
                    noAnimalText.SetActive(true);
                    interactImage.SetActive(false );
                    //display text that player needs animal 
                }
            }
        }
    }
    private void OnTriggerEnter(Collider collision)
    {
        
        if (collision.tag == "Player")
        {
            //Debug.Log("Player entered range");
            inRange = true;
            interactImage.SetActive(true);
        }
        if(collision.tag == "Animal")
        {
            //Debug.Log("Animal carried into range");
            hasAnimal = true;
             nameOfAnimal = collision.name;
            animalBeingCarried = collision.gameObject;
            //Debug.Log(nameOfAnimal);
            //string toFind = "FpsArms (1)";
            oldArms = collision.transform.GetChild(1).gameObject;
            if (quotaAnimalsList.Contains(nameOfAnimal))
            {
                //Debug.Log("Animal was in quota");
                isQuota = true;
            }
            else
            {
                //Debug.Log("Not in quota");
                isQuota = false;
            }
        }
        
        
    }
    private void OnTriggerExit(Collider collision)
    {
        
        if (collision.tag == "Player")
        {
            //Debug.Log("Player left range");
            inRange = false;
            interactImage.SetActive(false);
            noAnimalText.SetActive(false);
            // turn off text saying player needs animal
        }
        
    }
    public void dayCountUpdate()
    {
        
        dayCounter++;
        Debug.Log("DAY UPDATED TO" + dayCounter);
    }
}
