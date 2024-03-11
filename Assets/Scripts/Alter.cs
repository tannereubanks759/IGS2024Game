using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alter : MonoBehaviour
{
    public GameObject[] alterTotems;
    public GameObject spawnPos;
    public GameObject TotemPref;
    public KeyCode spawnKeyDebug;
    public bool allTotemsIn;
    public GameManager manager;
    public AudioSource caveSource;
    // Start is called before the first frame update
    void Start()
    {
        allTotemsIn = false;
        alterTotems = GameObject.FindGameObjectsWithTag("AlterTotem");
        for(int i = 0; i < alterTotems.Length; i++)
        {
            alterTotems[i].SetActive(false);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            spawnTotem();
        }
    }
    public void place()
    {
        for(int i = 0; i < alterTotems.Length; i++)
        {
            if(alterTotems[i].activeSelf == false)
            {
                alterTotems[i].SetActive(true);
                break;
            }
        }
        if (alterTotems[6].activeSelf)
        {
            this.GetComponent<Animator>().SetBool("end", true);
            this.GetComponent<AudioSource>().Play();
            caveSource.Play();
            manager.win();
        }
    }

    public void spawnTotem()
    {
        Instantiate(TotemPref, spawnPos.transform.position, Quaternion.identity);
    }
}
