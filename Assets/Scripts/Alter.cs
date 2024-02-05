using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alter : MonoBehaviour
{
    public GameObject[] alterTotems;
    public GameObject spawnPos;
    public GameObject TotemPref;
    public KeyCode spawnKeyDebug;
    // Start is called before the first frame update
    void Start()
    {
        alterTotems = GameObject.FindGameObjectsWithTag("AlterTotem");
        for(int i = 0; i < alterTotems.Length; i++)
        {
            alterTotems[i].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(spawnKeyDebug))
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
    }

    public void spawnTotem()
    {
        Instantiate(TotemPref, spawnPos.transform.position, Quaternion.identity);
    }
}
