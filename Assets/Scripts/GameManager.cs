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
        timeText.text = "Day " + sunControl.currentDay.ToString() + "  " + currentTime.ToString("00.0").Replace(".", ":") + "0 " + AMORPM;
    }
}
