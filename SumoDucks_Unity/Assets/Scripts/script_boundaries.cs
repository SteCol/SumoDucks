﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class script_boundaries : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter()
    {
        Debug.Log("test");
    }
    void OnTriggerExit(Collider col)
    {
        Debug.Log(col.name);
    }
}
