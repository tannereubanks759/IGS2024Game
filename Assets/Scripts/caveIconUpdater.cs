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
    public GameObject nightImage;
    public DayNightControl sunScript;
    private GameObject usingImage;
    void Start()
    {
        mainCam = Camera.main;
        usingImage = image;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 directionToLookPoint = lookPoint.position - mainCam.transform.position;
        directionToLookPoint.Normalize();

        
        Vector3 cameraForward = mainCam.transform.forward;

        
        float angle = Vector3.Angle(cameraForward, directionToLookPoint);


        if(sunScript.currentTime >= 19)
        {
            nightImage.SetActive(true);
            image.SetActive(false);
            usingImage = nightImage;
        }
        else
        {
            nightImage.SetActive(false);
            image.SetActive(true);
            usingImage = image;
        }
        //player fov looking at lookpoint
        if (angle <= fovAngle)
        {
            
            Vector3 pos = mainCam.WorldToScreenPoint(lookPoint.position + offset);

           
            if (usingImage.transform.position != pos)
            {
                usingImage.transform.position = pos;
            }

            //renable if previously was false
            if (!usingImage.activeSelf)
            {
                usingImage.SetActive(true);
            }
        }
        else
        {
            
            if (usingImage.activeSelf)
            {
                usingImage.SetActive(false);
            }
        }
    }
}
