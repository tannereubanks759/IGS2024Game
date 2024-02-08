using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class headShotScript : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject parent;
    animalDamageHandler par;
    RandomMovement ranMove;
    void Start()
    {
        par = parent.GetComponent<animalDamageHandler>();
        ranMove = parent.GetComponent <RandomMovement>();   
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void headShot()
    {
        par.isDead = true;

        //Debug.Log("DIED FROM HEADSHOT");
        if (par.isDead == true) 
        {
            death();
        }
    }
    void death()
    {
        ranMove.anim.SetBool("die", true);
        Debug.Log("Die animation from headshot");
    }
}
