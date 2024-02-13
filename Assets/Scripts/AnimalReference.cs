using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalReference : MonoBehaviour
{
    public animalDamageHandler handler;

    public void talkToHandler()
    {
        if(this.gameObject.tag == "head")
        {
            handler.SendMessage("headShot");
        }
        else if(this.gameObject.tag == "body")
        {
            handler.SendMessage("hitByRay");
        }
    }
}
