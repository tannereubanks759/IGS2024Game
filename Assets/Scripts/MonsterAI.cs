using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class MonsterAI : MonoBehaviour
{
    //private variables

    //ocean
    private GameObject ocean;

    //player
    private CharacterControllerScript player;
    private float distanceFromPlayer;

    //agent
    private NavMeshAgent agent;

    //enumerator to control the state of the enemy, uses switch statement within update
    private enum enemyStates { Wander, Attack, Sleep }
    enemyStates state;

    private Vector3 point;

    public static int MonstersInRange;
    public bool inRangeOfPlayer;

    //public variables
    public float wanderRange;

    
    // Start is called before the first frame update
    void Start()
    {
        ocean = GameObject.Find("Ocean");
        agent = this.GetComponent<NavMeshAgent>();
        state = enemyStates.Wander;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterControllerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        distanceFromPlayer = Vector3.Distance(player.transform.position, this.transform.position);
        switch (state)
        {
            case enemyStates.Wander :
                Wander();
                break;
            case enemyStates.Sleep:
                Sleep();
                break;
            case enemyStates.Attack:
                Attack();
                break;
            default:
                break;
        }

        Debug.Log(MonstersInRange);

        if(distanceFromPlayer < wanderRange)
        {
            inRangeOfPlayer = true;
        }
        else
        {
            inRangeOfPlayer = false;
        }
        
    }

    void Wander()
    {
       
        if (agent.remainingDistance <= agent.stoppingDistance + 1)
        {
            if (inRangeOfPlayer || (MonstersInRange < 1 && !inRangeOfPlayer))
            {
                if (RandomPoint(agent.transform.position, wanderRange * 2, out point))
                {
                    float pointDistance = Vector3.Distance(point, player.transform.position);
                    if (point.y > ocean.transform.position.y + 5f)
                    {
                        agent.SetDestination(point);
                    }
                }
            }
            else
            {
                if (RandomPoint(agent.transform.position, wanderRange * 2, out point))
                {
                    float pointDistance = Vector3.Distance(point, player.transform.position);
                    if (point.y > ocean.transform.position.y + 5f && pointDistance > wanderRange)
                    {
                        agent.SetDestination(point);
                    }
                }
            } 
        }
        else
        {
            //Debug.Log("hasPath");
        }
    }

    void Sleep()
    {

    }

    void Attack()
    {

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

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            MonstersInRange++;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player") 
        {
            MonstersInRange--;
        }
    }
}
