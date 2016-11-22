using UnityEngine;
using System.Collections;

public class UserInputHandler : MonoBehaviour {

	public delegate void TapAction(); //delgate is a reference to a method
	public static event TapAction OnRightTap; //Event is method sent to other objects mep onenable() og ondisable

	public delegate void HoldAction(float hTime); 
	public static event HoldAction OnHold;

	public delegate void ReleaseAction();
	public static event ReleaseAction OnRelease;

	public delegate void LeftTap();
	public static event LeftTap OnLeftTap;


	public bool startCounting;
	public float holdtime;

	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {


		if (Input.touchCount > 0) {

			foreach (Touch touch in Input.touches) {
				
				//Touch touch = Input.touches [0]; //Array of all the touches on the screen

				if (touch.phase == TouchPhase.Began) { //phases er svipað  og getmousrbuttonDown og -up og músarklikkki
					//holdtime += Time.deltaTime;

					if (OnRightTap != null && touch.position.x > Screen.width / 2) {
						startCounting = true;
						OnRightTap ();

					} else if (OnLeftTap != null && touch.position.x <= Screen.width / 2) {
						if (OnLeftTap != null) {
							OnLeftTap ();
						}
					} 
				} else if (touch.phase == TouchPhase.Stationary && touch.position.x > Screen.width / 2) {
					holdtime += Time.deltaTime;
					if (OnHold != null) {
						OnHold (holdtime);
					}
				}
					
				else if (touch.phase == TouchPhase.Ended && touch.position.x > Screen.width / 2) {
	
					holdtime = 0;

					if (OnRelease != null) {
						OnRelease ();
					}
				}

			}
		}
	}
}


	

