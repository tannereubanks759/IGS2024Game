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
    // Start is called before the first frame update
    void Start()
    {
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
}
