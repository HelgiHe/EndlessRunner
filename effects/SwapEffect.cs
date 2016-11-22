using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;

public class SwapEffect : MonoBehaviour {

	Colorful.Wiggle wiggleEffect;
	Colorful.Glitch glitchEffect;

	void Awake () {

		wiggleEffect = GetComponent<Colorful.Wiggle>();
		glitchEffect = GetComponent<Colorful.Glitch> ();
		wiggleEffect.enabled = false;
		glitchEffect.enabled = false;
	}

	public void OnEnable (){
		MoreMountains.InfiniteRunnerEngine.LevelManager.OnPlayerDeath += PlayWiggleEffect;
		Swap.OnSwap += PlayGlitchEffect;
	}

	public void OnDisable () {
		MoreMountains.InfiniteRunnerEngine.LevelManager.OnPlayerDeath += PlayWiggleEffect;
		Swap.OnSwap -= PlayGlitchEffect;
	}


	void PlayWiggleEffect () {
		wiggleEffect.enabled = true;
		StartCoroutine (Wiggle (0.4f));
	
	}

	IEnumerator Wiggle (float activeTime) {

		yield return new WaitForSeconds (activeTime);
		wiggleEffect.enabled = false;
	}

	void PlayGlitchEffect () {
		glitchEffect.enabled = true;
		StartCoroutine (Glitch (0.2f));
	}

	IEnumerator Glitch (float activeTime) {
		yield return new WaitForSeconds (activeTime);
		glitchEffect.enabled = false;
	}
}
	
