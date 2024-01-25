using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animalDamageHandler : MonoBehaviour
{

    public int health = 100;
    public int damage(int amount, int health)
    {
        
        health = health - amount;
        

        return health;  
    }
    /*public void killAnimal()
    {
        Destroy(this.gameObject);
    }*/
}
