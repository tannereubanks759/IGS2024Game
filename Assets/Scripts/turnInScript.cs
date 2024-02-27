using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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
    //private int dayCounter = 0;
    Interact interactScipt;
    private Camera cam;
    bool readyForNextDay = false;
    int quotaCount=0;
    /*public GameObject lookObj;*/
    [SerializeField]
    public GameObject quotaOne;
    public GameObject quotaTwo;
    public GameObject quotaThree;
    public GameObject quotaFour;
    public GameObject quotaFive;
    public GameObject quotaSix;
    public GameObject quotaSeven;
    public GameObject objWithAlterScript;
    void Start()
    {
        cam = Camera.main;
        boxCol = GetComponent<BoxCollider>();
        interactImage.SetActive(false);
        noAnimalText.SetActive(false);
        interactScipt = cam.GetComponent<Interact>();
        objWithAlterScript.GetComponent<Alter>();
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
                    // isLooking = false;
                    //lookObj = null;
                    //isHolding = false;
                    quotaAnimalsList.Remove(nameOfAnimal);
                    animalBeingCarried.SetActive(false);
                    interactScipt.isLooking = false;
                    interactScipt.lookObj = null;
                    interactScipt.isHolding = false;
                    interactScipt.holdingAnimal = false;
                    interactScipt.holderAnimal = null;
                    //oldArms.SetActive(false);
                    rifle.SetActive(true);
                    Debug.Log("Animal removed from quota");
                    if(quotaAnimalsList.Count == 0)
                    {
                        Debug.Log("QUOTA FINISHED");
                        readyForNextDay=true;
                        //dayCounter++; set this in sleep script
                        
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
            Debug.Log("Animal carried into range");
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
        if(readyForNextDay)
        {
            quotaCount++;
            updateQuota(quotaCount);
            objWithAlterScript.GetComponent<Alter>().spawnTotem();
            //Debug.Log("New quota number is:" + quotaCount);
        }
        
    }
    private void updateQuota(int aQuotaCount)
    {
        if(aQuotaCount == 1)
        {
            quotaAnimalsList.Clear();
            //Debug.Log("!!!QUOTA FOR DAY 2");
            quotaAnimalsList.Add("Quota totem 2");
            quotaOne.SetActive(false );
            quotaTwo.SetActive(true );
            readyForNextDay = false;
            //SET totem 2 QUOTA
        }
        else if(aQuotaCount == 2) 
        {
            quotaAnimalsList.Clear();
            quotaAnimalsList.Add("Quota totem 3");
            quotaTwo.SetActive(false);
            quotaThree.SetActive(true);
            readyForNextDay = false;
            //SET totem 3 QUOTA
        }
        else if (aQuotaCount == 3)
        {
            quotaAnimalsList.Clear();
            quotaAnimalsList.Add("Quota totem 4");
            quotaThree.SetActive(false);
            quotaFour.SetActive(true);
            readyForNextDay = false;
            // set totem 4 quota
        }
        else if (aQuotaCount == 4)
        {
            quotaAnimalsList.Clear();
            quotaAnimalsList.Add("Quota totem 5");
            quotaFour.SetActive(false);
            quotaFive.SetActive(true);
            readyForNextDay = false;
            // set totem 5 quota
        }
        else if (aQuotaCount == 5)
        {
            quotaAnimalsList.Clear();
            quotaAnimalsList.Add("Quota totem 6");
            quotaFive.SetActive(false);
            quotaSix.SetActive(true);
            readyForNextDay = false;
            // set totem 6 quota
        }
        else if (aQuotaCount == 6)
        {
            quotaAnimalsList.Clear();
            quotaAnimalsList.Add("Quota totem 7");
            quotaSix.SetActive(false);
            quotaSeven.SetActive(true);
            readyForNextDay = false;
            // set totem 7 quota
        }
    }
}

