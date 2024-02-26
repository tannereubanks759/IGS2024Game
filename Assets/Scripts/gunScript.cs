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
    public float range;
    public GameObject animal;
    public int currentHP;
    Animator animator;
    public bool isScoped = false;
    public GameObject scopeUI;
    public GameObject gun;
    public LayerMask mask;
    public GameObject crosshair;
    private GameObject[] animals;
    public float shotSoundDistance;
    public float scopedFOV;
    public float baseFOV;
    public Animator armAnimator;
    public bool isReloaded = true;
    void Start()
    {
        reloadText.SetActive(false);
        currentHP = 100;
        animator = GetComponent<Animator>();
        ani = animal.GetComponent<animalDamageHandler>();
        scopeUI.SetActive(false);
        baseFOV = mainCam.fieldOfView;
        //Debug.Log(currentHP);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (isReloaded == true)
            {
                //Debug.Log("Fire");
                fireCalled();
            }

        }
        if (Input.GetMouseButtonDown(1))
        {
            //Debug.Log("Scope");
            // bool isScoped = false;
            if (isScoped == false)
            {

                animator.SetTrigger("ads");

                isScoped = true;
                if (isScoped)
                {
                    StartCoroutine(onScoped());

                }

            }
            else
            {
                //Debug.Log("Unscoped called");
                animator.ResetTrigger("ads");
                animator.SetTrigger("unAds");

                isScoped = false;
                notScoped();

            }


        }
        if (Input.GetKeyDown(KeyCode.R) && isAmmo == false)
        {
            Debug.Log("Reload");
            isAmmo = true;  
            StartCoroutine(reload());
            //armAnimator.SetBool("isReload", false);
        }
        /*if (isAmmo = !true)
        {
            reloadText.SetActive (true);    
        }*/
    }
    IEnumerator onScoped()
    {
        Camera.main.GetComponent<Interact>().enabled = false;
        yield return new WaitForSeconds(.25f);
        scopeUI.SetActive(true);
        gun.SetActive(false);
        mainCam.fieldOfView = scopedFOV;
        crosshair.SetActive(false);
    }
    void notScoped()
    {
        Camera.main.GetComponent<Interact>().enabled = true;
        scopeUI.SetActive(false);
        mainCam.fieldOfView = baseFOV;
        gun.SetActive(true);
        crosshair.SetActive(true);
    }
    IEnumerator reload()
    {
        // create into ienumerator and wait reload animation time
        if (isReloaded == false)
        {
        armAnimator.SetTrigger("rr");
        yield return new WaitForSeconds(1.16f);
        isAmmo = true;
        reloadText.SetActive(false);
        }
        isReloaded = true;
    }
    private void fireCalled()
    {
        isReloaded = false;
        RaycastHit target;

        Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);
        armAnimator.ResetTrigger("rr");
        if (Physics.Raycast(ray, out target, range, mask))
        {
            //Debug.Log("Hit something");
            //hitPointObj.transform.position = target.point;
            
            if (target.collider.tag == "body")
            {
                Debug.Log("Hit animal");
                //target.transform.SendMessage("hitByRay");
                target.collider.GetComponent<AnimalReference>().talkToHandler();
                //ani.damage(DAMAGE);
                //Debug.Log(currentHP);
                /*if (currentHP <= 0)
                {
                    Debug.Log("Destroyed");
                    Destroy(target.transform.gameObject);
                }*/
            }
            else if (target.collider.tag == "head")
            {
                Debug.Log("Headshot");
                target.collider.GetComponent<AnimalReference>().talkToHandler(); 
            }

        }
        if (isScoped == false && isAmmo == true)
        {
            animator.SetTrigger("clickShot");
        }

        AnimalChecker();

        isAmmo = false;
        reloadText.SetActive(true);
    }
    private void AnimalChecker() //check if animals are nearby
    {
        animals = GameObject.FindGameObjectsWithTag("Animal");
        for(int i = 0; i < animals.Length; i++)
        {
            if (animals[i].GetComponent<RandomMovement>() && Vector3.Distance(this.transform.position, animals[i].transform.position) < shotSoundDistance)
            {
                animals[i].GetComponent<RandomMovement>().heardShot();
            }
        }
    }
}
