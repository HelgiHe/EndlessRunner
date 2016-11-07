using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class EndLevel : MonoBehaviour {

	public delegate void wonLevel();
	public static event wonLevel onWin;

	public bool finishedLevel;

	public GameObject player;
	public int nextScene;
	public float transitionTime;

	public GameObject endParticles;

	GameObject levelManagerObject;
	LevelManager levelManager;

	public Transform particlePos;

	// Use this for initialization
	void Awake () {

		levelManagerObject = GameObject.Find ("LevelManager");
		levelManager = levelManagerObject.GetComponent<LevelManager> ();

	}

	void OnTriggerEnter(Collider other){

		particlePos = particlePos.transform;

		if(other.CompareTag("Player")) {

			if(onWin != null){
				
				onWin ();
			}

			//levelManager.unlockedLevelTwo = true;
			StartCoroutine (loadNextLevel(transitionTime, nextScene));
			finishedLevel = true;
			player.SetActive (false);
			Instantiate (endParticles, particlePos.position, Quaternion.identity);
		}
	}

	IEnumerator loadNextLevel(float waitTime, int nextLevel) {

		yield return new WaitForSeconds (waitTime);
		SceneManager.LoadScene (nextLevel);
	}

}
	

