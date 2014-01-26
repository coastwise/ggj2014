using UnityEngine;
using System.Collections;

public class StartScreen : MonoBehaviour {

	public SpriteRenderer splashImage;
	public Animation	background;
	public Animation	title;
	public Animation	startButton;
	delegate void StartScreenState ();

	StartScreenState state;

	int curSplash = 0;
	bool isVisible = false;

	void Start () {
		state = SplashScreenUpdate;
		background.wrapMode = WrapMode.Once;
		title.wrapMode = WrapMode.Once;
		startButton.wrapMode = WrapMode.Once;
	}

	void Update () {
		state();
	}

	void SplashScreenUpdate () {
		float color = Mathf.Sin (Time.timeSinceLevelLoad - Mathf.Deg2Rad * 89);

		splashImage.color = new Color(1, 1, 1, color);
		
		isVisible = color > 0;
		if (!isVisible && color < Mathf.Sin (Time.timeSinceLevelLoad - Mathf.Deg2Rad * 89 - Time.deltaTime)) {
			state = StartScreenInit;
		}
	}


	void StartScreenInit () {

		Debug.Log ("Init");
		background.Play();
		
		Debug.Log ("Init2");

		title.Play();
		startButton.Play();

		state = StartScreenUpdate;
	}

	void StartScreenUpdate () {

	}
}
