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
    public turnInScript objectWithScript;
    //public turnInScript turnInObj;
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

        if(currentTime >= 19 || objectWithScript.quotaAnimalsList.Count == 0)
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
        
        // update quota list on turninscript with day one quota
        /*if (currentDay == 1)
        {
            turnInObj.quotaAnimalsList.Add("Deer");
            turnInObj.quotaAnimalsList.Add("Deer");
            turnInObj.quotaAnimalsList.Add("Rabbit");
            turnInObj.quotaAnimalsList.Add("Rabbit");
        }*/
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
        if(currentTime >= 19 || objectWithScript.quotaAnimalsList.Count == 0) { 
            currentTime = 5.5f;
            isRotating = true;
            currentDay += 1;
            // quota logic for days past day one, add quota animals to list on turninscript based on current day
            /*if (currentDay == 2)
            {
                turnInObj.quotaAnimalsList.Add("Deer");
                turnInObj.quotaAnimalsList.Add("Deer");
                turnInObj.quotaAnimalsList.Add("Rabbit");
                turnInObj.quotaAnimalsList.Add("Rabbit");
                turnInObj.quotaAnimalsList.Add("Wolf");
            }
            if (currentDay == 3)
            {
                turnInObj.quotaAnimalsList.Add("Wolf");
                turnInObj.quotaAnimalsList.Add("Deer");
                turnInObj.quotaAnimalsList.Add("Wolf");
                turnInObj.quotaAnimalsList.Add("Rabbit");
            }
            if (currentDay == 4)
            {
                turnInObj.quotaAnimalsList.Add("Bear");
                turnInObj.quotaAnimalsList.Add("Deer");
                turnInObj.quotaAnimalsList.Add("Wolf");
                turnInObj.quotaAnimalsList.Add("Wolf");

            }
            if (currentDay == 5)
            {
                turnInObj.quotaAnimalsList.Add("Bear");
                turnInObj.quotaAnimalsList.Add("Bear");
                turnInObj.quotaAnimalsList.Add("Wolf");
                turnInObj.quotaAnimalsList.Add("Goat");
            }
            if (currentDay == 6)
            {
                turnInObj.quotaAnimalsList.Add("Bear");
                turnInObj.quotaAnimalsList.Add("Bear");
                turnInObj.quotaAnimalsList.Add("Bear");
                turnInObj.quotaAnimalsList.Add("Wolf");
                turnInObj.quotaAnimalsList.Add("Wolf");
                turnInObj.quotaAnimalsList.Add("Goat");
            }
            if (currentDay == 7)
            {
                turnInObj.quotaAnimalsList.Add("Bear");
                turnInObj.quotaAnimalsList.Add("Goat");
                turnInObj.quotaAnimalsList.Add("Rabbit");
                turnInObj.quotaAnimalsList.Add("Wolf");
                turnInObj.quotaAnimalsList.Add("Deer");

            }*/
            SleepText.enabled = false;
            manager.animalsDespawned = false;
            GameObject[] spawners = GameObject.FindGameObjectsWithTag("spawner");
            manager.DespawnAllAnimals();
            manager.despawnMonsters();
            manager.monsterSpawnerParent.SetActive(false);
            manager.animalsDespawned = false;
            objectWithScript.dayCountUpdate();
            //Debug.Log("CALLED DAYCOUNT FROM DAYNIGHT");
            for (int i = 0; i < spawners.Length; i++)
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
