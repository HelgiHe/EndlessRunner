using UnityEngine;
using System.Collections;

public class AnimatedTex : MonoBehaviour {

	private Renderer rend;

	[SerializeField]
	private float offsetSpeed = 0.05f;

	//Constructor;
	/*public AnimatedTex(float speed, Renderer renderer) {
		
		offsetSpeed = speed;
		rend = renderer;

	} */
	void Start () {
		rend = gameObject.GetComponent<Renderer> ();
		offsetSpeed *= (float)Random.Range (-3, 4);
	}

	// Update is called once per frame
	void Update () {
		
		//time.time returns the  time since the game started
		float offset = Time.time * offsetSpeed;
		rend.material.SetTextureOffset("_MainTex", new Vector2(offset, offset));

	}


	public void animateTex () {

		//rend = GetComponent<Renderer> ();

		//float offset = Time.time * offsetSpeed;

		//rend.material.SetTextureOffset("_MainTex", new Vector2 (offset, 0));

	}
}










