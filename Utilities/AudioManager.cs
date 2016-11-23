using UnityEngine;
using System.Collections;

//sér um global hljóðið

public class AudioManager : MonoBehaviour {

	public AudioClip bkgSound;
	public AudioClip loseSound;
	AudioSource audio;

	// Use this for initialization
	void Start () {
		audio = gameObject.GetComponent<AudioSource> ();
		audio.clip = bkgSound;
		audio.Play ();
	}

	void OnEnable () {
		PlayerManger.OnPlayerDied += playLoseSound;
		MoreMountains.InfiniteRunnerEngine.LevelManager.OnPlayerDeath += playLoseSound;
	}

	void OnDisable () {
		PlayerManger.OnPlayerDied -= playLoseSound;
		MoreMountains.InfiniteRunnerEngine.LevelManager.OnPlayerDeath -= playLoseSound;
	}

	public void playLoseSound () {
		audio.Stop ();
		audio.PlayOneShot (loseSound);
	}
}
