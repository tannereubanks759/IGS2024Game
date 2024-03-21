using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public bool paused = false;
    public Canvas PauseCanvas;
    public CharacterControllerScript player;
    //public Canvas mainCanv;
    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            pauseMenu();
        }
    }



    public void pauseMenu()
    {
        if (!paused)
        {
            //paused = true;
            player.isPaused = true;
            PauseCanvas.gameObject.SetActive(true);
            //mainCanv.gameObject.SetActive(false);
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

        }
        else
        {
            //paused =false;  
            player.isPaused = false;
            Time.timeScale = 1;
            PauseCanvas.gameObject.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            //mainCanv.gameObject.SetActive(true);
        }

        paused = !paused;
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(0);
        //fixing time bug when restarting game from main menu (time scale was still 0 on scene load)
        Time.timeScale = 1;
    }


    public void exit()
    {
        Application.Quit();
    }
}
