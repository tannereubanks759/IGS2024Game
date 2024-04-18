using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class gunScript : MonoBehaviour
{
    // Start is called before the first frame update
    public Camera mainCam;
    public GameObject firepoint;
    //public GameObject hitPointObj;
    public bool isAmmo = true;
    public GameObject reloadText;
    [SerializeField] animalDamageHandler ani;
    public int DAMAGE = 50;
    public float range;
    public GameObject animal;
    public int currentHP;
    public Animator animator;
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
    public CharacterController playerControl;
    public Animator scopeAnimator;
    public AudioSource gunShot;
    public GameObject bloodEffect;
    public Pause pauseRef;

    void Start()
    {
        reloadText.SetActive(false);
        currentHP = 100;
        //animator = GetComponent<Animator>();
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
            if (isReloaded == true && pauseRef.paused == false)
            {
                //Debug.Log("Fire");
                fireCalled();
            }

        }
        if (Input.GetMouseButtonDown(1) && playerControl.isGrounded)
        {
            //Debug.Log("Scope");
            // bool isScoped = false;
            if (isScoped == false)
            {

                animator.SetBool("Scoping", true);

                
                if (isScoped)
                {
                    Camera.main.GetComponent<Interact>().enabled = false;
                }

            }
            else if(isScoped == true)
            {
                //Debug.Log("Unscoped called");
                animator.SetBool("Scoping", false);

                isScoped = false;
                notScoped();

            }
            


        }
        if (Input.GetKeyDown(KeyCode.R) && (isAmmo == false || isReloaded == false))
        {
            Debug.Log("Reload");
            animator.SetBool("rr", true);
            //armAnimator.SetBool("isReload", false);
        }
        /*if (isAmmo = !true)
        {
            reloadText.SetActive (true);    
        }*/
    }
    
    public void Scoped()
    {
        scopeUI.SetActive(true);
        gun.SetActive(false);
        mainCam.fieldOfView = scopedFOV;
        crosshair.SetActive(false);
        isScoped = true;
    }
    void notScoped()
    {
        mainCam.GetComponent<Interact>().enabled = true;
        scopeUI.SetActive(false);
        mainCam.fieldOfView = baseFOV;
        gun.SetActive(true);
        crosshair.SetActive(true);
    }
    
    public void Reload()
    {
        if (isReloaded == false)
        {
            isAmmo = true;
            reloadText.SetActive(false);
            animator.SetBool("rr", false);
        }
        isReloaded = true;
    }
    private void fireCalled()
    {
        isReloaded = false;
        RaycastHit target;
        gunShot.Play();
        Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);
        armAnimator.ResetTrigger("rr");
        if (Physics.Raycast(ray, out target, range, mask))
        {
            Debug.Log("Hit something");
            //hitPointObj.transform.position = target.point;

            Debug.Log(target.collider.name);
            if (target.collider.tag == "body")
            {
                Debug.Log("Hit animal");
                //target.transform.SendMessage("hitByRay");
                Instantiate(bloodEffect, target.point, Quaternion.LookRotation(target.normal));
                Debug.Log("Spawned blood");
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
                Instantiate(bloodEffect, target.point, Quaternion.LookRotation(target.normal));
            }

        }
        if (isScoped == false && isAmmo == true)
        {
            animator.SetTrigger("clickShot");
        }
        if(isScoped == true && isAmmo == true)
        {
            scopeAnimator.SetTrigger("scopedShot");
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
