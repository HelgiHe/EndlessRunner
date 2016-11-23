using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SwapMaterial : MonoBehaviour {

	public float timeSinceStart;

	public float difficultyIncrementOne = 10f;
	public float difficultyIncrementTwo = 40f;
	public float difficultyIncrementThree = 60f;

	public float refractionValueOne;
	public float refractionValueTwo;
	public float refractionValueThree;

	public float transperancyValueOne;
	public float transperancyValueTwo;
	public float transperancyValueThree;

	public float refractionValue = 0.9f;
	public float transparencyValue = 0.8f;

	public float maxGapXOne = 23f;
	public float maxGapYOne = 6f;
	public float maxGapXTwo = 26f;
	public float maxGapYTwo = 6.5f;
	public float maxGapXThree = 30f;
	public float maxGapYThree = 7f;

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
	MoreMountains.InfiniteRunnerEngine.DistanceSpawner spawner;

	void OnEnable () {
		MoreMountains.InfiniteRunnerEngine.DistanceSpawner.OnSpawn += ApplyRandomMaterial;
		UserInputHandler.OnLeftTap += SwapDimensions;
	}

	void OnDisable () {
		MoreMountains.InfiniteRunnerEngine.DistanceSpawner.OnSpawn += ApplyRandomMaterial;
		UserInputHandler.OnLeftTap -= SwapDimensions;
	}
		
	void Start () {
		//spawner variable will enable changes between platforms and their size
		spawner = platformSpawner.GetComponent<MoreMountains.InfiniteRunnerEngine.DistanceSpawner> ();

		simpleObjectPooler = platformSpawner.GetComponent<MoreMountains.Tools.SimpleObjectPooler> ();
		objectsInPool = simpleObjectPooler._pooledGameObjects;

		foreach(GameObject pooledObj in objectsInPool){
			ApplyRandomMaterial(pooledObj);
		}
	}

	// Update is called once per frame
	void Update () {

		timeSinceStart += Time.deltaTime;
		//refraction should not be less than 0.4
	
		matGlass.SetFloat ("_Refraction", refractionValue);
		matGlass.SetFloat ("_Transparency", transparencyValue);

		setDifficulty ();

	}

	void setDifficulty () {

		if (timeSinceStart > difficultyIncrementOne && timeSinceStart < difficultyIncrementTwo) {
			spawner.MaximumGap.x = maxGapXOne;
			spawner.MaximumGap.y = maxGapYOne;
			refractionValue = refractionValueOne;
			transparencyValue = transperancyValueOne;

		} else if (timeSinceStart > difficultyIncrementTwo && timeSinceStart < difficultyIncrementThree) {
			spawner.MaximumGap.x = maxGapXTwo;
			spawner.MaximumGap.y = maxGapYTwo;
			refractionValue = refractionValueTwo;
			transparencyValue = transperancyValueTwo;
		} else if (timeSinceStart > difficultyIncrementThree) {
			spawner.MaximumGap.x = maxGapXThree;
			spawner.MaximumGap.y = maxGapYThree;
			refractionValue = refractionValueThree;
			transparencyValue = transperancyValueThree;
		} 
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
