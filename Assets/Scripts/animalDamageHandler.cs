using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            Destroy(this.gameObject);
        }
    }
   
    
}
