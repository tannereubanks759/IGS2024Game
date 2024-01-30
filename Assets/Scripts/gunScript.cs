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
   [SerializeField] animalDamageHandler ani;
    public int DAMAGE = 50;
    public GameObject animal;
    public int currentHP;
    Animator animator;
    private bool isScoped = false;
    public GameObject scopeUI;
    void Start()
    {
        reloadText.SetActive(false);
        currentHP = 100;
        animator = GetComponent<Animator>();
        ani = animal.GetComponent<animalDamageHandler>();
        scopeUI.SetActive(false);
        //Debug.Log(currentHP);
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
           // bool isScoped = false;
            if (isScoped == false)
            {

                animator.SetTrigger("ads");
                
                isScoped = true;
                if(isScoped)
                {
                    //onScoped();
                }
                else
                {
                    //notScoped();
                }
            }
            else
            {
                Debug.Log("Unscoped called");
                animator.ResetTrigger("ads");
                animator.SetTrigger("unAds");
                isScoped = false;
                
            }
            
            
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
    void onScoped()
    {
        scopeUI.SetActive(isScoped);
    }
    void notScoped()
    {
        scopeUI.SetActive(isScoped);
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
            //Debug.Log("Hit something");
            hitPointObj.transform.position = target.point;
            if (target.collider.tag == "Animal")
            {
                Debug.Log("Hit animal");
                target.transform.SendMessage("hitByRay");
                //ani.damage(DAMAGE);
                //Debug.Log(currentHP);
                /*if (currentHP <= 0)
                {
                    Debug.Log("Destroyed");
                    Destroy(target.transform.gameObject);
                }*/
            }

        }
        if (isScoped == false && isAmmo == true)
        {
            animator.SetTrigger("clickShot");
        }
        
        isAmmo = false;
        reloadText.SetActive(true);
         }
 
}
