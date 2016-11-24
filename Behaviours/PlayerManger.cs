//sér um animationStates o.fl.
using UnityEngine;
using System.Collections;


public class PlayerManger : MonoBehaviour {


	public delegate void Death(MoreMountains.InfiniteRunnerEngine.PlayableCharacter playa);
	public static event Death OnDeath;
	public delegate void Died();
	public static event Died OnPlayerDied; 

	//reffa í components
	private Walk walkBehavior;
	static Animator animator;
	CollisionState collisionState;
	Jump jump;

	AudioSource sound;


	public MoreMountains.InfiniteRunnerEngine.PlayableCharacter player;

	void Awake(){

		walkBehavior = GetComponent<Walk> ();
		animator = GetComponent<Animator> ();
		collisionState = GetComponent<CollisionState> ();
		jump = GetComponent<Jump> ();
		sound = GetComponent<AudioSource> ();
	}


		
	// Update is called once per frame
	void Update () {

		//animator.speed = walkBehavior.running ? walkBehavior.runMultiplier : 1;
 		
		if (collisionState.standing) {
			ChangeAnimationState (0);
		}

		if (collisionState.standing && !jump.isJumping) {
			
		}

		if (!collisionState.standing && jump.isJumping) {
			if (jump.jumpsRemaining >= 1) {
				ChangeAnimationState (1);
			}
			if (jump.jumpsRemaining == 0) {
				ChangeAnimationState (2);
			}
		}
	}

	public static void ChangeAnimationState(int value){
		animator.SetInteger ("AnimState", value);

	}

	void OnTriggerEnter(Collider other) {
		if(other.CompareTag("Solid")) {
			
			StartCoroutine (KillPlaya (0.03f));

		}
	}

	public IEnumerator KillPlaya(float waitTime) {
		yield return new WaitForSeconds (waitTime);
		if(OnDeath != null) {
			OnDeath (player);
		}
		if (OnPlayerDied != null) {
			OnPlayerDied ();
		}
	} 
}

