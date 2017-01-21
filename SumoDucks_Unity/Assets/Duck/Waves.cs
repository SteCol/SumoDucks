using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waves : MonoBehaviour {
    

    public float waveStrength;
    public int playerNum;
    public float slerpValue;

    public GameObject controller;
    public float damping;

    public bool waving;

    public float angle;
    public float angleOffset;

    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        float waveValue = Input.GetAxis("Player_" + playerNum + "_Wave");

        if (waveValue < 0 && angle < angleOffset)
        {
            angle = angle + waveStrength * Time.deltaTime;
        }
        else if (waveValue > 0 && angle > -angleOffset)
        {
            angle = angle - waveStrength * Time.deltaTime;
        }
        else {
            if (angle < 0)
                angle = angle + waveStrength * Time.deltaTime;
            if (angle > 0)
                angle = angle - waveStrength * Time.deltaTime;
        }

        this.transform.localEulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, angle * waveStrength);

        if (controller.GetComponent<DucklingsGenerator>().ducklings.Count > 0)
        {
            Quaternion rotation = Quaternion.LookRotation(controller.GetComponent<DucklingsGenerator>().ducklings[0].transform.position - this.transform.position);
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, rotation, Time.deltaTime * damping);
            foreach (GameObject d in controller.GetComponent<DucklingsGenerator>().ducklings) {
                d.transform.localEulerAngles = new Vector3(d.transform.eulerAngles.x, d.transform.eulerAngles.y, angle * waveStrength);
            }
        }
    }
}
