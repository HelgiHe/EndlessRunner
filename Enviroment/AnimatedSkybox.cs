using UnityEngine;
using System.Collections;

public class AnimatedSkybox : MonoBehaviour {

	public float speed;

	float rot;
	Material sky;

	// Use this for initialization
	void Start () {
		sky = RenderSettings.skybox;
	}
	
	// Update is called once per frame
	void Update () {

		rot = speed * Time.time;
		sky.SetFloat ("_Rotation", rot);
	}
}
