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
    public bool ableToAttack;
    public float attackDamage;
    public float attackDistance;
    private bool isRunning;
    private GameObject Player;
    private bool hasPoint;
    private float oceanHeight;
    public float listeningDistance;
    private bool isAttacking;

    public bool packAnimal;
    public bool packLeader;
    public GameObject packleaderAnimal;

    private float idleNextTime;
    private bool hasTime;
    public float idleTimeTop;
    public float idleTimeBot;
    Vector3 point;
    float distanceFromPlayer;

    public Animator anim;

    public GameManager manager;
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
        isAttacking = false;
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    
    void Update()
    {
        distanceFromPlayer = Vector3.Distance(Player.transform.position, this.transform.position);
        if (isRunning == false && isAttacking == false)
        {
            Roam();
        }
        else if (isAttacking == true)
        {
            attack();
        }
        else if(isRunning == true)
        {
            Running();
        }
        
        

        if(ableToAttack && distanceFromPlayer < listeningDistance)
        {
            isAttacking = true;
        }else if (ableToRun && isAttacking == false && distanceFromPlayer <= listeningDistance && hasPoint == false) //debug simulate shoot at but not hit (make it run) OR player in range
        {
            hasTime = false;
            hasPoint = false;
            isRunning = true;
        }

        FindPackLeader();

    }

    void Running()
    {
        if (agent.velocity == Vector3.zero)
        {
            anim.SetBool("run", false);
        }
        else
        {
            anim.SetBool("run", true);
        }
        anim.SetBool("walk", false);
        anim.SetBool("attack", false);
        if (hasPoint == false)
        {
            if(RandomPoint(transform.position, range * runMultiplier * 2f, out point))
            {
                if(Vector3.Distance(Player.transform.position, point) > range*runMultiplier && point.y > oceanHeight + 5)
                {
                    Debug.DrawRay(point, Vector3.up, Color.blue, 5f); //so you can see with gizmos
                    agent.speed = RunSpeed;
                    agent.SetDestination(point);
                    hasPoint = true;
                }
                
            }
        }
        
        if (Vector3.Distance(agent.transform.position, point) <= 3f && hasPoint)
        {
            hasPoint = false;
            isRunning = false;
        }
        else if (hasPoint && agent.velocity == Vector3.zero)
        {
            hasPoint = false;
        }

    }

    void attack()
    {
        anim.SetBool("run", true);
        anim.SetBool("walk", false);
        agent.SetDestination(Player.transform.position);
        agent.speed = RunSpeed;
        if(distanceFromPlayer < 3)
        {
            anim.SetBool("attack", true);
            anim.SetBool("run", false);
        }
        else
        {
            anim.SetBool("run", true);
            anim.SetBool("attack", false);
        }

        if(agent.hasPath == false)
        {
            isRunning = true;
            isAttacking = false;
        }
    }

    void FindPackLeader()
    {
        if(packLeader == false && packAnimal == true && packleaderAnimal == null && this.gameObject.GetComponent<animalDamageHandler>().isDead == false) //check if it rides in packs
        {
            for(int i = 0; i < manager.animalList.Count; i++) //loop through animals in scene
            {
                RandomMovement animal = manager.animalList[i].GetComponent<RandomMovement>();
                if (animal.packLeader && Vector3.Distance(animal.transform.position, this.transform.position) < 90)
                {
                    packleaderAnimal = animal.gameObject;
                    Debug.Log("Found pack animal");
                }
            }
        }
    }

    void Roam()
    {
        if (agent.velocity == Vector3.zero)
        {
            anim.SetBool("walk", false);
        }
        else
        {
            anim.SetBool("walk", true);
        }
        anim.SetBool("run", false);
        anim.SetBool("attack", false);
        agent.speed = WalkSpeed;
        if (packAnimal && packleaderAnimal != null)
        {
            agent.SetDestination(packleaderAnimal.transform.position);
            if (agent.remainingDistance <= agent.stoppingDistance+3) //done with path
            {
                agent.isStopped = true;
            }
            else
            {
                agent.isStopped = false;
            }
        }
        else 
        {
            if (agent.remainingDistance <= agent.stoppingDistance+1) //done with path
            {
                if (hasTime == false) //Get Idle Time
                {
                    idleNextTime = Time.time + Random.Range(idleTimeBot, idleTimeTop);
                    hasTime = true;
                }
                if (hasTime && Time.time > idleNextTime && RandomPoint(centrePoint, range, out point))
                {
                    if (point.y > oceanHeight + 5)
                    {
                        Debug.DrawRay(point, Vector3.up, Color.blue, 1.0f); //so you can see with gizmos
                        agent.speed = WalkSpeed;
                        agent.SetDestination(point);
                        hasTime = false;
                    }
                }
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
    
    public void heardShot()
    {
        if(ableToRun && isRunning == false && isAttacking == false)
        {
            isRunning = true;
        }
    }
    
}

