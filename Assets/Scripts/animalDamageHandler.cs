using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animalDamageHandler : MonoBehaviour
{

    int health = 100;
    public int damage(int amount)
    {
        
        health = health - amount;
        

        return health;  
    }
    public void killAnimal()
    {
        Destroy(this.gameObject);
    }
}
