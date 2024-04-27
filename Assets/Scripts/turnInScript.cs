using System;
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
    public bool ableToSleep;
    public GameObject interactImage;
    public bool inRange = false;
    public bool hasAnimal = false;
    public GameObject noAnimalText;
    //public string[] quotaAnimals;
    public bool isQuota = false;
    string nameOfAnimal;
    public List<string> quotaAnimalsList;
    public GameObject rifle;
    public GameObject animalBeingCarried;
    public GameObject oldArms;
    //private int dayCounter = 0;
    public Interact interactScipt;
    public Camera cam;
    bool readyForNextDay = false;
    public int quotaCount;
    /*public GameObject lookObj;*/
    [SerializeField]
    public GameObject quotaOne;
    public GameObject quotaTwo;
    public GameObject quotaThree;
    public GameObject quotaFour;
    /*public GameObject quotaFive;
    public GameObject quotaSix;
    public GameObject quotaSeven;*/
    public GameObject objWithAlterScript;
    public Animator anim;
    public GameObject[] arrayOne;
    public GameObject[] arrayTwo;
    public GameObject[] arrayThree;
    public GameObject[] arrayFour;
    public GameObject[] usingArray;
    private int rabbitCount = 1;
    private int deerCount = 1;
    private int wolfCount=0;
    private int bearCount=0;
    private int goatCount=0;
    private int rabbitIndex = 0;
    private int deerIndex = 1;
    private int wolfIndex = 0;
    private int bearIndex = 0;  
    private int goatIndex = 0;
    public AudioSource turninSource;
    public AudioClip turninSound;
    public GameObject fireParticleSys;
    public ParticleSystem normalFire;
    public ParticleSystem turnInFire;

    void Start()
    {
        
        boxCol = GetComponent<BoxCollider>();
        interactImage.SetActive(false);
        noAnimalText.SetActive(false);
        
        //lookObj = null;
        usingArray = arrayOne;
        ableToSleep = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (inRange) 
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                //Debug.Log("Tried to interact");
                if(hasAnimal && isQuota)
                {
                    //Debug.Log("Player sacraficed animal");
                    // isLooking = false;
                    //lookObj = null;
                    //isHolding = false;
                    quotaAnimalsList.Remove(nameOfAnimal);
                    //animalBeingCarried.SetActive(false);
                    interactScipt.isLooking = false;
                    interactScipt.lookObj = null;
                    interactScipt.isHolding = false;
                    interactScipt.holdingAnimal = false;
                    interactScipt.holderAnimal = null;
                    isQuota = false;
                    hasAnimal = false;
                    //oldArms.SetActive(false);
                    //rifle.SetActive(true);
                    anim.SetBool("holdingAnimal", false);
                    Instantiate(turnInFire, fireParticleSys.transform);
                    if (nameOfAnimal == "Rabbit") 
                    {
                        for (int i = rabbitIndex; i > rabbitIndex - rabbitCount; i--)
                        {
                            if (usingArray[i].activeInHierarchy == true) 
                            {
                                usingArray[i].SetActive(false);
                                turninSource.PlayOneShot(turninSound);
                                //normalFire.Stop();
                                break;
                            }
                        }
                    }
                    
                    
                    else if(nameOfAnimal == "Deer")
                    {
                        for (int i = deerIndex; i > deerIndex - deerCount; i--)
                        {
                            if (usingArray[i].activeInHierarchy == true)
                            {
                                usingArray[i].SetActive(false);
                                turninSource.PlayOneShot(turninSound);
                                break;
                            }
                        }
                    }
                    else if (nameOfAnimal == "Wolf")
                    {
                        for (int i = wolfIndex; i > wolfIndex - wolfCount; i--)
                        {
                            if (usingArray[i].activeInHierarchy == true)
                            {
                                usingArray[i].SetActive(false);
                                turninSource.PlayOneShot(turninSound);
                                break;
                            }
                        }
                    }
                    else if (nameOfAnimal == "Bear")
                    {
                        for (int i = bearIndex; i > bearIndex - bearCount; i--)
                        {
                            if (usingArray[i].activeInHierarchy == true)
                            {
                                usingArray[i].SetActive(false);
                                turninSource.PlayOneShot(turninSound);
                                break;
                            }
                        }
                    }
                    else if(nameOfAnimal == "Goat")
                    {
                        for (int i = goatIndex; i > goatIndex - goatCount; i--)
                        {
                            if (usingArray[i].activeInHierarchy == true)
                            {
                                usingArray[i].SetActive(false);
                                turninSource.PlayOneShot(turninSound);
                                break;
                            }
                        }
                    }

                    //Debug.Log("ANimation set to false");
                    if (quotaAnimalsList.Count == 0)
                    {
                        Debug.Log("QUOTA FINISHED");
                        readyForNextDay=true;
                        interactScipt.ableToSleepAfterQuota = true;
                        isQuota = false;
                        hasAnimal = false;
                        //dayCounter++; set this in sleep script
                        
                    }

                }
                else
                {
                    //Debug.Log("Player tried to interact without an animal");
                    noAnimalText.SetActive(true);
                    interactImage.SetActive(false);
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
            Debug.Log(nameOfAnimal);
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
            
            
            quotaAnimalsList.Add("Deer");
            quotaAnimalsList.Add("Goat");
            quotaAnimalsList.Add("Wolf");
            quotaOne.SetActive(false );
            quotaTwo.SetActive(true );
            readyForNextDay = false;
            deerCount = 1;
            goatCount = 1;
            wolfCount = 1;
            usingArray = arrayTwo;
            deerIndex = 0;
            goatIndex = 1;  
            wolfIndex = 2; 
            //setCount and new indexForNew Array here.
            //SET totem 2 QUOTA
        }
        else if(aQuotaCount == 2) 
        {
            quotaAnimalsList.Clear();
            
            quotaAnimalsList.Add("Wolf");
            quotaAnimalsList.Add("Wolf");
            quotaAnimalsList.Add("Bear");
            quotaTwo.SetActive(false);
            quotaThree.SetActive(true);
            readyForNextDay = false;
            wolfCount = 2;
            bearCount = 1;
            usingArray = arrayThree;
            wolfIndex = 1;
            bearIndex = 2;
            //SET totem 3 QUOTA
        }
        else if (aQuotaCount == 3)
        {
            quotaAnimalsList.Clear();
            quotaAnimalsList.Add("Deer");
            quotaAnimalsList.Add("Wolf");
            quotaAnimalsList.Add("Rabbit");
            quotaAnimalsList.Add("Bear");
            quotaAnimalsList.Add("Goat");
            quotaThree.SetActive(false);
            quotaFour.SetActive(true);
            readyForNextDay = false;
            usingArray = arrayFour;
            deerCount = 1;
            wolfCount = 1;
            rabbitCount = 1;
            bearCount = 1;
            goatCount = 1;
            rabbitIndex = 0;
            deerIndex = 1;
            goatIndex = 2;
            wolfIndex = 3;
            bearIndex = 4;

            // set totem 4 quota
            //END HERE FOR NEW QUOTA
        }
        
    }
}

