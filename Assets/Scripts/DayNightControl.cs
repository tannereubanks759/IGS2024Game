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

    [Header("Light Settings")]
    public Light sunLight;

    private void Update()
    {
        currentTime += Time.deltaTime * timeSpeed;
        Debug.Log(currentTime);
       
        //end of day/night
        if (currentTime >= 24) 
        {
            //GetComponent<DayNightControl>().enabled = false;
        }



        UpdateLight();
    }

    private void Awake()
    {
        currentTime = 0f;
        UpdateLight();
    }

    void UpdateLight()
    {
        float sunRotation = currentTime / 24f * 360f;
        sunLight.gameObject.transform.rotation = Quaternion.Euler(sunRotation - 90f, 0, 0f);
    }
}
