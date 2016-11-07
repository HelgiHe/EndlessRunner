//á empty object
using UnityEngine;
using System.Collections;


//enum er type safe og því þægilegt að nota það til að tjékka á inputti
//í staðinn fyrir að þurfa muna ID á ákveðnum takka
public enum Buttons{
	Right,
	Left,
	Up,
	Down,
	A,
	B
}


//athugar hvaða takk er verið að nota
public enum Condition{
	GreaterThan,
	LessThan
}

//notað til að tjékka á axisState inn í Unity vélinni
//hluturinn dreginn á dæmið
[System.Serializable]
public class InputAxisState{
	public string axisName; //s.s. Horizontal
	public float offValue; //value á axis-inum
	public Buttons button; // mappar axis út Unity Input Managernum á Button enum-ið
	public Condition condition;

	public bool value{

		get{ //Read Only

			var val = Input.GetAxis(axisName); //value-ið á axis

			switch(condition){
			case Condition.GreaterThan:
				return val > offValue; //ef enum er gretar er val stærra en 0 þ.e. fer til hægri
			case Condition.LessThan:
				return val < offValue; // annars til vinstri
			}

			return false; //default return value
		}

	}
}

public class InputManager : MonoBehaviour {

	public InputAxisState[] inputs; //array af InputAxisState-s
	public InputState inputState; 

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

		foreach (var input in inputs) {
			inputState.SetButtonValue(input.button, input.value); //segir inputstate-inu hvaða valuue á lyklinum er 
		}
	}
}

