using UnityEngine;
using System.Collections;

public class SimpleMovement : MonoBehaviour {

	public float speed = 5f;
	public Buttons[] input;

	private Rigidbody body;
	private InputState inputState;


	// Use this for initialization
	void Start () {
		body = GetComponent<Rigidbody> ();
		inputState = GetComponent<InputState> ();
	}

	// Update is called once per frame
	void Update () {

		var right = inputState.GetButtonValue (input [0]); // nær í value-ið á fyrst takkanum sem er mappaður(right hér)
		var left = inputState.GetButtonValue (input [1]);
		var velX = speed;

		if (right || left) {

			velX *= left ? -1 : 1; //margfaldar með -1 eð 1
		} else {
			velX = 0; //ef ekki verið að nýta á einn
		}

		//býr til nýtt velocit
		body.velocity = new Vector2 (velX, body.velocity.y);
	}
}
