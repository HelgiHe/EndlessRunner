using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SwapMaterial : MonoBehaviour {

	//material refs
	Renderer rend;
	[SerializeField]
	Material matGlass;
	[SerializeField]
	Material matTex;

	//object redfs
	public GameObject platformSpawner;
	List<GameObject> objectsInPool;
	MoreMountains.Tools.SimpleObjectPooler simpleObjectPooler;

	void OnEnable () {
		MoreMountains.InfiniteRunnerEngine.DistanceSpawner.OnSpawn += ApplyRandomMaterial;
	}

	void OnDisable () {
		MoreMountains.InfiniteRunnerEngine.DistanceSpawner.OnSpawn += ApplyRandomMaterial;
	}

	void Start () {

		simpleObjectPooler = platformSpawner.GetComponent<MoreMountains.Tools.SimpleObjectPooler> ();
		objectsInPool = simpleObjectPooler._pooledGameObjects;

		foreach(GameObject pooledObj in objectsInPool){
			ApplyRandomMaterial(pooledObj);
		}
	}

	// Update is called once per frame
	void Update () {


	}

	//swap the material and the the diemension boolean
	public void SwapDimensions () {


		foreach(GameObject pooledObj in objectsInPool){

			rend = pooledObj.gameObject.GetComponent<Renderer> ();
			Collider col = pooledObj.gameObject.GetComponent<Collider> ();

			if (rend.material.name == "matTex (Instance)") {
				rend.material = matGlass;
				col.isTrigger = true;
			} else if(rend.material.name == "glassy (Instance)"){
				rend.material = matTex;
				col.isTrigger = false;
			}
		
		}

	}
	//create a random number and assign the boolean and material
	public void ApplyRandomMaterial(GameObject obj) {
		
		rend = obj.gameObject.GetComponent<Renderer> ();
		Collider col = obj.gameObject.GetComponent<Collider> ();

		int randomNum = (int)Random.Range (0, 2);

			switch (randomNum) {
		case 0:
			rend.material = matTex;
			col.isTrigger = false;
				break;
		case 1:
			rend.material = matGlass;
			col.isTrigger = true;
				break;
			}

	}
}
