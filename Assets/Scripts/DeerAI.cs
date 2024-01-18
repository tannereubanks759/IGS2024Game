using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DeerAI : MonoBehaviour 
{
    public float walkSpeed;
    public float runSpeed;
    public Node followNode;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Die()
    {

    }

    void Idle()
    {

    }

    void walkToNode()
    {

    }

    void runAway()
    {

    }

    void newNode()
    {

    }
}
