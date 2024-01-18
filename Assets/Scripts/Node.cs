using System.Collections;
using System.Collections.Generic;
//using System.Numerics;
using UnityEngine;

public class Node : MonoBehaviour
{

    private GameObject[] grasslandNodeList;
    private GameObject[] mountainNodeList;
    private GameObject[] beachNodeList;
    private GameObject[] forestNodeList;
    private GameObject[] SnowNodeList;

    private Node[] nodes = new Node[3];
    //public float 
    // Start is called before the first frame update
    void Start()
    {
        grasslandNodeList = GameObject.FindGameObjectsWithTag("DeerNode");
        mountainNodeList = GameObject.FindGameObjectsWithTag("WolfNode");
        beachNodeList = GameObject.FindGameObjectsWithTag("RabbitNode");
        forestNodeList = GameObject.FindGameObjectsWithTag("BearNode");
        SnowNodeList = GameObject.FindGameObjectsWithTag("GoatNode");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Node GetNode(GameObject[] RegionalNodes)
    {
        
        for (int j = 0; j < 3; j++)
        {
            Node temp = RegionalNodes[0].GetComponent<Node>();
            for (int i = 0; i < RegionalNodes.Length; i++)
            {
                float distance = Vector3.Distance(this.transform.position, temp.transform.position);
                if (distance > Vector3.Distance(this.transform.position, RegionalNodes[i].transform.position))
                {
                    for (int k = 0; k < 3; k++)
                    {
                        if (RegionalNodes[i].GetComponent<Node>() != nodes[k])
                        {
                            temp = RegionalNodes[i].GetComponent<Node>();
                        }
                    }
                }
            }
            nodes[j] = temp;
        }
        return nodes[Random.Range(0, 3)];
    }
}
