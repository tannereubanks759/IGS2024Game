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
    
    // Start is called before the first frame update
    void Start()
    {
        sunControl = GameObject.Find("Sun").GetComponent<DayNightControl>();
        currentDay = 1;
    }

    // Update is called once per frame
    void Update()
    {
        currentTime = sunControl.currentTime;
        string AMORPM;
        if (currentTime < 12f)
        {
            AMORPM = " AM";
        }
        else if (currentTime < 13)
        {
            AMORPM = " PM";
        }
        else
        {
            currentTime = currentTime - 12;
            AMORPM = " PM";
        }
        timeText.text = "Day " + currentDay.ToString() + "  " + currentTime.ToString("00.00").Replace(".", ":") + AMORPM;
    }
}
