using UnityEngine;
using System.Collections;

public class UserInputHandler : MonoBehaviour {

	public delegate void TapAction(Touch t); //delgate is a reference to a method
	public static event TapAction OnTap; //Event is method sent to other objects mep onenable() og ondisable

	public delegate void HoldAction(Touch t, float hTime); 
	public static event HoldAction OnHold;

	public delegate void ReleaseAction();
	public static event ReleaseAction OnRelease;

	public delegate void LeftTap();
	public static event LeftTap OnLeftTap;

	/*
	public float tapMaxMovement = 50f; //maximum a touch can be moved to be considered a tap

	private Vector2 movement; //how far we have moved

	private bool tapGestureFailed = false; // ef user-inn færir puttann of langt
*/

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

					if (OnTap != null && touch.position.x > Screen.width / 2) {
						OnTap (touch);

					} else if (OnLeftTap != null && touch.position.x <= Screen.width / 2) {
						OnLeftTap ();
					}
				} else if (touch.phase == TouchPhase.Stationary && touch.position.x > Screen.width / 2) { //eftir Began phase-inu
					//should longJump
					holdtime += Time.deltaTime;
			
					if (OnHold != null) {
						OnHold (touch, holdtime);
					}

				} else if (touch.phase == TouchPhase.Ended && touch.position.x > Screen.width / 2) {

					holdtime = 0;

					if (OnRelease != null) {
						OnRelease ();
					}
				}

			}
		}
			//else
			
				/*if (!tapGestureFailed)
				{
					if (OnTap != null)  //er null ef engin önnur script hafa register-að
						OnTap(touch); // sendit event-ið með current-tpuch sem parameter
					
				}

				tapGestureFailed = false; 
				*/
			
		}
}


	

