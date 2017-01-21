using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class manager_UI : MonoBehaviour {

    public GameObject[] m_elements_ui;

	public void snow_text_start(bool show)
    {

        m_elements_ui[0].SetActive(show);
        m_elements_ui[1].SetActive(show);

    }
}
