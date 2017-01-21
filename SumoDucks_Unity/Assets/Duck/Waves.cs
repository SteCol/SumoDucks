using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waves : MonoBehaviour {

    public float waveStrength;
    public int playerNum;
    public Rigidbody rb;

	// Use this for initialization
	void Start () {
        rb = this.GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        rb.AddRelativeTorque (Vector3.forward * Input.GetAxis("Player_" + playerNum + "_Wave") * 100);
	}
}
