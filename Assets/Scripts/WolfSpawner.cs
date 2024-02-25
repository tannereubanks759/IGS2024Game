using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;

public class WolfSpawner : MonoBehaviour
{
    
    public float spawnRange;
    public float spawnCount;
    private float animalsSpawnedCount;
    public GameObject animal;
    public GameObject leader;

    private Vector3 point;
    private GameObject Ocean;

    public GameManager manager;
    // Start is called before the first frame update
    void Start()
    {
        
        Ocean = GameObject.Find("Ocean");
        
        
    }

    private void Update()
    {
        Spawn();
    }


    public void Spawn()
    {
        if(animalsSpawnedCount < spawnCount)
        {
            if (RandomPoint(this.transform.position, spawnRange, out point))
            {
                if (point.y > Ocean.transform.position.y)
                {
                    GameObject temp;
                    if (animalsSpawnedCount == 0)
                    {
                        temp = Instantiate(leader, transform.position, Quaternion.identity);
                    }
                    else
                    {
                        temp = Instantiate(animal, transform.position, Quaternion.identity);
                    }
                    //add temp animal to manager list for despawn (not working)
                    manager.animalList.Add(temp);
                    Vector3 positionForAnimal = point + new Vector3(0, 3, 0);
                    temp.transform.position = positionForAnimal;
                    animalsSpawnedCount++;
                    
                }
            }
        }
        
    }

    

    bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {

        Vector3 randomPoint = center + Random.insideUnitSphere * range; //random point in a sphere 
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, range, NavMesh.AllAreas)) //documentation: https://docs.unity3d.com/ScriptReference/AI.NavMesh.SamplePosition.html
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
