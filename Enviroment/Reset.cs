using UnityEngine;
using System.Collections;

public class Reset : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	/*
	IEnumerator KillPlayer(float waitTime)
	{

		Vector3 playerPos = player.transform.position; //sækir position-ið áður en kallinn deyr

		followPlayer.enabled = false;
		Destroy (player);
		Instantiate (explosionPrefab, playerPos, Quaternion.identity) ;
		yield return new WaitForSeconds (waitTime);
		//SceneManager.LoadScene(sceneNum);
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}*/
}
