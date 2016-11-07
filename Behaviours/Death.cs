using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Death : MonoBehaviour {

//	public GameObject explosionPrefab;
//	public GameObject player;
//	public Transform spawnPos;
//	public GameObject cam;

	public delegate void Died ();
	public static event Died playerDied;


	Status status;
	//FollowPlayer followPlayer;

	void Start () {

		status = GetComponent<Status> ();
		//followPlayer = cam.GetComponent<FollowPlayer> ();
	}
		
	// Use this for initialization
	void OnTriggerEnter(Collider other) {

		if(other.gameObject.CompareTag("Player") && !status.isSelected){
			//StartCoroutine (KillPlayer (0.4f));
		}
	}
	/*
	IEnumerator KillPlayer(float waitTime)
	{
		if (playerDied != null) {
			playerDied ();
		}
		Vector3 playerPos = player.transform.position; //sækir position-ið áður en kallinn deyr
		Instantiate (explosionPrefab, playerPos, Quaternion.identity) ;
		player.SetActive (false);

		yield return new WaitForSeconds (waitTime);
		player.SetActive (true);
		player.transform.position = spawnPos.position;

	} */
}
