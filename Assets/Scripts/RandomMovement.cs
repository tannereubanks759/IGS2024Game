using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class RandomMovement : MonoBehaviour
{
    public NavMeshAgent agent;
    public float range; //radius of sphere

    private Vector3 centrePoint; //centre of the area the agent wants to move around in

    public float WalkSpeed;
    public float RunSpeed;
    public bool ableToRun;
    public float runMultiplier; //the multiplier of the range that it must travel
    private bool isRunning;
    private GameObject Player;
    private bool hasPoint;
    private float oceanHeight;

    private float idleNextTime;
    private bool hasTime;
    public float idleTimeTop;
    public float idleTimeBot;
    Vector3 point;
    //instead of centrePoint you can set it as the transform of the agent if you don't care about a specific area

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        idleNextTime = 0;
        hasTime = false;
        centrePoint = this.transform.position;
        agent.speed = WalkSpeed;
        isRunning = false;
        Player = GameObject.Find("Player");
        hasPoint = false;
        oceanHeight = GameObject.Find("Ocean").transform.position.y;
    }


    void Update()
    {
        
        if (isRunning == false)
        {
            Roam();
        }
        else if(isRunning == true)
        {
            Running();
        }

        if (isRunning == false && Input.GetKeyDown(KeyCode.J) && ableToRun) //debug simulate shoot at but not hit (make it run)
        {
            hasTime = false;
            hasPoint = false;
            isRunning = true;
        }

        Debug.Log(isRunning);
    }

    void Running()
    {
        if(hasPoint == false)
        {
            if(RandomPointNormalized(transform.position, range * runMultiplier, out point))
            {
                if(Vector3.Distance(Player.transform.position, point) > range*runMultiplier && point.y > oceanHeight + 3)
                {
                    Debug.DrawRay(point, Vector3.up, Color.blue, 1.0f); //so you can see with gizmos
                    agent.speed = RunSpeed;
                    agent.SetDestination(point);
                    hasPoint = true;
                }
                
            }
        }
        
        if (hasPoint == true && agent.remainingDistance <= 0f)
        {
            isRunning = false;
        }
        
    }

    void Roam()
    {
        if (agent.remainingDistance <= agent.stoppingDistance) //done with path
        {
            if (hasTime == false) //Get Idle Time
            {
                idleNextTime = Time.time + Random.Range(idleTimeBot, idleTimeTop);
                hasTime = true;
            }
            if (hasTime && Time.time > idleNextTime && RandomPoint(centrePoint, range, out point))
            {
                Debug.DrawRay(point, Vector3.up, Color.blue, 1.0f); //so you can see with gizmos
                agent.speed = WalkSpeed;
                agent.SetDestination(point);
                hasTime = false;
            }
        }
    }

    bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {

        Vector3 randomPoint = center + Random.insideUnitSphere * range; //random point in a sphere 
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, 10f, NavMesh.AllAreas)) //documentation: https://docs.unity3d.com/ScriptReference/AI.NavMesh.SamplePosition.html
        {
            //the 1.0f is the max distance from the random point to a point on the navmesh, might want to increase if range is big
            //or add a for loop like in the documentation
            result = hit.position;
            return true;
        }

        result = Vector3.zero;
        return false;
    }
    bool RandomPointNormalized(Vector3 center, float range, out Vector3 result)
    {

        Vector3 randomPoint = center + Random.insideUnitSphere.normalized * range; //random point in a sphere 
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, Mathf.Infinity, NavMesh.AllAreas)) //documentation: https://docs.unity3d.com/ScriptReference/AI.NavMesh.SamplePosition.html
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

