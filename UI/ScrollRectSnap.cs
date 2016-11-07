using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/* loops through an array of buttons 
 * and snaps to the center when onrelaeas
 * panel should start at 0
 * 
 * Buttons can be resized at will and the distance changed
 * To Add more buttons simply duplicate a button move them 300points in x and add them to the btn array
 */


public class ScrollRectSnap : MonoBehaviour {

	public RectTransform panel; // To hold the scrollpanel

	public Button[] btn;

	public RectTransform center; // center to compare the distance for each button

	//private variables
	private float[] distance;
	private bool dragging = false; // will be true when dragging the panel, diablear snapping
	private int btnDistance; // distance betwen the buttons
	private int minButtonNum; //smallest distance to center


	void Start () {
		int btnLength = btn.Length;
		distance = new float[btnLength];

		//Get Distance betwenn buttons

		btnDistance = (int)Mathf.Abs(btn[1].GetComponent<RectTransform>().anchoredPosition.x - btn[0].GetComponent<RectTransform>().anchoredPosition.x);
	}

	void Update () {

		for (int i = 0; i < btn.Length; i++){
			distance[i] = Mathf.Abs(center.transform.position.x - btn[i].transform.position.x);
		}

		float minDistance = Mathf.Min (distance); // min value in the array

		for (int a = 0; a < btn.Length; a++) {
			if (minDistance == distance [a]) {
				minButtonNum = a;
			}
		}
		if (!dragging) {
			LerpToBtn (minButtonNum * -btnDistance);
		}
	}
	void LerpToBtn (int position) {
		//increase the multiplier for slower Lerping
		float newX = Mathf.Lerp(panel.anchoredPosition.x, position, Time.deltaTime * 10f);
		Vector2 newPos = new Vector2 (newX, panel.anchoredPosition.y);

		panel.anchoredPosition = newPos;
	}

	public void StartDrag(){
		dragging = true;
	}

	public void EndDrag () {
		dragging = false;
	}
}