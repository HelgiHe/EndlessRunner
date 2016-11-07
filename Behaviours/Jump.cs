using UnityEngine;
using System.Collections;

public class Jump : AbstractBehavior {

	public float jumpSpeed = 200f;
	public float jumpDelay = .1f; //tími á milli hoppa
	public int jumpCount = 2; //hversu oft er hægt að hoppa


	protected float lastJumpTime = 0;
	public int jumpsRemaining = 0;

	public bool canLongJump = false;

	// Use this for initialization
	protected virtual void OnEnable ()
	{
		UserInputHandler.OnTap += OnJump;
	}

	protected virtual void OnDisable () {
		UserInputHandler.OnTap -= OnJump;
	}

	protected virtual void Start () {
		jumpsRemaining = jumpCount;
	}
		

	// Update is called once per frame
	protected virtual void Update () {

		//var canJump = inputState.GetButtonValue (inputButtons [0]); //sækir takkan út unputButtonArray-inu
		//var holdTime = inputState.GetButtonHoldTime (inputButtons [0]);
			
	}

	protected virtual void OnJump(Touch t){

		if (collisionState.standing || !collisionState.standing && jumpsRemaining > 0) {

			var vel = body.velocity; 
			lastJumpTime = Time.time;//óþarfi að nota delta í þessu tilfelli

			//maintain-ar x.velocity-inu en bætir við jumpSpeed á Y-ásnum
			body.velocity = new Vector2 (vel.x, jumpSpeed);
			jumpsRemaining -= 1;

			canLongJump = true;
		}
	}
}
