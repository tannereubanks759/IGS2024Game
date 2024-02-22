using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class caveIconUpdater : MonoBehaviour
{
    // Start is called before the first frame update
    public Camera mainCam;
    public Transform lookPoint;
    public Vector3 offset;
    void Start()
    {
        mainCam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = mainCam.WorldToScreenPoint(lookPoint.position+ offset);
        if (transform.position != pos) 
        { 
            transform.position = pos;
        }
    }
}
