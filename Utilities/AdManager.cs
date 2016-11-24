using UnityEngine;
using System.Collections;
using UnityEngine.Advertisements;

public class AdManager : MonoBehaviour {

	[SerializeField] string gameId = "1209152";
	[SerializeField] int numberOfDeaths  = 10;

	void OnEnable () {
		MoreMountains.InfiniteRunnerEngine.LevelManager.OnPlayerDeath += CountDeaths; 
	}

	void OnDisable () {
		MoreMountains.InfiniteRunnerEngine.LevelManager.OnPlayerDeath -= CountDeaths; 
	}

	void Start() {
		if(ES2.Exists("numberOfDeaths")){
			numberOfDeaths = ES2.Load<int> ("numberOfDeaths");
		}
	}

	public void CountDeaths () {
		numberOfDeaths -= 1;

		if (numberOfDeaths <= 0) {
			ShowAd ();
			numberOfDeaths = 10;
		}

		ES2.Save (numberOfDeaths, "numberOfDeaths");
	}

	public void ShowAd () {

		StartCoroutine(WaitForAd());

		if (Advertisement.IsReady ()) {
			Advertisement.Show ();
		}
	}
		
	//freezes the game while the ad is showing
	IEnumerator WaitForAd() {
		float currentTimeScale = Time.timeScale;
		Time.timeScale = 0f; //freezes time
		yield return null;

		while (Advertisement.isShowing) {
			yield return null;

			Time.timeScale = currentTimeScale;
		}
	}
}
