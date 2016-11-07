using UnityEngine;
using System.Collections;

//sér um global hljóðið

public class AudioManager : MonoBehaviour {


	public AudioClip bkgSound;
	AudioSource audio;

	// Use this for initialization
	void Awake () {
	
		audio = GetComponent<AudioSource> ();
		audio.enabled = false;
	}

	void Start () {
		StartCoroutine(PlaySound(0.2f));
	}
	
	IEnumerator PlaySound(float waitTime) {

		yield return new WaitForSeconds (waitTime);
		audio.enabled = true;
		audio.PlayOneShot (bkgSound);
	}


}
