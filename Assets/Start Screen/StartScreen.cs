using UnityEngine;
using System.Collections;

public class StartScreen : MonoBehaviour {

	public SpriteRenderer[] splashImages;

	int curSplash = 0;
	bool isVisible = false;

	void Start () {

	}

	void Update () {

		float color = Mathf.Sin (Time.timeSinceLevelLoad - Mathf.Deg2Rad * 89);


		splashImages[curSplash].color = new Color(color, 
		                                          color, 
		                                          color);

		isVisible = color > 0;

		//Debug.Log (color);
		//Debug.Log (Mathf.Sin (Time.timeSinceLevelLoad - Mathf.Deg2Rad * 89 - Time.deltaTime));

		if (!isVisible && color < Mathf.Sin (Time.timeSinceLevelLoad - Mathf.Deg2Rad * 89 - Time.deltaTime)) {
			if (++curSplash > splashImages.Length)
				Application.Quit();
				// load the next scene
		}

	}
}
