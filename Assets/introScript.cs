using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class introScript : MonoBehaviour
{
    public GameObject player;
    public GameObject icons;
    public GameObject Canvas;
    public GameObject turnIn;
    public AudioClip oceanSound;
    public AudioSource source;
    // Start is called before the first frame update
    private void Start()
    {
        player.SetActive(false);
        icons.SetActive(false);
        Canvas.SetActive(false);
        turnIn.SetActive(false);
    }
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            endAnimation();
        }
    }
    public void endAnimation()
    {
        player.SetActive(true);
        icons.SetActive(true);
        Canvas.SetActive(true);
        turnIn.SetActive(true);
        player.GetComponentInParent<CharacterControllerScript>().introComplete = true;
        player.GetComponentInChildren<Interact>().introDone = true;
        this.gameObject.SetActive(false);
    }
    public void playOceanSound()
    {
        source.PlayOneShot(oceanSound, 1f);
    }
}
