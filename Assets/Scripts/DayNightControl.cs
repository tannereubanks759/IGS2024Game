using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;
using UnityEngine.Rendering;
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
    public Animator skyAnim;
    public Volume skyVolume;
    public bool isSleep = false;
    public Interact interact;

    private void Start()
    {
        currentTime = 6f;
        isRotating = true;
        currentDay = 1;
        SleepText.enabled = false;

    }
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

        if(currentTime >= 19 || interact.ableToSleepAfterQuota)
        {
            SleepText.enabled = true;
            
        }
        if(currentTime >= 19 && skyVolume.weight <= 1f)
        {
            //skyAnim.SetBool("isNight", true);
            skyVolume.weight = skyVolume.weight + (.1f * Time.deltaTime);
        }
        if(currentTime < 19f && skyVolume.weight >= 0f)
        {
            //skyAnim.SetBool("isNight", false);
            skyVolume.weight = skyVolume.weight - (.1f * Time.deltaTime);
        }
        

        if (isRotating)
        {
            UpdateLight();
        }
        if (isSleep)
        {
            CharacterControllerScript player = manager.player.GetComponent<CharacterControllerScript>();
            player.isPaused = true;
            player.BlackFade(player.blackOverlay);
            if(player.blackOverlay.color.a >= 1)
            {
                player.isPaused = false;
                Sleep();
                isSleep = false;
            }
        }
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
         
        currentTime = 5.5f;
        isRotating = true;
        currentDay += 1;
        SleepText.enabled = false;
        manager.animalsDespawned = false;
        GameObject[] spawners = GameObject.FindGameObjectsWithTag("spawner");
        manager.DespawnAllAnimals();
        manager.despawnMonsters();
        manager.monsterSpawnerParent.SetActive(false);
        manager.animalsDespawned = false;
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
        objectWithScript.dayCountUpdate();
        
    }
    


}
