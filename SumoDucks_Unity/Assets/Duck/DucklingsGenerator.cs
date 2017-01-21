using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DucklingsGenerator : MonoBehaviour {
    public GameObject ducklingContainer;

    public int desiredAmount, currentAmount;

    public GameObject ducklingPrefab;
    public List<GameObject> ducklings;


	// Use this for initialization
	void Start () {
    }
	
	// Update is called once per frame
	void Update () {
        if (desiredAmount != currentAmount) {
            ducklings.Clear();

            foreach (GameObject d in GameObject.FindGameObjectsWithTag("Duckling"))
            {
                Destroy(d);
            }

            for (int d = 0; d < desiredAmount; d++)
            {
                GameObject duckling = Instantiate(ducklingPrefab, this.transform.position, Quaternion.identity);
                ducklings.Add(duckling);
                duckling.transform.parent = ducklingContainer.transform;
            }

            for (int d = 0; d < ducklings.Count; d++)
            {
                if (d - 1 >= 0)
                    ducklings[d].GetComponent<FollowScript>().toFollow = ducklings[d - 1];
                else
                    ducklings[d].GetComponent<FollowScript>().toFollow = this.gameObject;
            }

            currentAmount = desiredAmount;
        }
	}
}
