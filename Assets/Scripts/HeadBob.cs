using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadBob : MonoBehaviour
{
    [SerializeField]
    private bool enableToggle = true;
    [SerializeField, Range(0f, 0.1f)]
    private float refAmplitude;
    [SerializeField, Range(0f, 0.1f)]
    private float refFrequency;
    [SerializeField]
    private Transform refCamera = null;
    [SerializeField]
    private Transform cameraHold = null;
    
    private float toggleSpeed = 3.0f;
    private Vector3 startpos;
    private CharacterController charControl;

    private void Awake()
    {
        charControl = GetComponent<CharacterController>();
        startpos = refCamera.localPosition;
    }

    private void PlayMotion(Vector3 motion)
    {

    }

    
}
