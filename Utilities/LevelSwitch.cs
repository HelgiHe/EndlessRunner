using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelSwitch : MonoBehaviour {

	private int nextScene;
	public float transitionTime;

	IEnumerator loadNextLevel(float waitTime, int nextLevel) {

		yield return new WaitForSeconds (waitTime);
		SceneManager.LoadScene (nextLevel);
	}


	public void LoadTestLevel () {

		nextScene = 1;
		StartCoroutine(loadNextLevel(transitionTime, nextScene));

	}

	public void LoadLevelOne () {
		nextScene = 1;
		StartCoroutine(loadNextLevel(transitionTime, nextScene));
	}
}
