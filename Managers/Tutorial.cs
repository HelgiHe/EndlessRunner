using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Tutorial : MonoBehaviour {

	//UI references
	public Text instrucitonTxt;
	public Image mobileIcon;
	public Image touchRight;
	public Image touchLeft;
	public Image fadeImg;
	public float fadeSpeed;

	float timer;

	//difficulty breakpoints
	public float firstPart;
	public float secondPart;
	public float thirdPart;
	public float fourthPart;
	public float fifithPart;
	public float ending;

	bool gameIsRunning;
	bool fade;
	Swap swap;

	// Use this for initialization
	void OnEnable () {
		PlayerManger.OnPlayerDied += stopText;
		MoreMountains.InfiniteRunnerEngine.LevelManager.OnPlayerDeath += stopText;
	}
	void OnDisable () {
		PlayerManger.OnPlayerDied -= stopText;
		MoreMountains.InfiniteRunnerEngine.LevelManager.OnPlayerDeath -= stopText;
	}

	void Start () {
		swap = GetComponent<Swap> ();
		swap.enabled = false;
		gameIsRunning = true;
		fadeImg.enabled = false;
		mobileIcon.enabled = false;
		touchRight.enabled = false;
		touchLeft.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
	
		if (gameIsRunning) {
			timer += Time.deltaTime;

			if (timer > firstPart && timer < secondPart) {
				instrucitonTxt.text = "Tap the right side of the screen to jump";
				mobileIcon.enabled = true;
				touchRight.enabled = true;
			} else if (timer > secondPart && timer < thirdPart) {
				instrucitonTxt.text = "Hold to jump longer";
				mobileIcon.enabled = false;
				touchRight.enabled = false;
			} else if (timer > thirdPart && timer < fourthPart) {
				instrucitonTxt.text = "You can also double jump";
			} else if (timer > fourthPart && timer < fifithPart) {
				swap.enabled = true;
				instrucitonTxt.text = "When a platform is semi-transparent, \"Swap\" by tapping the left side of the screen";
				mobileIcon.enabled = true;
				touchLeft.enabled = true;
				StartCoroutine (SlowTime (3f));
			} else if (timer < firstPart) {
				instrucitonTxt.text = "";
			} else {
				fadeImg.enabled = true;
				mobileIcon.enabled = false;
				touchLeft.enabled = false;
				instrucitonTxt.text = "Good job, have fun";
				fade = true;
				StartCoroutine (goToStart (1.5f));
			}
		}
			
		//fade to black smoothly
		if(fade)
		{
			fadeImg.color = Color.Lerp(fadeImg.color, Color.black, fadeSpeed * Time.deltaTime);
		}

	}

	void stopText () {
		gameIsRunning = false;
		instrucitonTxt.text = "";
		mobileIcon.enabled = false;
		touchRight.enabled = false;
		touchLeft.enabled = false;
	}

	IEnumerator SlowTime(float waitTime) {
		
		float currentTimeScale = Time.timeScale;
		Time.timeScale = 0.5f;
		yield return new WaitForSeconds(waitTime);
		Time.timeScale = currentTimeScale;

	}

	IEnumerator goToStart (float waitTime){
		yield return new WaitForSeconds (waitTime);
		SceneManager.LoadScene(0);
	}

}
