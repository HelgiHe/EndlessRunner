using UnityEngine;
using System.Collections;

public class PlayerMove : MonoBehaviour {

	Rigidbody playerRb;

	[SerializeField]
	float speed;

	// Use this for initialization
	void Awake () {
	
		playerRb = GetComponent<Rigidbody> ();

	}
	
	// Update is called once per frame
	void FixedUpdate () {
		playerRb.velocity = new Vector2(speed, playerRb.velocity.y);
	}
}
