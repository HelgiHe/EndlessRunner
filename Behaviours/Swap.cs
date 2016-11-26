using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Swap : MonoBehaviour {

	[SerializeField]
	Material matGlass;
	[SerializeField]
	Material matTex;
	[SerializeField]
	GameObject platform;

	Renderer rend;
	Collider col;

	// Use this for initialization
	void Start () {
		rend = platform.GetComponent<Renderer> ();
		col = platform.GetComponent<Collider> ();
	}

	void OnEnable(){
		UserInputHandler.OnLeftTap += swapMaterial;
	}

	void OnDisable () {
		UserInputHandler.OnLeftTap -= swapMaterial;
	}

	
	// Update is called once per frame
	void Update () {
		
	
	}
		
	void swapMaterial () {
		
		if (rend.material.name == "matTex (Instance)") {
			rend.material = matGlass;
			col.isTrigger = true;
		} else if(rend.material.name == "glassy (Instance)"){
			rend.material = matTex;
			col.isTrigger = false;
		}

	}
}
