//sér um animationStates o.fl.
using UnityEngine;
using System.Collections;


public class PlayerManger : MonoBehaviour {

	//reffa í components
	private InputState inputState;
	private Walk walkBehavior;
	private Animator animator;
	private CollisionState collisionState; //ref í collisionState-ið
	private LongJump jumpState;


	void Awake(){

		inputState = GetComponent<InputState> ();
		walkBehavior = GetComponent<Walk> ();
		animator = GetComponent<Animator> ();
		collisionState = GetComponent<CollisionState> ();
		jumpState = GetComponent<LongJump> ();
	}

	// Use this for initialization
	void Start () {

	}
		

	// Update is called once per frame
	void Update () {


		if (collisionState.standing) { //tjékkar og athugar hvort player-inn sé grounded
			ChangeAnimationState(0);
		}

		if (inputState.absVelX > 0 && collisionState.standing) { // ef það er meira en 0 spila walk animation-ið
			ChangeAnimationState(1);
		}

		if (inputState.absVelY > 0 && !collisionState.standing) {
			ChangeAnimationState(2);
		}

		if (!collisionState.standing && jumpState.jumpsRemaining < 1) {
			ChangeAnimationState (3);
		}


		animator.speed = walkBehavior.running ? walkBehavior.runMultiplier : 1;
 
	}

	void ChangeAnimationState(int value){
		animator.SetInteger ("AnimState", value);
	}
}

