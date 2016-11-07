using UnityEngine;
using System.Collections;

public class QuitGame : MonoBehaviour {


	void Update () {

		if (Input.GetKey (KeyCode.Escape)) {
			Quit ();
		}

	}


	public static void Quit () {

		Application.Quit ();
	}
}
