using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;
using TMPro;
//[ExecuteAlways]
public class DayNightControl : MonoBehaviour
{
    [Header("Time Settings")]
    [Range(0f, 24f)]
    public float currentTime;
    public float timeSpeed = 1f;
    public int currentDay;
    private bool isRotating;
    public GameManager manager;
    public TextMeshProUGUI SleepText;
    private void Update()
    {
        if (currentTime < 24f)
        {
            currentTime += Time.deltaTime * timeSpeed;
            if (currentTime < 12)
            {
                manager.AMORPM = "AM";
            }
            else
            {
                manager.AMORPM = "PM";
            }
        }
        else if(currentTime >= 24f)
        {
            currentTime = 24f;
            manager.AMORPM = "AM";
        }
       
        //end of day/night
        if (currentTime >= 24) 
        {
            isRotating = false;
        }

        if(currentTime > 19)
        {
            SleepText.enabled = true;
        }


        if (isRotating)
        {
            UpdateLight();
        }
    }

    private void Start()
    {
        currentTime = 6f;
        isRotating = true;
        currentDay = 1;
        SleepText.enabled = false;
    }

    void UpdateLight()
    {
        float sunRotation = currentTime / 24f * 360f;
        this.transform.rotation = Quaternion.Euler(sunRotation - 90f, 0, 0f);
    }

    public void Knockout()
    {
        currentTime = 19;
        manager.DespawnAnimals();
    }
    public void Sleep()
    {
        if(currentTime >= 19) { 
            currentTime = 5.5f;
            isRotating = true;
            currentDay += 1;
            SleepText.enabled = false;
            manager.animalsDespawned = false;
            GameObject[] spawners = GameObject.FindGameObjectsWithTag("spawner");
            manager.DespawnAllAnimals();
            for(int i = 0; i < spawners.Length; i++)
            {
                if (spawners[i].GetComponent<AnimalSpawner>())
                {
                    spawners[i].GetComponent<AnimalSpawner>().animalsSpawnedCount = 0;
                }
                else if(spawners[i].GetComponent<WolfSpawner>())
                {
                    spawners[i].GetComponent<WolfSpawner>().animalsSpawnedCount = 0;
                }
            }
        }
    }
}
