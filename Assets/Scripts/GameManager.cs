using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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

    // Start is called before the first frame update
    void Start()
    {
        SpawnTreeObstacles();
        sunControl = GameObject.Find("Sun").GetComponent<DayNightControl>();
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
        if (sunControl.currentTime > 19 && animalList.Count > 0)
        {
            DespawnAnimals();
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
            }
        }
    }
}
