
using UnityEngine;
using System.Collections;

//extendar Jump-Klasann
public class LongJump : Jump {

	public float longJumpMax;
	public float longJumpDelay = .15f;
	public float longJumpMultiplier = 1.5f;
	//public bool canLongJump;
	public bool isLongJumping;
	public bool increaseHeight = true;
	public float longJumpDamp;
	public float longJumpSpeed;


	protected override void OnEnable ()
	{
		base.OnEnable ();
		UserInputHandler.OnHold += onLongJump;
		UserInputHandler.OnRelease += StopLongJumpIncrease;
		UserInputHandler.OnTap += ResetLongJump;

	}

	protected override void OnDisable () {
		base.OnDisable ();
		UserInputHandler.OnHold -= onLongJump;
		UserInputHandler.OnRelease -= StopLongJumpIncrease;
		UserInputHandler.OnTap -= ResetLongJump;

	}
		

	protected override void Update(){

		if (collisionState.standing && isLongJumping)
			isLongJumping = false;

		base.Update (); //noraml logic-ið byrjar hérna

		//athugat hvort hægt sé að longjump-a, longJumpDelay er meira en jumpDelay, ekur velocity-ið
		/*if (canLongJump && !collisionState.standing) {

			var vel = body.velocity;
			body.velocity = new Vector2(vel.x, jumpSpeed * longJumpMultiplier);
			canLongJump = false;
			isLongJumping = true;
		}*/



	}


	void onLongJump(Touch touch, float hTime) {
		
		/*
		if (canLongJump && !collisionState.standing && hTime > longJumpDelay) {

			var vel = body.velocity;
			body.velocity = new Vector2(vel.x, jumpSpeed * longJumpMultiplier);
			canLongJump = false;
			isLongJumping = true;
		}*/


		if(hTime > longJumpDelay) {
			if (canLongJump && !collisionState.standing) {

				isLongJumping = true;
				//var vel = body.velocity;
				//body.velocity = new Vector2(vel.x, jumpSpeed + longJumpMultiplier * (1+hTime));
				//print (body.velocity.magnitude);
				StartCoroutine(jumpAccelaration(hTime));
				canLongJump = false;

			}
		}

	}

	IEnumerator jumpAccelaration(float holdTime){

		var vel = body.velocity;
		//holdTime = 0f;

		while (isLongJumping && holdTime < longJumpMax && increaseHeight) {

			holdTime += Time.deltaTime * 1f;
			body.velocity = new Vector2 (vel.x, longJumpSpeed - holdTime);

			yield return null;
		}
	}

	void StopLongJumpIncrease (){
		increaseHeight = false;
	}

	void ResetLongJump (Touch t) {
		increaseHeight = true;
	}

}
