using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class RandomMovement : MonoBehaviour
{
    public NavMeshAgent agent;
    public float range; //radius of sphere

    public Transform centrePoint; //centre of the area the agent wants to move around in

    private float idleNextTime;
    private bool hasTime;
    public float idleTimeTop;
    public float idleTimeBot;
    //instead of centrePoint you can set it as the transform of the agent if you don't care about a specific area

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        idleNextTime = 0;
        hasTime = false;
    }


    void Update()
    {
        if (agent.remainingDistance <= agent.stoppingDistance) //done with path
        {
            if(hasTime == false) //Get Idle Time
            {
                idleNextTime = Time.time + Random.Range(idleTimeBot, idleTimeTop);
                hasTime = true;
            }
            Vector3 point;
            if (hasTime && Time.time > idleNextTime && RandomPoint(centrePoint.position, range, out point))
            {
                Debug.DrawRay(point, Vector3.up, Color.blue, 1.0f); //so you can see with gizmos
                agent.SetDestination(point);
                hasTime = false;
            }
        }

    }
    
    bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {

        Vector3 randomPoint = center + Random.insideUnitSphere * range; //random point in a sphere 
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas)) //documentation: https://docs.unity3d.com/ScriptReference/AI.NavMesh.SamplePosition.html
        {
            //the 1.0f is the max distance from the random point to a point on the navmesh, might want to increase if range is big
            //or add a for loop like in the documentation
            result = hit.position;
            return true;
        }

        result = Vector3.zero;
        return false;
    }
}
