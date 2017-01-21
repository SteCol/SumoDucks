using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class manager_Game : MonoBehaviour {

    public bool game_in_progress = false;
    public GameObject[] ducks;

	// Use this for initialization
	void Start () {
        round_begin();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void round_begin()
    {
        game_in_progress = true;
    }

    public void exit_ring(int playerNumber)
    {
        game_in_progress = false;
        Debug.Log("Player " + playerNumber + " has exited the area");
    }

    public void reset_round()
    {
        game_in_progress = true;
    }

    public void add_duck(GameObject duck)
    {

    }

}
