using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Score : MonoBehaviour {

	/*
	 * Sækir seinsast high score ef það er til
	 * replace-ar það ef það er hærra
	 * 
	 * 
	 */

	public float currentBestTime;
	public float score;
	public float timer = 0;

	public string saveKeyString;

	public GameObject endLevelObject;
	EndLevel endLevel;

	float keyScore;

	void OnEnable() {
		EndLevel.onWin += saveKey;
		Death.playerDied += resetTimer;
	}
	void OnDisable (){
		EndLevel.onWin -= saveKey;
		Death.playerDied += resetTimer;
	}
		
	// Use this for initialization
	void Start () {
		
		endLevel = endLevelObject.GetComponent<EndLevel> ();

		if (ES2.Exists (saveKeyString)) {
			currentBestTime = ES2.Load<float>(saveKeyString);
		}
	
	}
	
	// Update is called once per frame
	void Update () {
	

		if (!endLevel.finishedLevel) {
			timer += Time.deltaTime;
		}


	}

	void saveKey () {

		if (timer < currentBestTime || currentBestTime == 0f) {
			currentBestTime = timer;
			ES2.Save (currentBestTime, saveKeyString);
		}
	}
		
	void resetTimer () {
		timer = 0;
	}
}
	

