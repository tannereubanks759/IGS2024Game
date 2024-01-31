using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class animalDamageHandler : MonoBehaviour
{

    public int health = 100;
    public int damage = 50;
    void hitByRay()
    {
        Debug.Log("Was hit my ray");
        health = health - damage;
        if (health <=0)
        {
            Debug.Log("Dead");
            this.GetComponent<RandomMovement>().anim.SetBool("die", true);
            Destroy(this.GetComponent<RandomMovement>());
            Destroy(this.GetComponent<NavMeshAgent>());

            //Destroy(this.gameObject);
        }
    }
   
    
}
