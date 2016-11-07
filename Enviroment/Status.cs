using UnityEngine;
using System.Collections;

public class Status : MonoBehaviour {

	public bool isSelected;

	//for the platform logic
	public float multiplier;
	public float freezedistance;
	public float refraction;

	//material reffs
	public Material mat;
	Renderer rend;
	AnimatedNorm normTexAnim;

	//player reff
	//public Transform playerPos;


	void Start () {
		rend = gameObject.GetComponent<Renderer> ();
		//StartCoroutine (checkForPlayer (playerPos,0.1f));
		rend.material.SetFloat ("_Refraction", refraction);
		normTexAnim = GetComponent<AnimatedNorm> ();
		normTexAnim.enabled = false;
	}


	void Update () {

		//call only when the object is in dark mode
		if (!isSelected) {
			//StartCoroutine (checkForPlayer ());
			normTexAnim.enabled = true;
		}

		if (isSelected) {
			normTexAnim.enabled = false;
		}
	}

	/*
	//checks for the player and within a certain distance transforms the platform material
	IEnumerator checkForPlayer(){

		if (playerPos != null) {
			while (Vector3.Distance (transform.position, playerPos.position) < freezedistance) {

				refraction += Time.deltaTime * multiplier;
		
				rend.material.SetFloat ("_Refraction", refraction);
				yield return null;
			}
		}
		//yield return new WaitForSeconds(waitTime);

	}
	*/
}



