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
    void Start()
    {
        boxCol = GetComponent<BoxCollider>();
        interactImage.SetActive(false);
        noAnimalText.SetActive(false);
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

                    Debug.Log("Animal removed from quota");
                }
                else
                {
                    Debug.Log("Player tried to interact without an animal");
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
            //Debug.Log(nameOfAnimal);
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
}
