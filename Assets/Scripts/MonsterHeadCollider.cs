using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterHeadCollider : MonoBehaviour
{
    public MonsterAI monster;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            monster.seesPlayer = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            monster.seesPlayer = false;
        }
    }
}
