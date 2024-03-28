using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{
    public float reachDistance;
    //private string animalType;
    //public Animator anim;
    public GameObject lookObj;
    public bool isLooking;
    public bool holdingAnimal;
    public DayNightControl lightSystem;
    public bool isHolding;
    public GameObject totem;
    public GameObject rifle;
    public GameObject deadBearObj;
    public GameObject holderAnimal;
    public Alter alterScript;
    public GameObject[] animalParents;
    public GameObject objectWithScript;
    public Animator anim;
    public turnInScript turnIn; 
    //turnInScript turnInRef;
    // Start is called before the first frame update
    void Start()
    {
        holdingAnimal = false;
        isLooking = false;
        objectWithScript.GetComponent<turnInScript>();
        rifle.SetActive(true);
        totem.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (isLooking && !holdingAnimal)
            {
                if (lookObj != null && lookObj.gameObject.name == "Bed")
                {
                    lightSystem.Sleep();
                    //objectWithScript.GetComponent<turnInScript>().dayCountUpdate();
                }
                else if (!isHolding && lookObj != null && lookObj.gameObject.layer == 8)
                {
                    PickupTotem();
                }
                else if (lookObj != null && isHolding && lookObj.gameObject.name == "alter")
                {
                    PlaceTotem();
                }
                else if (!isHolding && lookObj != null && (lookObj.gameObject.tag == "head" || lookObj.gameObject.tag == "body") && lookObj.gameObject.GetComponent<AnimalReference>().handler.isDead == true)
                {
                    PickupAnimal();

                }
            }
            else if (holdingAnimal == true && turnIn.inRange == false)
            {
                DropAnimal();
            }
        }


    }
    public void PickupTotem()
    {
        anim.SetBool("Holding Totem", true);
        isHolding = true;
        Destroy(lookObj);
        lookObj = null;
        isLooking = false;
    }

    public void PlaceTotem()
    {
        anim.SetBool("Holding Totem", false);
        alterScript.place();
        isLooking = false;
        isHolding = false;
        lookObj = null;
    }



    public void PickupAnimal()
    {
        //string holderName = getcomp<animal>().name;
        //holderAnimal = lookObj;
        //etc.
        isLooking = true;
        holderAnimal = lookObj.GetComponent<AnimalReference>().handler.gameObject;
        string holderName = holderAnimal.GetComponent<Entity>().name;
        for(int i = 0; i < animalParents.Length; i++)
        {
            if(animalParents[i].gameObject.name == holderName)
            {
                //animalParents[i].SetActive(true);
                anim.SetTrigger(holderName);
                break;
            }
        }
        holderAnimal.transform.position = this.gameObject.transform.position - Vector3.up * 10;
        Debug.Log("tried animal pickup");
        isHolding = true;
        holdingAnimal = true;
        //rifle.SetActive(false);
        lookObj = null;
        anim.SetBool("holdingAnimal", true);
        //holderAnimal.GetComponent<BoxCollider>().enabled = false;
    }


    public void DropAnimal()
    {
        anim.SetBool("holdingAnimal", false);
        isLooking = false;
        lookObj = null;
        isHolding = false;
        holderAnimal.SetActive(true);
        string holderName = holderAnimal.GetComponent<Entity>().name;
        for (int i = 0; i < animalParents.Length; i++)
        {
            if (animalParents[i].gameObject.name == holderName)
            {
                animalParents[i].SetActive(false);
            }
        }
        RaycastHit hit;
        
        if (Physics.Raycast(this.gameObject.transform.position, transform.forward, out hit, 5f))
        {
            RaycastHit hit1;
            if(Vector3.Angle(hit.normal, transform.forward) < 45)
            {
                
                Debug.Log("raycast");
                Vector3 position = hit.point + Vector3.up * .7f;
                holderAnimal.transform.position = position;
            }
            else if (Physics.Raycast(this.gameObject.transform.position + this.transform.forward * (hit.distance / 2), Vector3.down, out hit1, 7.5f))
            {
                Debug.Log("raycast");
                Vector3 position = hit1.point + Vector3.up * .7f;
                
                holderAnimal.transform.position = position;
            }






        }
        else if(Physics.Raycast(this.gameObject.transform.position + transform.forward * 4.2f, Vector3.down, out hit, 7.5f))
        {
            Vector3 position = hit.point + Vector3.up * .7f;
            holderAnimal.transform.position = position;
        }
        
        rifle.SetActive(true);
        holdingAnimal = false;
        holderAnimal = null;
    }


    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Interact" || other.tag == "head" || other.tag == "body")
        {
            isLooking = true;
            lookObj = other.gameObject;
            string output = lookObj.tag.ToString();
            //Debug.Log(output);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Interact" || other.tag == "head" || other.tag == "body")
        {
            isLooking = false;
            lookObj = null;
        }
    }
}
