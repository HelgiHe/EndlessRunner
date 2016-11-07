using UnityEngine;
using System.Collections;

public class AnimatedNorm : MonoBehaviour {

	private Renderer rend;

	[SerializeField]
	private float offsetSpeed = 0.05f;

	
	void Start () {
		rend = gameObject.GetComponent<Renderer> ();
		//offsetSpeed *= (float)Random.Range (-3, 4);
	}
	
	// Update is called once per frame
	void Update () {


		//time.time returns the  time since the game started
		float offset = Time.time * offsetSpeed;
		rend.material.SetTextureOffset("_NormalMap", new Vector2(0, offset));
	}


	void RandomNum () {
	
		offsetSpeed *= (float)Random.Range (-2, 2);

		if (offsetSpeed == 0f) {
			offsetSpeed *= (float)Random.Range (-2, 2);
		}
	}
}
