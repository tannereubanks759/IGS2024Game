using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimEvents : MonoBehaviour
{
    public gunScript gun;
    public AudioSource meteorSource;
    public GameObject endingScreen;
    // Start is called before the first frame update
    public void GunScoped()
    {
        gun.Scoped();
    }
    public void StopMusic()
    {
        GameObject.Find("GameManager").GetComponent<GameManager>().DayMusicChange();
    }
    public void reload()
    {
        gun.Reload();
    }

    public void playMeteorSound()
    {
        meteorSource.Play();
    }

    public void endScreenEnable()
    {
        cursorEnable();
        endingScreen.SetActive(true);

    }
    public void cursorDisable()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    public void cursorEnable()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
