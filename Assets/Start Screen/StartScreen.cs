using UnityEngine;
using System.Collections;

public class StartScreen : MonoBehaviour {

	public SpriteRenderer splashImage;
	public Animation	menu;
	delegate void StartScreenState ();

	StartScreenState state;

	bool isVisible = false;

	void Start () {
		state = SplashScreenUpdate;
		menu.wrapMode = WrapMode.Once;
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

		menu.Play();

		state = StartScreenAnimation;
	}

	void StartScreenAnimation () {
		if ( !menu.isPlaying || Input.GetButtonDown("Start_1") || Input.GetButtonDown("Start_2") || Input.GetButtonDown("Start_3") || Input.GetButtonDown("Start_4")) {
			menu["Slide Background"].time = 1000;
			state = StartScreenCheckForTheStartButton;
		}
	}

	void StartScreenCheckForTheStartButton () {
		if (Input.GetButtonDown("Start_1") || Input.GetButtonDown("Start_2") || Input.GetButtonDown("Start_3") || Input.GetButtonDown("Start_4")) {
			menu.Play ("Zoom Fade");

			state = StartScreenExiting;

			StartCoroutine("LoadCharacterSelectCoroutine");
		}
	}

	void StartScreenExiting () {

	}

	IEnumerator LoadCharacterSelectCoroutine () {
		yield return new WaitForSeconds(1);
		Application.LoadLevel("Character Select");
		// Load Character Select Scene
	}
}
