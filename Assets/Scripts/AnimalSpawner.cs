using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AnimalSpawner : MonoBehaviour
{
    public float spawnRange;
    public float spawnCount;
    public GameObject animal;

    private Vector3 point;
    private bool hasPoint;
    private GameObject Ocean;
    // Start is called before the first frame update
    void Start()
    {
        Ocean = GameObject.Find("Ocean");
        hasPoint = false;
        Spawn();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Spawn()
    {
        for(int i = 0; i < spawnCount; i++)
        {
            hasPoint = false;
            while(hasPoint == false)
            {
                if (RandomPoint(this.transform.position, spawnRange, out point))
                {
                    if(point.y > Ocean.transform.position.y)
                    {
                        GameObject temp = Instantiate(animal, transform.position, Quaternion.identity);
                        Vector3 positionForAnimal = point + new Vector3(0, 3, 0);
                        temp.transform.position = positionForAnimal;
                        hasPoint = true;
                    }
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
