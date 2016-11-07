// á hlutnum
using UnityEngine;
using System.Collections;
using System.Collections.Generic;


//athugar hvort og hversu lengi er verið að ýta á takkann
public class ButtonState{
	public bool value;
	public float holdTime = 0;
}

//states sem direction getur verið í
public enum Directions{
	Right = 1, //default values
	Left = -1
}

public class InputState : MonoBehaviour {

	public Directions direction = Directions.Right; //Right er default value
	public float absVelX = 0f;
	public float absVelY = 0f;

	private Rigidbody body;

	//collection með öllum buttonState 
	private Dictionary<Buttons, ButtonState> buttonStates = new Dictionary<Buttons, ButtonState>();

	void Awake(){
		body = GetComponent<Rigidbody> ();
	}

	void FixedUpdate(){
		//absoulute value á x og y velocity-inu
		absVelX = Mathf.Abs (body.velocity.x);
		absVelY = Mathf.Abs (body.velocity.y);
	}

	//Buttons er takkinn sem ýtt er á en value er hvort verið sé að ýta eða sleppa takkanum
	public void SetButtonValue(Buttons key, bool value){

		//ef key er ekki til er honum bætt við og ButtonState búið til sem fylgir key-inum
		if(!buttonStates.ContainsKey(key))
			buttonStates.Add(key, new ButtonState());

		var state = buttonStates [key]; //reference út Dictionary

		//athugat hvaða value-kmeur inn
		if (state.value && !value) { // ef value er false er verið að sleppa honum
			state.holdTime = 0; //holdTime resett-að
		} else if (state.value && value) { //annar er honum haldið niðri og timer telur hvers lengi
			state.holdTime += Time.deltaTime;
		}

		state.value = value; //value á Key-inu sett

	}

	//passar inn key enum-ið
	public bool GetButtonValue(Buttons key){
		if (buttonStates.ContainsKey (key)) // athugar hvort að key-inn sé tl
			return buttonStates [key].value;
		else
			return false;
	}
	//athugar hversju lengi ákveðinn takka er haldið niðr
	public float GetButtonHoldTime(Buttons key){
		if (buttonStates.ContainsKey (key))
			return buttonStates [key].holdTime;
		else
			return 0;
	}

}

