//athugar Collisions á playernum
//þarf að hafa collision layer í inpsectornum sama og Ground-ið
using UnityEngine;
using System.Collections;

public class CollisionState : MonoBehaviour {

	public LayerMask collisionLayer;
	public bool standing;
	public bool onWall;
	public Vector2 bottomPosition = Vector2.zero;
	public Vector2 rightPosition = Vector2.zero; //notað til vita hvort wallcollider sé hægri eða vinstri
	public Vector2 leftPosition = Vector2.zero;
	public float collisionRadius = 10f;
	public Color debugCollisionColor = Color.red; //býr til litinn

	private SphereCollider sphereCol;

	private InputState inputState;
	private LongJump jump;

	// Use this for initialization
	void Awake () {
		inputState = GetComponent<InputState> ();
		sphereCol = GetComponent<SphereCollider> ();
		jump = GetComponent<LongJump> ();
	}

	// Update is called once per frame
	void Update () {

	}

	void OnTriggerEnter(Collider other) {
		if(other.gameObject.CompareTag("Solid")) {
			standing = true;
			jump.jumpsRemaining = jump.jumpCount;
			jump.increaseHeight = true;
		}
	}

	void OnTriggerExit(Collider other) {
		if(other.gameObject.CompareTag("Solid")) {
			standing = false;
		}
	}
	

	void FixedUpdate(){


		/*
		var pos = bottomPosition;
		pos.x += transform.position.x;
		pos.y += transform.position.y;


		pos = inputState.direction == Directions.Right ? rightPosition : leftPosition; //returnar bool
		pos.x += transform.position.x;
		pos.y += transform.position.y;

		onWall = Physics.OverlapSphere (pos, collisionRadius, collisionLayer);
		*/
	}


	/*
	void OnDrawGizmos(){ // sést bara í Editonum

		Gizmos.color = debugCollisionColor;

		//array af position-s
		var positions = new Vector2[] {rightPosition, bottomPosition, leftPosition};

		foreach (var position in positions) {
			//teiknar hring þar sem collisionRadius-i er
			var pos = position;
			pos.x += transform.position.x;
			pos.y += transform.position.y;


			Gizmos.DrawWireSphere (pos, collisionRadius);
		}
	}
	*/
}
