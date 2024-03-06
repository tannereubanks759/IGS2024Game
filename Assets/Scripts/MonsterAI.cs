using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class MonsterAI : MonoBehaviour
{
    //private variables
    bool leftSight;
    private bool playingAttackAnim;

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

    //Sight variables
    public static int MonstersInRange;
    public bool inRangeOfPlayer;
    public bool seesPlayer;

    //public variables
    public float wanderRange;
    public float attackSearchTime; //how much time it takes to stop attacking after the player has left the sight of the monster;
    private Animator anim;
    
    // Start is called before the first frame update
    void Start()
    {
        leftSight = false;
        seesPlayer = false;
        ocean = GameObject.Find("Ocean");
        agent = this.GetComponent<NavMeshAgent>();
        state = enemyStates.Wander;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterControllerScript>();
        anim = this.GetComponent<Animator>();
        playingAttackAnim = false;
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
        Debug.Log(state);

        if(distanceFromPlayer < wanderRange)
        {
            inRangeOfPlayer = true;
        }
        else
        {
            inRangeOfPlayer = false;
        }

        //animationControl
        if (agent.velocity != Vector3.zero)
        {
            anim.SetBool("is_walking", true);
        }
        else{
            anim.SetBool("is_walking", false);
        }
        
    }
    IEnumerator ResetAttack()
    {
        yield return new WaitForSeconds(3);
        playingAttackAnim = false;
    }
    void Wander()
    {
       
        if (agent.remainingDistance <= agent.stoppingDistance + 1)
        {
            Debug.Log("does not have point");
            if (inRangeOfPlayer || (MonstersInRange < 1 && !inRangeOfPlayer))
            {
                if (RandomPoint(agent.transform.position, wanderRange * 2, out point))
                {
                    if (point.y > ocean.transform.position.y + 2f)
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
                    if (point.y > ocean.transform.position.y + 2f && pointDistance > wanderRange)
                    {
                        agent.SetDestination(point);
                    }
                }
            } 
        }
        else
        {
            Debug.Log("hasPath");
        }
        if (seesPlayer)
        {
            state = enemyStates.Attack;
        }
    }

    void Sleep()
    {

    }

    void Attack()
    {
        if (seesPlayer)
        {
            agent.SetDestination(player.transform.position);
            if (agent.remainingDistance <= agent.stoppingDistance + 3 && playingAttackAnim == false)
            {
                playingAttackAnim = true;
                Debug.Log("Is Attacking");
                int random = Random.Range(1, 4);
                if(random == 1)
                {
                    anim.SetTrigger("do_attack_melee_1");
                }
                else if(random == 2)
                {
                    anim.SetTrigger("do_attack_melee_2");
                }
                else
                {
                    anim.SetTrigger("do_attack_ranged_1");
                }
                StartCoroutine(ResetAttack());
            }
        }
        else if (leftSight == false)
        {
            StartCoroutine(leavingSight());
            leftSight = true;
        }

    }

    public IEnumerator leavingSight()
    {
        yield return new WaitForSeconds(attackSearchTime);
        state = enemyStates.Wander;
        leftSight = false;
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
