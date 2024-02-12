using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class MonsterAlignWithGround : MonoBehaviour
{
    public GameObject Animal;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(Animal.transform.position, Vector3.down, out hit, 10f))
        {
            Quaternion slopeRotation = Quaternion.FromToRotation(Vector3.up + Vector3.right, hit.normal);
            transform.rotation = Quaternion.Slerp(transform.rotation, slopeRotation * Animal.transform.rotation, 3 * Time.deltaTime);
        }
    }
}
