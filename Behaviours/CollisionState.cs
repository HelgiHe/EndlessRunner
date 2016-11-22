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
	private Jump jump;

	// Use this for initialization
	void Awake () {
		inputState = GetComponent<InputState> ();
		sphereCol = GetComponent<SphereCollider> ();
		jump = GetComponent<Jump> ();
	}
		

	void OnCollisionEnter(Collision other) {
		
		if(other.gameObject.CompareTag("Solid")) {
			standing = true;
			jump.jumpsRemaining = jump.jumpCount;

		}
	}

	void OnCollisionExit(Collision other) {
		if(other.gameObject.CompareTag("Solid")) {
			standing = false;
		}
	}

}
