using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentDuck : MonoBehaviour {
    public bool destroyDuck;

    public GameObject[] children = new GameObject[3];

    public void DestroyDuck() {
        Destroy(this.gameObject);
    }


    void Update() {
        if (destroyDuck)
        DestroyDuck();
    }

    public void ExitArena() {

    }
}
