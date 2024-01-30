using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;

[ExecuteAlways]
public class DayNightControl : MonoBehaviour
{
    [Header("Time Settings")]
    [Range(0f, 24f)]
    public float currentTime;
    public float timeSpeed = 1f;

    [Header("Light Settings")]
    public Light sunLight;
    public float sunPosition = 1f;

    private void Update()
    {
        currentTime += Time.deltaTime * timeSpeed;

        //end of day/night
        if (currentTime >= 24) 
        {
            GetComponent<DayNightControl>().enabled = false;
        }

        UpdateLight();
    }

    private void OnValidate()
    {
        UpdateLight();
    }

    void UpdateLight()
    {
        float sunRotation = currentTime / 24f * 360f;
        sunLight.transform.rotation = Quaternion.Euler(sunRotation - 90f, sunPosition, 0f);
    }
}
