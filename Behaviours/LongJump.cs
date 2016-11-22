
using UnityEngine;
using System.Collections;

//extendar Jump-Klasann
public class LongJump : Jump {

	public float longJumpMax = 2f;
	public float longJumpDelay = .15f;
	public float longJumpMultiplier = 1.5f;
//	public bool canLongJump;
	public bool isLongJumping;

	protected override void OnEnable ()
	{
		base.OnEnable ();
		UserInputHandler.OnHold += onLongJump;
	}

	protected override void OnDisable () {
		base.OnDisable ();
		UserInputHandler.OnHold -= onLongJump;
	}
		

	protected override void Update(){

		if (collisionState.standing && isLongJumping)
			isLongJumping = false;

		base.Update (); //noraml logic-ið byrjar hérna

		//athugat hvort hægt sé að longjump-a, longJumpDelay er meira en jumpDelay, ekur velocity-ið
		if (canLongJump && !collisionState.standing) {

			var vel = body.velocity;
			body.velocity = new Vector2(vel.x, jumpSpeed * longJumpMultiplier);
			canLongJump = false;
			isLongJumping = true;
		} 
	}

	protected void onLongJump(float hTime){
		
		canLongJump = true;
	}

}
	


