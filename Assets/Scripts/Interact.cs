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
    private bool holdingAnimal = false;
    public DayNightControl lightSystem;
    public bool isHolding;
    public GameObject totem;
    public GameObject rifle;
    public GameObject deadBearObj;
    private GameObject holderAnimal;
    public Alter alterScript;
    // Start is called before the first frame update
    void Start()
    {
        isLooking = false;
        rifle.SetActive(true);
        totem.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && isLooking)
        {
            if(lookObj.gameObject.name == "Bed")
            {
                lightSystem.Sleep();
            }
            else if(lookObj.gameObject.layer == 8)
            {
                PickupTotem();
            }
            else if(lookObj != null && isHolding && lookObj.gameObject.name == "alter")
            {
                PlaceTotem();
            }
            else if (lookObj.gameObject.tag == "Animal" && lookObj.gameObject.GetComponent<animalDamageHandler>().isDead == true)
            {
                PickupAnimal();

            }
            else if (holdingAnimal == true)
            {
                DropAnimal();
            }
        }

    }
    public void PickupTotem()
    {
        isHolding = true;
        totem.SetActive(true);
        Destroy(lookObj);
        rifle.SetActive(false);
        lookObj = null;
        isLooking = false;
    }

    public void PlaceTotem()
    {
        alterScript.place();
        rifle.SetActive(true);
        totem.SetActive(false);
        isLooking = false;
        isHolding = false;
        lookObj = null;
    }


    public void PickupAnimal()
    {
        //string holderName = getcomp<animal>().name;
        //holderAnimal = lookObj;
        //etc.

        Debug.Log("tried animal pickup");
        //new code
        if (lookObj.layer == 11)
        {
            //animalType = "Bear";
        }
        holdingAnimal = true;
        lookObj.gameObject.SetActive(false);
        rifle.SetActive(false);
        // activate child prefab of respective animal here
        isLooking = true;
        lookObj = null;
        // old code
        //holdingAnimal = true;
        //lookObj.transform.SetParent(Camera.main.gameObject.transform, true);
    }


    public void DropAnimal()
    {
        // if (holderName = bear)
        //      spawn dead bear
        //etc.

        //final steps
        //holderName = "";
        //

        //new code
        //either spawn in already dead prefab of respective animal,
        //or spawn in animal in death animation with animation sped
        //way up,then reset animation speed when done
        if (lookObj.layer == 11)
        {
            Instantiate(deadBearObj, this.transform);
        }
        rifle.SetActive(true);
        isLooking = false;
        holdingAnimal = false;
        lookObj.gameObject.SetActive(true);
        lookObj = null;
        //old code
        //last execution
        holdingAnimal = false;
        //animalType = "";
    }


    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Interact" || other.tag == "Animal")
        {
            isLooking = true;
            lookObj = other.gameObject;
            string output = lookObj.tag.ToString();
            Debug.Log(output);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Interact")
        {
            isLooking = false;
            lookObj = null;
        }
    }
}
