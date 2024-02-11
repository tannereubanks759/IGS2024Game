﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LB3DSlowRotate : MonoBehaviour
{
    public float speed=20;
    public bool go = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            go = !go;
        }
        if (go)
        {
            transform.Rotate(Vector3.up * Time.deltaTime * speed);
        }
    }
}
