using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CharacterControllerScript : MonoBehaviour
{
    //public variables
    public float xSens;
    public float ySens;
   private float scopeSenseX;
    private float scopeSenseY;   
    public float moveSpeed;
    public float jumpForce;
    public bool isGrounded;
    public bool isCrouched;
    public bool isPaused;
    public gunScript gun;

    //private variables
    private float mouseX;
    private float mouseY;
    private Vector3 Velocity;
    private Vector3 moveDirection;
    private GameObject mainCam;
    private CharacterController controller;
    private float horizontal;
    private float vertical;
    private bool isSprinting;
    private float originalSpeed;
    private float sprintSpeed;
    private float airSpeed;
    private bool isScoped;
    private bool inWater;
    public Pause pauseScript;
    public DayNightControl lightControl;
    private int Lives;
    bool knockedOut;
    public GameObject caveSpawn;
    public GameObject knockoutImage;

    public GameObject endingScreen;
    public AudioSource meteorSource;
    public AudioSource audioFootstep;
    public AudioClip[] footstepClip;
    private Animator anim;

    private bool footstepflag = false;
    private bool isJumpFlag = false;

    private float footstepVolume;
    private int randFootInt;

    // texture detection
    public Terrain terra;
    public Transform playerT;
    public int playerposx;
    public int playerposz;
    public float[] textureValues;

    public float nextTime;
    public float FoostepInterval;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        knockoutImage.SetActive(false);
        knockedOut = false;
        inWater = false;
        Lives = 3;
        originalSpeed = moveSpeed;
        sprintSpeed = moveSpeed * 2;
        airSpeed = moveSpeed / 4;
        scopeSenseX = xSens / 6;
        scopeSenseY = ySens / 6;
        controller = this.GetComponent<CharacterController>();
        mainCam = Camera.main.gameObject;
        isScoped = false;
        cursorDisable();
        terra = Terrain.activeTerrain;
    }

    // Update is called once per frame
    void Update()
    {
        randFootInt = Random.Range(0, 2);
        if (!isPaused)
        {
            if(this.transform.position.y < 4.173f)
            {
                inWater = true;
            }
            else
            {
                inWater = false;
            }

            //movement
            horizontal = Input.GetAxisRaw("Horizontal");
            vertical = Input.GetAxisRaw("Vertical");
            moveDirection = transform.forward * vertical + transform.right * horizontal;
            //Debug.Log("Move Direction: " + moveDirection);
            if (Input.GetKeyDown(KeyCode.C))
            {
                //float temp = originalSpeed;
                if (isCrouched == false)
                {
                    controller.transform.localScale = new Vector3(.5f, .5f, .5f);
                    //controller.height = controller.height / 2;
                    originalSpeed = originalSpeed / 2;
                    isCrouched = true;
                }
                else
                {
                    controller.transform.localScale = new Vector3(1f, 1f, 1f);
                    //controller.height = controller.height * 2;
                    originalSpeed = originalSpeed * 2;
                    isCrouched = false;
                }
                //Debug.Log("Crouched");
            }
            isScoped = gun.isScoped;

            /*float currentX = isScoped ? scopeSenseX : xSens;
            float currentY = isScoped ? scopeSenseY : ySens;*/
            if (controller.isGrounded)
            {
                //if (Velocity.y < 0)
                //{
                //    Velocity.y = -25f;
                //}

                if (Input.GetKey(KeyCode.Space))
                {
                    Velocity.y = jumpForce;
                }

                if (!inWater && Input.GetKey(KeyCode.LeftShift))
                {
                    moveSpeed = sprintSpeed;
                    anim.SetBool("Sprinting", true);
                    footstepVolume = 0.8f;
                    FoostepInterval = .37f;
                }
                else
                {
                    if (!inWater)
                    {
                        moveSpeed = originalSpeed;
                        footstepVolume = 0.44f;
                        anim.SetBool("Sprinting", false);
                        FoostepInterval = .44f;
                    }
                    else
                    {
                        moveSpeed = 1f;
                    }
                }

            }
            else
            {
                if (Velocity.y > -9.81f)
                {
                    Velocity.y -= 9.81f * Time.deltaTime;
                }
            }

            if (gun.isScoped == false)
            {

                controller.SimpleMove((moveDirection).normalized * moveSpeed);
                controller.Move(Velocity * Time.deltaTime);
                
            }

            /*if (moveDirection.magnitude > 0f && footstepflag == false && controller.isGrounded == true)
            {
                audioFootstep.Play();
                footstepflag = true;
            }*/

            if (nextTime < Time.time && moveDirection.magnitude > 0.1f && controller.isGrounded)
            {
                audioFootstep.PlayOneShot(footstepClip[randFootInt], footstepVolume);
                nextTime = Time.time + FoostepInterval;
            }

            if (!controller.isGrounded)
            {
                isJumpFlag = true;
            }

            if (controller.isGrounded == true && isJumpFlag == true)
            {
                audioFootstep.PlayOneShot(footstepClip[randFootInt], 0.5f);
                isJumpFlag = false;
            }
            
            //mouse Look
            
        }
        

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

    public void KnockedOut()
    {
        if(knockedOut == false)
        {
            knockedOut = true;
            lightControl.Knockout();
            StartCoroutine(HurtEnum());
        }
    }
    public void Die()
    {
        knockedOut = true;
        Lives--;
        this.transform.position = caveSpawn.transform.position;
        StartCoroutine(HurtEnum());
    }

    public IEnumerator HurtEnum()
    {
        knockoutImage.SetActive(true);
        isPaused = true;
        yield return new WaitForSeconds(12.1f);
        knockoutImage.SetActive(false);
        lightControl.manager.spawnMonsters();
        knockedOut = false;
        isPaused = false;
    }

    private void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.tag == "MonsterAttack")
        {
            col.gameObject.GetComponentInParent<MonsterAI>().seesPlayer = false;
            Die();
        }
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

    public void GetTerrainTexture()
    {
        ConvertPosition(playerT.position);
        CheckTexture();
    }
    void ConvertPosition(Vector3 playerPosition)
    {
        Vector3 terrainPosition = playerPosition - terra.transform.position;
        Vector3 mapPosition = new Vector3(terrainPosition.x / terra.terrainData.size.x, 0, terrainPosition.z / terra.terrainData.size.z);
        float xCoord = mapPosition.x * terra.terrainData.alphamapWidth;
        float zCoord = mapPosition.z * terra.terrainData.alphamapHeight;
        playerposx = (int)xCoord;
        playerposz = (int)zCoord;
    }
    void CheckTexture()
    {
        float[,,] aMap = terra.terrainData.GetAlphamaps(playerposx, playerposz, 1, 1);
        textureValues[0] = aMap[0, 0, 0];
        textureValues[1] = aMap[0, 0, 1];
        textureValues[2] = aMap[0, 0, 2];
        textureValues[3] = aMap[0, 0, 3];
    }
    private void LateUpdate()
    {
        if (Time.timeScale == 1 && pauseScript.paused == false)
        {
            float currentX = isScoped ? scopeSenseX : xSens;
            float currentY = isScoped ? scopeSenseY : ySens;
            mouseX -= Input.GetAxisRaw("Mouse Y") * currentY;
            mouseY += Input.GetAxisRaw("Mouse X") * currentX;
            mouseX = Mathf.Clamp(mouseX, -90f, 90f);
            this.transform.rotation = Quaternion.Euler(0, mouseY, 0);
            mainCam.transform.rotation = Quaternion.Euler(mouseX, mouseY, 0);
        }
        
    }
}
