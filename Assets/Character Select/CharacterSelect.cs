using UnityEngine;
using System.Collections;

public class CharacterSelect : MonoBehaviour {

	public static bool yellowPlayer;
	public static bool greenPlayer;
	public static bool redPlayer;
	public static bool bluePlayer;

	private GameObject p1;
	private GameObject p2;
	private GameObject p3;
	private GameObject p4;

	private GUIStyle subtitle_style;

	// Use this for initialization
	void Start () {
		p1 = GameObject.Find("p1");
		p2 = GameObject.Find("p2");
		p3 = GameObject.Find("p3");
		p4 = GameObject.Find("p4");

		subtitle_style = new GUIStyle();
		subtitle_style.normal.textColor = Color.white;
		subtitle_style.alignment = TextAnchor.MiddleCenter;
		subtitle_style.fontStyle = FontStyle.Bold;
		subtitle_style.font = (Font)Resources.Load ("VT323-Regular");
		subtitle_style.fontSize = 40;

		if(yellowPlayer){
			p1.guiTexture.enabled = true;
		}
		else{
			p1.guiTexture.enabled = false;
		}
		
		if(greenPlayer){
			p2.guiTexture.enabled = true;
		}
		else{
			p2.guiTexture.enabled = false;
		}
		
		if(redPlayer){
			p3.guiTexture.enabled = true;
		}
		else{
			p3.guiTexture.enabled = false;
		}
		
		if(bluePlayer){
			p4.guiTexture.enabled = true;
		}
		else{
			p4.guiTexture.enabled = false;
		}
	}
	
	// Update is called once per frame
	void Update () {

		if(Input.GetButtonDown("Start_1") && !yellowPlayer){
			yellowPlayer = true;
			p1.guiTexture.enabled = true;
		}
		else if(Input.GetButtonDown("Start_2") && !greenPlayer){
			greenPlayer = true;
			p2.guiTexture.enabled = true;
		}
		else if(Input.GetButtonDown("Start_3") && !redPlayer){
			redPlayer = true;
			p3.guiTexture.enabled = true;
		}
		else if(Input.GetButtonDown("Start_4") && !bluePlayer){
			bluePlayer = true;
			p4.guiTexture.enabled = true;
		}
		else{
			// Proceed
			if(Input.GetButtonDown("Start_1") && yellowPlayer && (greenPlayer || redPlayer || bluePlayer)){
				GoToStageSelect();
			}
			else if(Input.GetButtonDown("Start_2") && greenPlayer && (yellowPlayer || redPlayer || bluePlayer)){
				GoToStageSelect();
			}
			else if(Input.GetButtonDown("Start_3") && redPlayer && (yellowPlayer || greenPlayer || bluePlayer)){
				GoToStageSelect();
			}
			else if(Input.GetButtonDown("Start_2") && bluePlayer && (yellowPlayer || greenPlayer || redPlayer)){
				GoToStageSelect();
			}
		}

		if(Input.GetKeyDown(KeyCode.KeypadEnter)){
			GoToStageSelect();
		}
	}

	void OnGUI(){
		float x = 0.4f * Screen.width;
		float y = 0.01f * Screen.height;
		float w = 0.2f * Screen.width;
		float h = 0.1f * Screen.height;
		float padding = 0.02f * Screen.height;
		
		GUI.TextArea(new Rect(x,y,w,h), "Press start to join", subtitle_style);
	}

	private void GoToStageSelect(){
		Application.LoadLevel("StageSelect");
	}
}
