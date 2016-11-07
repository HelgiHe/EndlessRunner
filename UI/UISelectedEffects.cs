using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;


/* Resizes the button thats currentsly centered
 
 */

public class UISelectedEffects : MonoBehaviour {

	public RectTransform centerObject;

	public float triggerEffectDistance;

	public float newSizeX;
	public float newSizeY;

	public float orginalSizeX;
	public float orginalSizeY;

	float dcenterPos;
	float distanceToCenter;

	public RectTransform panel;
	RectTransform btnRectTransform;

	float distance;
	float offset;
	public float multiplier;

	public bool isSelected;

	Vector2 newSize;
	Vector2 orginalSize;

	Image image;
	Color color;
	// Use this for initialization
	void Awake () {

		btnRectTransform = gameObject.GetComponent<RectTransform>();
		image = gameObject.GetComponent<Image> ();
		color = image.color;
	}

	void Start (){
		orginalSize = new Vector2 (orginalSizeX, orginalSizeY);
		newSize = new Vector2 (newSizeX, newSizeY);
		offset = btnRectTransform.anchoredPosition.x;
	}


	// Update is called once per frame
	void Update () {

		distance = Mathf.Abs((panel.anchoredPosition.x + offset) - centerObject.anchoredPosition.x);

		if (distance < triggerEffectDistance) {
			btnRectTransform.sizeDelta = newSize;
			image.SetTransparency (0.9f);
		} 
		if(distance > triggerEffectDistance) {
			btnRectTransform.sizeDelta = orginalSize;
			image.SetTransparency (0.3f);
		}
	}
}
