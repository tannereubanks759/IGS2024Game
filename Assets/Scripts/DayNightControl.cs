using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;

//[ExecuteAlways]
public class DayNightControl : MonoBehaviour
{
    [Header("Time Settings")]
    [Range(0f, 24f)]
    public float currentTime;
    public float timeSpeed = 1f;

    private bool isRotating;
    private void Update()
    {
        if (currentTime < 24f)
        {
            currentTime += Time.deltaTime * timeSpeed;
        }
        else if(currentTime > 24f)
        {
            currentTime = 24f;
        }
       
        //end of day/night
        if (currentTime >= 24) 
        {
            isRotating = false;
        }


        if (isRotating)
        {
            UpdateLight();
        }
    }

    private void Awake()
    {
        currentTime = 6f;
        isRotating = true;
    }

    void UpdateLight()
    {
        float sunRotation = currentTime / 24f * 360f;
        this.transform.rotation = Quaternion.Euler(sunRotation - 90f, 0, 0f);
    }
}
