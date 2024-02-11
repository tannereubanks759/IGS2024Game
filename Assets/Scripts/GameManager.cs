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
}
