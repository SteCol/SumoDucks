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
        rb = this.GetComponent<Rigidbody>();
        Destroy(this.gameObject, lifeTime);
	}
	
	// Update is called once per frame
	void Update () {
        rb.AddRelativeForce(Vector3.forward * speed);
        scale = scale + 10 * Time.deltaTime;
        transform.localScale = new Vector3(scale, scale, scale);
    }

    void OnTriggerStay(Collider c)
    {
        // force is how forcefully we will push the player away from the enemy.

        
            // Calculate Angle Between the collision point and the player
            //Vector3 dir = c.contacts[0].point - transform.position;
            Vector3 dir = c.transform.position - transform.position;

            // We then get the opposite (-Vector3) and normalize it
            dir = -dir.normalized;
            // And finally we add force in the direction of dir and multiply it by force. 
            // This will push back the player
            c.GetComponent<Rigidbody>().AddForce(-dir * force);
        
    }
}
