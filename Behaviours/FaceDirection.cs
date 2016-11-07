//snýr playernum
using UnityEngine;
using System.Collections;

//inheritar frá abstractBehaviuor
public class FaceDirection : AbstractBehavior {

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		var right = inputState.GetButtonValue (inputButtons [0]); //GetButtonValue returna boolían
		var left = inputState.GetButtonValue (inputButtons [1]);

		//paretn klasinnn gefur scriptunni aðgang að inputState
		if (right) {
			inputState.direction = Directions.Right;
		} else if (left) {
			inputState.direction = Directions.Left;
		}

		//Vector3 fyrir core hluti í Unity s.s. GaemObjects eða transforms, Vector2 fyrir physics
		transform.localScale = new Vector3 ((float)inputState.direction, 1, 1);
		//inputState er 1 eða -1 og breytir þannig direction-inu
	}
}
