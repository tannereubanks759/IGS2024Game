using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlignWithGround : MonoBehaviour
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
        if(Physics.Raycast(transform.position, Vector3.down, out hit, 10f))
        {
            Quaternion slopeRotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
            transform.rotation = Quaternion.Slerp(transform.rotation, slopeRotation * Animal.transform.rotation, 3 * Time.deltaTime);
            Debug.Log(hit.normal);
        }
    }
}
