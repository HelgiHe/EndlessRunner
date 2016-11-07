using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Mute : MonoBehaviour {

	public bool mute;

	//UI
	public Sprite soundOn;
	public Sprite soundOff;
	Image statusImage;
	public Button btn;

	//sound
	AudioSource mainMenuSong;

	// Use this for initialization
	void Awake () {

		mainMenuSong = GetComponent<AudioSource> ();
		statusImage = btn.GetComponent<Image> ();
	}

	void Start () {

		if (ES2.Exists ("mute")) {
			mute = ES2.Load<bool> ("mute");
		}

	}
	
	// Update is called once per frame
	void Update () {

		switch (mute) {
		case true:
			mainMenuSong.volume = 0;
			statusImage.sprite = soundOff;
			break;
		case false:
			mainMenuSong.volume = 1;
			statusImage.sprite = soundOn;
			break;
		}
	}

	public void toggle (){

		mute = !mute;
		ES2.Save (mute, "mute");
	}
}
