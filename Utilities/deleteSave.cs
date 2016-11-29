using UnityEngine;
using System.Collections;

public class deleteSave : MonoBehaviour {

	// Use this for initialization
	void Start () {

		if(ES2.Exists("highScore"))
			ES2.Delete("highScore");
	}
	

}
