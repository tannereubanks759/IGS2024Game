using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class StartScreenScript : MonoBehaviour
{
    public TextMeshProUGUI text;
    public Color color;
    public int buttonType;
    public int page;
    public bool starting = false;
    void Start()
    {
        color.a = 0;
        text.color = color;
    }

    void Update()
    {
        text.color = color;
        if (color.a >= 1)
        {
            color.a = 1;
        }
        else
        {
            color.a += Time.deltaTime / 2;
        }
    }
}
