using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
public class animalDamageHandler : MonoBehaviour
{
    public bool isDead;
    public int health = 100;
    public int damage = 50;
    RandomMovement animal;
    public GameObject head;
    public GameObject body;
    private void Start()
    {
        animal = this.GetComponent<RandomMovement>();
    }
    void hitByRay()
    {
        if(!(isDead)){
            Debug.Log("Was hit my ray");
            health = health - damage;
            if (health <= 0)
            {
                die();
            }
            else
            {
                animal.heardShot();
            }
        }
    }

    void headShot()
    {
        if (!(isDead))
        {
            die();
        }
    }
    void die()
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
        head.gameObject.AddComponent<Rigidbody>();
        body.gameObject.AddComponent<Rigidbody>();
        Rigidbody rb = head.gameObject.GetComponent<Rigidbody>();
        Rigidbody rb2 = body.gameObject.GetComponent<Rigidbody>();
        rb.useGravity = true;
        rb.isKinematic = true;
        rb2.useGravity = true;
        rb2.isKinematic = true;
        //animal.GetComponentInChildren<AlignWithGround>().enabled = false;
        this.gameObject.transform.rotation = this.GetComponentInChildren<Animator>().transform.rotation;
        this.GetComponentInChildren<Animator>().transform.localRotation = Quaternion.identity;
    }

    
}



