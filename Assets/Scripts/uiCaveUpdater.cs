using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class uiCaveUpdater : MonoBehaviour
{
    // Start is called before the first frame update
    private Camera cam;
    public Vector3 offset;
    public Transform lookAt;

    void Start()
    {
        cam = Camera.main;
            
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = cam.WorldToScreenPoint(lookAt.position+offset);
        if(transform.position != pos) 
        {
            transform.position = pos;
        }
    }
}
