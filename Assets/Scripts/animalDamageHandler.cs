using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class animalDamageHandler : MonoBehaviour
{
    public bool isDead;
    public int health = 100;
    public int damage = 50;
    RandomMovement animal;
    private void Start()
    {
        animal = this.GetComponent<RandomMovement>();
    }
    void hitByRay()
    {
        Debug.Log("Was hit my ray");
        health = health - damage;
        if (health <=0)
        {
            Debug.Log("Dead");
            animal.anim.SetBool("die", true);
            Destroy(animal);
            Destroy(this.GetComponent<NavMeshAgent>());
            //flag for pickup script

            ///
            // this code sucks but works
            ///

            isDead = true;
            //this.gameObject.AddComponent<Rigidbody>();
            //this.gameObject.GetComponent<Rigidbody>().useGravity = true;
            //Destroy(this.gameObject);
        }
        else
        {
            animal.heardShot();
        }
    }

    void headShot()
    {
        isDead = true;
    }
}

