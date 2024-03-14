using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class caveIconUpdater : MonoBehaviour
{
    // Start is called before the first frame update
    public Camera mainCam;
    public Transform lookPoint;
    public Vector3 offset;
    public float fovAngle = 60f;
    public GameObject image;
    void Start()
    {
        mainCam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 directionToLookPoint = lookPoint.position - mainCam.transform.position;
        directionToLookPoint.Normalize();

        
        Vector3 cameraForward = mainCam.transform.forward;

        
        float angle = Vector3.Angle(cameraForward, directionToLookPoint);

        //player fov looking at lookpoint
        if (angle <= fovAngle)
        {
            
            Vector3 pos = mainCam.WorldToScreenPoint(lookPoint.position + offset);

           
            if (image.transform.position != pos)
            {
                image.transform.position = pos;
            }

            //renable if previously was false
            if (!image.activeSelf)
            {
                image.SetActive(true);
            }
        }
        else
        {
            
            if (image.activeSelf)
            {
                image.SetActive(false);
            }
        }
    }
}
