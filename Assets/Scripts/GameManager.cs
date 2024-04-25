using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
public class GameManager : MonoBehaviour
{
    private int currentDay;
    private float currentTime;
    public TextMeshProUGUI timeText;
    private DayNightControl sunControl;
    public string AMORPM;
    private float seconds;
    public Terrain terrain;
    public GameObject treeObstacle;
    public List<GameObject> animalList = new List<GameObject>();
    public bool animalsDespawned;
    public GameObject monsterSpawnerParent;
    public List<GameObject> monsterList;
    public GameObject canvas;
    public GameObject player;
    public List<AudioClip> nightClips;
    public List<AudioClip> dayClips;
    private AudioSource source;
    public GameObject endingAnimator;
    public GameObject Icon;
    public GameObject sailboat;
    public GameObject lookPointCanvas;
    // Start is called before the first frame update
    void Start()
    {
        animalsDespawned = false;
        SpawnTreeObstacles();
        sunControl = GameObject.Find("Sun").GetComponent<DayNightControl>();
        monsterSpawnerParent.SetActive(false);
        source = this.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        currentTime = sunControl.currentTime;
        if(currentTime > 13)
        {
            currentTime -= 12;
        }
        seconds = (currentTime - (int)currentTime) * .6f;
        timeText.text = "Day " + sunControl.currentDay.ToString() + "  " + ((int)currentTime).ToString() + ":" + ((int)(seconds*100)).ToString("00") + AMORPM;
        if (sunControl.currentTime > 19 && animalsDespawned == false)
        {
            DespawnAnimals();
            spawnMonsters();
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            win();
        }
        if(sunControl.currentTime >= 19)
        {
            playRandomSong(nightClips);
        }
        else
        {
            source.Stop();
        }
        /*
        else
        {
            playRandomSong(dayClips);
        }*/
    }
    public void playRandomSong(List<AudioClip> clips)
    {
        if(source.isPlaying == false)
        {
            int random = Random.Range(0, clips.Count);
            AudioClip clip = clips.ElementAt(random);
            source.clip =(clip);
            source.Play();
        }
    }
    void SpawnTreeObstacles()
    {
        for (int i = 0; i < terrain.terrainData.treeInstances.Length; i++)
        {
            var treeInstancePos = terrain.terrainData.GetTreeInstance(i).position;
            var localPos = new Vector3(treeInstancePos.x * terrain.terrainData.size.x, treeInstancePos.y * terrain.terrainData.size.y, treeInstancePos.z * terrain.terrainData.size.z);
            var worldPos = Terrain.activeTerrain.transform.TransformPoint(localPos);
            Instantiate(treeObstacle, worldPos, treeObstacle.transform.rotation);
        }
    }

    //check is dead flag of animal fromcdamage handler script, despawn if not dead (allows players to maintain progress in terms of dead animals)
    public void DespawnAnimals()
    {
        for (int i = 0; i < animalList.Count; i++)
        {
            if (animalList[i].GetComponent<animalDamageHandler>().isDead == false)
            {
                Destroy(animalList[i]);
                animalList.Remove(animalList[i]);
                i--;
            }
        }
        animalsDespawned = true;
    }

    public void DespawnAllAnimals()
    {
        for (int i = 0; i < animalList.Count; i++)
        {
            Destroy(animalList[i]);
            animalList.Remove(animalList[i]);
            i--;
        }
        animalsDespawned = true;
    }

    public void spawnMonsters()
    {
        
        monsterSpawnerParent.SetActive(true);
        GameObject[] monsterSpawners = GameObject.FindGameObjectsWithTag("MonsterSpawner");
        for (int i = 0; i < monsterSpawners.Length; i++)
        {
            monsterSpawners[i].GetComponent<AnimalSpawner>().animalsSpawnedCount = 0;
        }
    }
    public void despawnMonsters()
    {
        //monsterSpawnerParent.SetActive(true);
        for (int i = 0; i < monsterList.Count; i++)
        {
            Destroy(monsterList[i]);
            monsterList.Remove(monsterList[i]);
            i--;
        }
    }
    public void win()
    {
        //451.88, 15.29, 577.54
        //rot 0, 84, 0
        endingAnimator.SetActive(true);
        GameObject.Find("alter").GetComponent<Animator>().SetBool("end", true);
        canvas.SetActive(false);
        player.SetActive(false);
        Icon.SetActive(false);
        sailboat.SetActive(false);
        lookPointCanvas.SetActive(false);
    }
    

}
