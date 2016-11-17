//sér um animationStates o.fl.
using UnityEngine;
using System.Collections;


public class PlayerManger : MonoBehaviour {


	public delegate void Death(MoreMountains.InfiniteRunnerEngine.PlayableCharacter playa);
	public static event Death OnDeath; 

	//reffa í components
	private InputState inputState;
	private Walk walkBehavior;
	private Animator animator;

	public MoreMountains.InfiniteRunnerEngine.PlayableCharacter player;

	void Awake(){

		inputState = GetComponent<InputState> ();
		walkBehavior = GetComponent<Walk> ();
		animator = GetComponent<Animator> ();
	}

	void Start () {

	}
		
	// Update is called once per frame
	void Update () {

		animator.speed = walkBehavior.running ? walkBehavior.runMultiplier : 1;
 
	}

	void ChangeAnimationState(int value){
		animator.SetInteger ("AnimState", value);
	}

	void OnTriggerEnter(Collider other) {
		if(other.CompareTag("Solid")) {
			
			StartCoroutine (KillPlaya (0.05f));

		}
	}

	public IEnumerator KillPlaya(float waitTime) {
		yield return new WaitForSeconds (waitTime);
		if(OnDeath != null) {
			OnDeath (player);
		}
	} 
}

