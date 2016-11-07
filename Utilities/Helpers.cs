using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[System.Serializable]
public struct StarTime {

	public float timeOneStar;
	public float timeTwoStar;
	public float timeThreeStar;

}

[System.Serializable]
public struct ScoreHandler {

	public string savePath;
	public Text textObj;
	public float levelScore;
}
