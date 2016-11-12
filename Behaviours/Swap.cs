using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Swap : MonoBehaviour {

	public delegate void Swapped ();
	public static event Swapped OnSwap;


	[SerializeField]
	Material matGlass;
	[SerializeField]
	Material matTex;

	//Parent objects of dark and light objects
	public GameObject darkObjectsParent;
	public GameObject lightObjectsParent;

	//List of the objects
	public List<GameObject> darkObjects;
	public List<GameObject> lightObjects;

	//true on dark platforms
	public bool isLightDimension = true;

	//Ref to the swapeffect
	SwapEffect swapEffect;

	void Awake () {

		GetLightObjects();
		GetDarkObjects ();


	}

	// Use this for initialization
	void Start () {

		float rand = Random.Range (0, 2);
		print (rand);

		disableColliders (darkObjects);
		swapEffect = GetComponent<SwapEffect> ();
	}

	void OnEnable(){
		UserInputHandler.OnLeftTap += swapMaterial;
		UserInputHandler.OnLeftTap += SwapDimensions;
		Death.playerDied += ResetMaterials;
	}

	void OnDisable () {
		UserInputHandler.OnLeftTap -= swapMaterial;
		UserInputHandler.OnLeftTap -= SwapDimensions;
		Death.playerDied -= ResetMaterials;


	}

	
	// Update is called once per frame
	void Update () {
		
		if(Input.GetKeyDown(KeyCode.L)) {
			swapMaterial ();
			isLightDimension = !isLightDimension;
		}
	}

	void GetLightObjects() {
		
		//Býr til array af gameObectum sem eru child af lightobjectsParent
		Transform [] lightObj = lightObjectsParent.GetComponentsInChildren<Transform>();

		lightObjects = new List<GameObject>(); //listinn sem er notaður

		foreach (Transform lightObject in lightObj) {

			//bætir við child hlutunum í lightObjects Listann
			if (lightObject != null && lightObject != lightObj[0]) {
				lightObjects.Add(lightObject.gameObject);
			}
		}
	}

	void GetDarkObjects() {

		Transform [] DarkObj = darkObjectsParent.GetComponentsInChildren<Transform>();

		darkObjects = new List<GameObject>(); //listinn sem er notaður

		foreach (Transform darkObject in DarkObj) {

			//bætir við child hlutunum í dark Listann
			if (darkObject != null && darkObject != DarkObj[0]) {
				darkObjects.Add(darkObject.gameObject);

			}
		}
	}

	void swapMaterial () {


		//Calls on the onSwap event and triggers the glitchEffect
		if (OnSwap != null) {
			OnSwap ();
		}

		//function swaps the material on All platform excep the first one
		foreach (GameObject darkObj in darkObjects) {

			//Getting the object ready
			Renderer materialRenderer;
			materialRenderer = darkObj.GetComponent<Renderer>();

			//swaps the status of the object
			Status status = darkObj.GetComponent<Status> ();
			status.isSelected = !status.isSelected;

			//Getting the Collider
			Collider col;
			col = darkObj.GetComponent<Collider> ();

			if (!isLightDimension) {
				materialRenderer.material = matGlass;
			//	col.enabled = false;
				col.isTrigger = true;


			} else {
				materialRenderer.material = matTex;

			//	col.enabled = true;
				col.isTrigger = false;
			}
		}

	
		foreach (GameObject lightObj in lightObjects) {
		
			//Getting the object ready for material switch
			Renderer materialRenderer;
			materialRenderer = lightObj.GetComponent<Renderer>();

			//swaps the status of the object
			Status status = lightObj.GetComponent<Status>();
			status.isSelected = !status.isSelected;

			//Getting the Colliders
			Collider col;
			col = lightObj.GetComponent<Collider> ();

			if (!isLightDimension) {
				materialRenderer.material = matTex;
			
				//col.enabled = true;
				col.isTrigger = false;

			} else {
				materialRenderer.material = matGlass;
				//col.enabled = false;
				col.isTrigger = true;
			}
		}
	}

	void disableColliders(List<GameObject> objs){

		foreach(GameObject someObject in objs) {

			Collider col = someObject.GetComponent<Collider> ();
			col.isTrigger = true;
		}
	}

	void SwapDimensions () {
		isLightDimension = !isLightDimension;
	}
		
	void ResetMaterials() {

		/*if (!isLightDimension) {
			swapMaterial ();
			isLightDimension = true;
		}*/
		StartCoroutine (waitForReset (0.4f));
	}

	IEnumerator waitForReset (float waitTime){
		
		yield return new WaitForSeconds(waitTime);
		if (!isLightDimension) {
			swapMaterial ();
			isLightDimension = true;
		}
	}
}
