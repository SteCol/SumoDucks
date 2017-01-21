using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class manager_Game : MonoBehaviour {

    public bool game_in_progress = false;
    public int player_lost = 0;

    public GameObject prefab_duck;
    public GameObject[] m_spown_points = new GameObject[2];
    public manager_UI m_manager_ui;


    public List<GameObject> ducks = new List<GameObject>();
    private bool m_first_boot = true;



	// Use this for initialization
	void Start () {

        reset_round();

	}



    //reset round
    public void reset_round()
    {

        if(player_lost == -1)
        {
            return;
        }
        else if( player_lost > 0 )
        {
          
            m_manager_ui.show_text_playerWin(true,player_lost);
            player_lost = -1;
            game_in_progress = false;
            StartCoroutine( delayed_restart() );

        } 
        else if ( game_in_progress || m_first_boot )
        {

            //show welcome ui
            m_manager_ui.snow_text_start(true);

            game_in_progress = false;
            m_first_boot = false;

        }
        else if (game_in_progress == false)
        {
            round_begin();
            game_in_progress = true;
        }
    }



    // Update is called once per frame
    void Update () {

        if (Input.GetKeyDown("enter"))
        {
            reset_round();
        }
    }



    public void round_begin()
    {

        //clear old ducks
        foreach (GameObject duck in ducks)
        {
            duck.GetComponent<ParentDuck>().DestroyDuck();
        }
        ducks.Clear();

        //hide welcome ui
        m_manager_ui.snow_text_start(false);

        //spown ducks
        for (int i = 0; i < m_spown_points.Length; i++) {

            Vector3 spown_position = m_spown_points[i].transform.position;
            GameObject new_duck = Instantiate(prefab_duck);
            new_duck.transform.position = spown_position;

            //set duck properties
            GameObject duck_child = new_duck.GetComponent<ParentDuck>().children[1];
            duck_child.GetComponent<Movement>().playerNum = i + 1;
            ducks.Add(new_duck);
            

        }

        game_in_progress = true;

    }

    public void exit_ring(int playerNumber)
    {
        if(game_in_progress)
        {
            Debug.Log("Player " + playerNumber + " has exited the area");
            player_lost = playerNumber;
            reset_round();
        }
        
    }


    public void add_duck(GameObject duck)
    {
        ducks.Add(duck);

    }

    IEnumerator delayed_restart ()
    {
        yield return new WaitForSeconds(2);
        m_manager_ui.show_text_playerWin(false, 0);
        player_lost = 0;
    }

}
