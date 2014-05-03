using UnityEngine;
using System.Collections;

public enum Levels{

	level1,
	level2,
};

public class StageSelect : MonoBehaviour {

	public static string chosenLevel;

	public Texture _level_1_texture;
	public Texture _level_2_texture;

	public float _x;
	public float _y;
	public float _w;
	public float _h;


	private GUIStyle subtitle_style;
	private string[] _levels;
	private int _selectedIndex;
	private int _numLevels;

	private bool stick_1_down;
	private bool stick_2_down;
	private bool stick_3_down;
	private bool stick_4_down;

	// Use this for initialization
	void Start () {
		_levels = new string[] {"level_01","level_03"};
		_selectedIndex = (int)Levels.level1;
		_numLevels = _levels.Length;

		subtitle_style = new GUIStyle();
		subtitle_style.normal.textColor = Color.white;
		subtitle_style.alignment = TextAnchor.MiddleCenter;
		subtitle_style.fontStyle = FontStyle.Normal;
		subtitle_style.fontSize = 40;
	}

	private void CheckForDown(){
		//Player 1
		if(Input.GetAxis("L_YAxis_1") == 1 && !stick_1_down){
			if(_selectedIndex >= _numLevels-1){
				_selectedIndex = (int)Levels.level1; //first level in list goes here
			}
			else{
				_selectedIndex++;
			}
			stick_1_down = true;
		}
		else if(Input.GetAxis("L_YAxis_1") == 0){
			stick_1_down = false;
		}

		//Player 2
		if(Input.GetAxis("L_YAxis_2") == 1 && !stick_2_down){
			if(_selectedIndex >= _numLevels-1){
				_selectedIndex = (int)Levels.level1; //first level in list goes here
			}
			else{
				_selectedIndex++;
			}
			stick_2_down = true;
		}
		else if(Input.GetAxis("L_YAxis_2") == 0){
			stick_2_down = false;
		}

		//Player 3
		if(Input.GetAxis("L_YAxis_3") == 1 && !stick_3_down){
			if(_selectedIndex >= _numLevels-1){
				_selectedIndex = (int)Levels.level1; //first level in list goes here
			}
			else{
				_selectedIndex++;
			}
			stick_3_down = true;
		}
		else if(Input.GetAxis("L_YAxis_3") == 0){
			stick_3_down = false;
		}

		//Player 4
		if(Input.GetAxis("L_YAxis_4") == 1 && !stick_4_down){
			if(_selectedIndex >= _numLevels-1){
				_selectedIndex = (int)Levels.level1; //first level in list goes here
			}
			else{
				_selectedIndex++;
			}
			stick_4_down = true;
		}
		else if(Input.GetAxis("L_YAxis_4") == 0){
			stick_4_down = false;
		}
	}

	private void CheckForUp(){
		// Player 1
		if(Input.GetAxis("L_YAxis_1") == -1 && !stick_1_down){
			if(_selectedIndex <= 0){
				_selectedIndex = (int)Levels.level2; //last level in list goes here
			}
			else{
				_selectedIndex--;
			}
			stick_1_down = true;
		}
		else if(Input.GetAxis("L_YAxis_1") == 0){
			stick_1_down = false;
		}

		// Player 2
		if(Input.GetAxis("L_YAxis_2") == -1 && !stick_2_down){
			if(_selectedIndex <= 0){
				_selectedIndex = (int)Levels.level2; //last level in list goes here
			}
			else{
				_selectedIndex--;
			}
			stick_2_down = true;
		}
		else if(Input.GetAxis("L_YAxis_2") == 0){
			stick_2_down = false;
		}

		// Player 3
		if(Input.GetAxis("L_YAxis_3") == -1 && !stick_3_down){
			if(_selectedIndex <= 0){
				_selectedIndex = (int)Levels.level2; //last level in list goes here
			}
			else{
				_selectedIndex--;
			}
			stick_3_down = true;
		}
		else if(Input.GetAxis("L_YAxis_3") == 0){
			stick_3_down = false;
		}

		// Player 4
		if(Input.GetAxis("L_YAxis_4") == -1 && !stick_4_down){
			if(_selectedIndex <= 0){
				_selectedIndex = (int)Levels.level2; //last level in list goes here
			}
			else{
				_selectedIndex--;
			}
			stick_4_down = true;
		}
		else if(Input.GetAxis("L_YAxis_4") == 0){
			stick_4_down = false;
		}
	}

	// Update is called once per frame
	void Update () {

		// B for going back
		if((Input.GetButtonDown("B_1") && CharacterSelect.yellowPlayer)
		   || (Input.GetButtonDown("B_2") && CharacterSelect.greenPlayer)
		   || (Input.GetButtonDown("B_3") && CharacterSelect.redPlayer)
		   || (Input.GetButtonDown("B_4") && CharacterSelect.bluePlayer)){

			Application.LoadLevel("CharacterSelect");
		}

		// Down
		CheckForDown();

		// Up
		CheckForUp();

		// Start to proceed
		if(Input.GetButtonDown("Start_1") || 
		   Input.GetButtonDown("Start_2") || 
		   Input.GetButtonDown("Start_3") || 
		   Input.GetButtonDown("Start_4")
		   ){
			if(_levels[_selectedIndex] == "level_01"){
				chosenLevel = "level_01";
				Application.LoadLevel("level_01");
			}
			else if(_levels[_selectedIndex] == "level_03"){
				chosenLevel = "level_03";
				Application.LoadLevel("level_03");
			}
		}

	}

	void OnGUI(){

		float xs = 0.4f * Screen.width;
		float ys= 0.01f * Screen.height;
		float ws = 0.2f * Screen.width;
		float hs = 0.1f * Screen.height;
		
		GUI.TextArea(new Rect(xs,ys,ws,hs), "Select Stage", subtitle_style);

		float xg = _x * Screen.width;
		float yg = _y * Screen.height;
		float wg = _w * Screen.width;
		float hg = _h * Screen.height;
		float padding = 0.3f * Screen.height;

		if(_selectedIndex == (int)Levels.level1){
			GUI.color = Color.white;
		}
		else{
			GUI.color = Color.gray;
		}
		GUI.DrawTexture(new Rect(xg,yg,wg,hg), _level_1_texture);

		yg += padding;

		if(_selectedIndex == (int)Levels.level2){
			GUI.color = Color.white;
		}
		else{
			GUI.color = Color.gray;
		}
		GUI.DrawTexture(new Rect(xg,yg,wg,hg), _level_2_texture);

		yg += padding;
	}
}
