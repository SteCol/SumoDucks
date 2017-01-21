using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour {

	// Use this for initialization
	void Awake () {
        AkSoundEngine.PostEvent("music_play", this.gameObject);

    }
}
