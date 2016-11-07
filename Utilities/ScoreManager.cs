using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

	public Text levelOneScoreText;
	public float levelOneScore;

	//public ScoreHandler levelOne;

	public string levelOneScoreKey;


	float keyScore;
	float levelOneHighScore;

	// Use this for initialization
	void Awake () {
	
		getHighScore ();

	}

	void Start () {

		/*
		if (ES2.Exists (levelOneScoreKey)) {
			levelOneHighScore = ES2.Load<float>(levelOneScoreKey);
			testText.text = levelOneHighScore.ToString ();
		}
		*/
	}


	void getHighScore () {

		if (ES2.Exists (levelOneScoreKey)) {
			levelOneScore = ES2.Load<float> (levelOneScoreKey);
			levelOneScore = (float)System.Math.Round(levelOneScore, 2);
			levelOneScoreText.text = levelOneScore.ToString ();
		}
	}
}
