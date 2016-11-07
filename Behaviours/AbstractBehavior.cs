using UnityEngine;
using System.Collections;

//note abstract keyword segir öllum að ekki er hægt að instanitate klasan eða bæta hinum við sem component
//aðrir behaviur klasann nota þennan klasa til að búa til grunn behavour án þess að þurfa að duplicate-a kóðann útum
//allt

public abstract class AbstractBehavior : MonoBehaviour {

	public Buttons[] inputButtons;

	//arrya af behavour scriptum
	public MonoBehaviour[] dissableScripts;

	protected InputState inputState;
	protected Rigidbody body;
	protected CollisionState collisionState;

	protected virtual void Awake(){
		inputState = GetComponent<InputState> ();
		body = GetComponent<Rigidbody> ();
		collisionState = GetComponent<CollisionState> ();
	}

	//Disablar öll scriptin í viðeigandu array
	protected virtual void ToggleScripts(bool value){
		foreach (var script in dissableScripts) {
			script.enabled = value;
		}
	}
}
