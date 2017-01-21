using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {
    private Rigidbody rb;
    public float scale;

	// Use this for initialization
	void Start () {
        rb = this.GetComponent<Rigidbody>();
        Destroy(this.gameObject, 3f);
	}
	
	// Update is called once per frame
	void Update () {
        //rb.velocity = new Vector3(0, 0, 5);
        scale = scale + 10 * Time.deltaTime;
        transform.localScale = new Vector3(scale, scale, scale);
    }
}
