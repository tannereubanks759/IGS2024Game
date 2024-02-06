using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{
    public float reachDistance;
    public Animator anim;
    public GameObject lookObj;
    public bool isLooking;
    private bool holdingAnimal = false;
    public DayNightControl lightSystem;
    public bool isHolding;
    public GameObject totem;
    public GameObject rifle;
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
            //else if(lookObj.gameObject.tag == "Animal" && lookObj.gameObject.GetComponent<animalDamageHandler>().isDead == true)
            //{
            //    PickupAnimal();

            //}
            //else if(holdingAnimal == true)
            //{
            //    DropAnimal();
            //}
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
        holdingAnimal = true;
        lookObj.transform.SetParent(Camera.main.gameObject.transform, true);
    }


    public void DropAnimal()
    {
        lookObj.gameObject.transform.parent = null;
        //last execution
        holdingAnimal = false;
    }


    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Interact" || other.tag == "Animal")
        {
            isLooking = true;
            lookObj = other.gameObject;
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
