using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {
    private Rigidbody rb;
    public float scale;
    public float lifeTime;
    public GameObject generatedFrom;
    public float force;
    public float speed;


    // Use this for initialization
    void Start () {
        //rb = this.GetComponent<Rigidbody>();
        Destroy(this.gameObject, lifeTime);
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(-Vector3.forward * speed * Time.deltaTime);

        //rb.AddRelativeForce(-Vector3.forward * speed);
        scale = scale + 10 * Time.deltaTime;
        transform.localScale = new Vector3(scale, scale, scale);
    }

    void OnTriggerStay(Collider c)
    {
        if (c.GetComponent<Rigidbody>() != null) {
            c.GetComponent<Rigidbody>().AddExplosionForce(force, this.transform.position, scale);
        }
        //Push away from center
        /*
            Vector3 dir = c.transform.position - transform.position;
            dir = -dir.normalized;
            c.GetComponent<Rigidbody>().AddForce(-dir * force);
            */
        
    }
}
