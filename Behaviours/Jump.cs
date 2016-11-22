using UnityEngine;
using System.Collections;

public class Jump : AbstractBehavior {

	public float jumpSpeed = 200f;
	public float holdJumpSpeed = 2f;
	public float jumpDelay = .1f; //tími á milli hoppa
	public int jumpCount = 2; //hversu oft er hægt að hoppa

	public float maxJumpTime = 0.2f;
	public float holdTime;
	public bool starCounting;

	protected float lastJumpTime = 0;
	public int jumpsRemaining = 0;

	public bool canLongJump = false;

	// Use this for initialization
	protected virtual void OnEnable ()
	{
		UserInputHandler.OnRightTap += OnJump;
		//UserInputHandler.OnHold += isHolding;
	}

	protected virtual void OnDisable () {
		UserInputHandler.OnRightTap -= OnJump;
		//UserInputHandler.OnHold -= isHolding;
	}

	protected virtual void Start () {
		jumpsRemaining = jumpCount;
	}
		

	// Update is called once per frame
	protected virtual void Update () {

		if (starCounting) {
			holdTime += Time.deltaTime;
		}
			
	}
	//initial jump Speed
	protected virtual void OnJump(){

		if (collisionState.standing || !collisionState.standing && jumpsRemaining > 0) {

			var vel = body.velocity; 
			//lastJumpTime = Time.time;//óþarfi að nota delta í þessu tilfelli

			//maintain-ar x.velocity-inu en bætir við jumpSpeed á Y-ásnum
			body.velocity = new Vector2 (vel.x, jumpSpeed);
			jumpsRemaining -= 1;

			canLongJump = true;
		}
	}

	public void isHolding(float hTime){
	 
		StartCoroutine (increaseHeight (hTime));
	}

	public void resetCounting() {
		starCounting = false;
		holdTime = 0f;
	}

	IEnumerator increaseHeight(float holdTime) {
		var vel = body.velocity;
		while (holdTime < maxJumpTime) {
			body.velocity = new Vector2 (vel.x, holdJumpSpeed += holdTime * Time.deltaTime);
			print (holdTime);
			yield return null;
		}

	}
}
