using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour {

	/*public GameObject gameManagerObj;
	MoreMountains.InfiniteRunnerEngine.GameManager gameManger;
*/
	public Text instrucitonTxt;
	public float timer;

	public float firstPart;
	public float secondPart;
	public float thirdPart;
	public float fourthPart;
	public float fifithPart;
	public float ending;

	// Use this for initialization
	void Awake () {
		//gameManger = gameManagerObj.GetComponent<MoreMountains.InfiniteRunnerEngine.GameManager> ();
	}
	
	// Update is called once per frame
	void Update () {
	
		timer += Time.deltaTime;

		if (timer > firstPart && timer < secondPart) {
			instrucitonTxt.text = "Tap the right side of the screen to jump";
		} else if (timer > secondPart && timer < thirdPart) {
			instrucitonTxt.text = "Hold to jump longer";
		} else if (timer > thirdPart && timer < fourthPart) {
			instrucitonTxt.text = "You can also double jump";
		} else if (timer > fourthPart && timer < fifithPart) {
			instrucitonTxt.text = "When a platform is semi-transparent, you need to \"Swap\" by tapping the left side of the screen";
			StartCoroutine (SlowTime (3f));
		} else if (timer < firstPart) {
			instrucitonTxt.text = "";
		}
		else {
			instrucitonTxt.text = "Good job, have fun";
		}
	}

	IEnumerator SlowTime(float waitTime) {
		
		float currentTimeScale = Time.timeScale;
		Time.timeScale = 0.5f;
		yield return new WaitForSeconds(waitTime);
		Time.timeScale = currentTimeScale;
	}
}
