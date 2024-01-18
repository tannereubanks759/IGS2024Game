using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class gunScript : MonoBehaviour
{
    // Start is called before the first frame update
    public Camera mainCam;
    public GameObject firepoint;
    public GameObject hitPointObj;
    public bool isAmmo = true;
    public GameObject reloadText;
    void Start()
    {
        reloadText.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if (isAmmo == true)
            {
                //Debug.Log("Fire");
                fireCalled();
            }
            
        }
        if(Input.GetMouseButtonDown (1))
        {
            Debug.Log("Scope");
        }
        if(Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("Reload");
            reload();
        }
        /*if (isAmmo = !true)
        {
            reloadText.SetActive (true);    
        }*/
    }
    private void reload()
    {
        isAmmo = true;
        reloadText.SetActive(false);
    }
    private void fireCalled()
    {
        
        RaycastHit target;

        Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out target))
        {
            Debug.Log("Hit something");
            hitPointObj.transform.position = target.point;


        }
        isAmmo = false;
        reloadText.SetActive(true);
    }
}
