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
	public GameObject objectsParent;

	//List of the objects
	public List<GameObject> Objects;


	//true on dark platforms
	public bool isLightDimension = true;

	//Ref to the swapeffect
	SwapEffect swapEffect;

	void Awake () {

		GetObjects ();
	}

	// Use this for initialization
	void Start () {

		
		swapMaterial ();

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



	void GetObjects() {

		Transform [] DarkObj = objectsParent.GetComponentsInChildren<Transform>();

		Objects = new List<GameObject>(); //listinn sem er notaður

		foreach (Transform darkObject in DarkObj) {

			//bætir við child hlutunum í dark Listann
			if (darkObject != null && darkObject != DarkObj[0]) {
				Objects.Add(darkObject.gameObject);

			}
		}
	}

	void swapMaterial () {


		//Calls on the onSwap event and triggers the glitchEffect
		if (OnSwap != null) {
			OnSwap ();
		}



		//function swaps the material on All platform excep the first one
		foreach (GameObject darkObj in Objects) {

			//creating random dimension
			int rand = (int)Random.Range (0, 2);

			switch (rand) {
			case 0:
				isLightDimension = true;
				break;
			case 1:
				isLightDimension = false;
				break;
			}

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
