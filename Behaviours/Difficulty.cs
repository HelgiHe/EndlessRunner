using UnityEngine;
using System.Collections;

public class Difficulty : MonoBehaviour {

	public float timeSinceStart;

	public GameObject lvlManager;
	MoreMountains.InfiniteRunnerEngine.LevelManager manager;

	void Awake () {
		manager = GetComponent<MoreMountains.InfiniteRunnerEngine.LevelManager> ();
	}
	
	// Update is called once per frame
	void Update () {

		timeSinceStart += Time.deltaTime;
		print (timeSinceStart);
	}
}
