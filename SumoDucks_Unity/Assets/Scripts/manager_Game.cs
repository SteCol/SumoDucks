﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class manager_Game : MonoBehaviour {

    public bool game_in_progress = false;
    public GameObject prefab_duck;
    public GameObject[] m_spown_points = new GameObject[2];


    private List<GameObject> ducks = new List<GameObject>();



	// Use this for initialization
	void Start () {

        reset_round();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void round_begin()
    {
        if(game_in_progress)
        {
            reset_round();
        }

        //spown ducks

        for (int i = 0; i < m_spown_points.Length; i++) {

            Vector3 spown_position = m_spown_points[i].transform.position;
            GameObject new_duck = Instantiate(prefab_duck);
            new_duck.transform.position = spown_position;

            //set duck properties
            GameObject duck_child = new_duck.GetComponent<ParentDuck>().children[1];
            duck_child.GetComponent<Movement>().playerNum = i + 1;
            

        }


        
    }

    public void exit_ring(int playerNumber)
    {
        game_in_progress = false;
        Debug.Log("Player " + playerNumber + " has exited the area");
    }

    public void reset_round()
    {
        foreach (GameObject duck in ducks)
        {
            Destroy(duck);
        }
        ducks.Clear();


    }

    public void add_duck(GameObject duck)
    {
        game_in_progress = true;
        ducks.Add(duck);
    }

}
