using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//Do not let setup move on to game if beginProblem or endProblem is true
//Display a message if beginProblem or endProblem is true;
//reset endProblem and beginProblem to false when moving to game

public class Setup : Game 
{
	/*public static Accordian Instance {get; private set;} //This set works but method doesnt work
	
	void Awake()
	{
		Instance = this;	
	}*/
	private static Setup instance;
	
	public static Setup Instance
	{
		 get { return instance ?? (instance = new GameObject("Setup").AddComponent<Setup>()); }
    
	}
	public bool startPressed = false;
	public bool setupDisplayed = false;
	GUIContent[] comboBoxList;
	private List<ComboBox> inputBoxes = new List<ComboBox> ();
	//ComboBox comboBoxControl;// = new ComboBox();
	GUIStyle listStyle = new GUIStyle();
	//public GUISkin CBox;
	private bool beginProblem = false;
	private bool endProblem = false;


	private int[,] mazeData = new int[16,16];
	string instructions = "Press each button to choose terrain type. \n O = Open \n S = Swamp \n G = Grass \n # = Obstacle \n B = Begin \n E = End \n Choose \"E\" and \"B\" only once each. Press \"Random Start\" to have the program randomly generate the terrain map for you.";
	
	/*public override void startSetup()
	{
		Debug.Log("Setup");
		clearMazeData ();
		setDisplayButtons();
		//SetBoardState.E
		//GameObject obj;
		//obj = (GameObject)Instantiate (PlayingCard, transform.position,transform.rotation);
		//Instantiate (PlayingCard, new Vector3(0,0,0), Quaternion.identity);
		//PlayingCard card = new PlayingCard();
	}*/

	
	public void setDisplayButtons()
	{
		//Destroy the button grid when leaving screen!!!!

		Debug.Log ("test");
		int x = 300;
		int y = 0;

		for (int i=0; i<16; i++) {
				for (int j=0; j<16; j++) {

					comboBoxList = new GUIContent[6];
					comboBoxList [0] = new GUIContent ("O");
					comboBoxList [1] = new GUIContent ("S");
					comboBoxList [2] = new GUIContent ("G");
					comboBoxList [3] = new GUIContent ("#");
					comboBoxList [4] = new GUIContent ("E");
					comboBoxList [5] = new GUIContent ("B");
		
					listStyle.normal.textColor = Color.white; 
					listStyle.fontSize = 12;

					listStyle.active.textColor = Color.black;
			
								// Create the texture and set its colour.
								//Texture2D blackTexture = new Texture2D(1,1);
								//blackTexture.SetPixel(1,1,Color.black);
				//blackTexture.Apply();
				//listStyle.active.background = blackTexture;
								listStyle.onHover.background = listStyle.hover.background = new Texture2D (2, 2);
								listStyle.padding.left = listStyle.padding.right = listStyle.padding.top = listStyle.padding.bottom = 4;
								ComboBox comboBoxControl;
								comboBoxControl = new ComboBox (new Rect (x, y, 37, 37), comboBoxList [0], comboBoxList, "button", "box", listStyle);
				//listStyle.active.background.alphaIsTransparency = true;
								inputBoxes.Add (comboBoxControl);
				x +=37;

			}
			x = 300;
			y += 37;
		}
		/*
		 * //Place Prefab
		GameObject grassland = (GameObject)Instantiate(Resources.Load("Grassland"));
		Vector3 testPosition = new Vector3 (0, 0, 0);
		grassland.transform.position = testPosition;
		*/
	}
	
	private void OnGUI () 
	{
		//GUI.skin = CBox;
		//Color colGUI = GUI.color;
		
		//colGUI.a = 1f;
		
		//GUI.color = colGUI;

		foreach(ComboBox cb in inputBoxes)
		cb.Show();

		if (setupDisplayed) {
						if (GUI.Button (new Rect (20, 40, 120, 20), "Start")) {
								//Application.LoadLevel(1);
								updateMazeData ();
								Debug.Log ("Start Game Pressed");
								startPressed = true;
						}
						if (GUI.Button (new Rect (20, 80, 120, 20), "Random Game")) {
								//Application.LoadLevel(1);
								Debug.Log ("Random Game Pressed");
								randomMazeData ();
								startPressed = true;
						}
						GUI.skin.box.wordWrap = true;
						GUI.Box (new Rect (20, 120, 250, 500), instructions);
				}

	}
	private void updateMazeData()
	{
		bool beginFound = false;
		bool endFound = false;
		int counter = 0;
		for (int i=0; i<16; i++) {
			for (int j=0; j<16; j++){
				int selected = inputBoxes[counter].SelectedItemIndex;
				if(selected == 4)
				{
					if(!beginFound)
						beginFound = true;
					else
						beginProblem = true;
				}
				if(selected == 5)
				{
					if(!endFound)
						endFound = true;
					else
						endProblem = true;
				}
				mazeData[i,j] = selected;
				counter++;
			}
		}
		if (!beginFound)
						beginProblem = true;
		if (!endFound)
						endProblem = true;
	}
	private void randomMazeData()
	{
		//set tiles to random between 0 and 3
		for (int i=0; i<16; i++) {
			for(int j=0; j<16; j++)
			{
				int tileType = Random.Range(0,4);
				mazeData[i,j] = tileType;
			}
		}
		//randomly pick begin tile
		int beginX = Random.Range (0, 16);
		int beginY = Random.Range (0, 16);
		mazeData [beginX, beginY] = 4;
		//randomly pick end tile
		int endX = Random.Range (0, 16);
		int endY = Random.Range (0, 16);
		mazeData [endX, endY] = 5;

	}
	public void clearMazeData()
	{
		for (int i=0; i<16; i++) {
			for (int j=0; j<16; j++) {
				mazeData[i,j]=0;
			}
		}
	}
	public void resetButtons(){
			inputBoxes.Clear();

	}
	public int[,] getMazeData()
	{
		return mazeData;
	}
}