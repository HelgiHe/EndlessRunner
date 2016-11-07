using UnityEngine;
using System.Collections;

public class DataManager : MonoBehaviour {


	public static DataManager datamanager;

	void Awake () {

		if (datamanager == null) {
			DontDestroyOnLoad (gameObject);
			datamanager = this;
		} else if (datamanager != this) {
			Destroy (gameObject);
		}

	}
}
