using UnityEngine;
using System.Collections;

public class Walk : AbstractBehavior {

	public float speed = 50f;
	public float runMultiplier = 2f;
	public bool running;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

		/*
		running = false;

		//sækir takkana
		var right = inputState.GetButtonValue (inputButtons [0]);
		var left = inputState.GetButtonValue (inputButtons [1]);
		var run = inputState.GetButtonValue (inputButtons [2]);

		if (right || left) {

			var tmpSpeed = speed;

			//eykur hraðann ef það er verið að hlaupa
			if(run && runMultiplier > 0){ //kemur í veg fyrir að playerinn stoppi
				tmpSpeed *= runMultiplier;
				running = true;
			}

			//margfaldar hraðann með direction enum-inum
			var velX = tmpSpeed * (float)inputState.direction;

			body.velocity = new Vector2(velX, body.velocity.y);

		}
*/

		body.velocity = new Vector2 (speed, body.velocity.y);
	}

}
