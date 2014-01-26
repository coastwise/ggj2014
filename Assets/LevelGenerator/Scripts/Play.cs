using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;


public class Play : Game 
{

	private static Play instance;
	
	public static Play Instance
	{
		get { return instance ?? (instance = new GameObject("Play").AddComponent<Play>()); }
		
	}
	

	static int gridLength = 30;
	static int gridHeight = 16;

	public bool finishPressed = false;
	public bool playDisplayed = false;

	public bool switchTransition = true;
	
	List <GameObject> tiles = new List<GameObject>();
	int[,] gameMap = new int[gridHeight,gridLength];


	public void clearGrid(){
		foreach (GameObject tile in tiles) {
						Destroy (tile);
		}
	}
	private void startSwitchTransition(){
		clearGrid ();
		displayGrid (2);
	}
	private void switchColours()
	{
		clearGrid ();
		for (int i=0; i<gridHeight; i++) {
			for(int j=0; j<gridLength; j++){
				gameMap[i,j]++;
				if(gameMap[i,j]==4)
					gameMap[i,j]= 0;
			}
		}
		displayGrid (1);
	}
	public IEnumerator switchColoursTimer() {
		while(true)
		{
			if(switchTransition){
			switchColours();
				switchTransition = false;
				int randomTime = Random.Range(10,21);
			yield return new WaitForSeconds(3);
			}
			else{
				startSwitchTransition();
				switchTransition = true;
				yield return new WaitForSeconds(3);
			}
		}
	}
	public void randomGrid(){
		for (int i=0; i<gridHeight; i++) {
			for (int j=0; j<gridLength; j++){
				int random = Random.Range (0, 4);
				gameMap[i,j] = random;
			}
		}
	}
	public void displayGrid(int toggle){
		float counterLength = 0f;
		float counterHeight = 0f;
		float posX = -7.25f;
		float posY = -3.75f;
		for (int i=0; i<gridHeight; i++) {
			for (int j=0; j<gridLength; j++) {
				GameObject tileSquare;
				Vector3 pos;

				if (toggle == 1) {
					tileSquare = getGameTile (gameMap [i, j]);
				}
				if (gameMap [i, j] == 0) {
					tileSquare = getGameTile (4);
				}
				else if (gameMap [i, j] == 1) {
					tileSquare = getGameTile (5);
				}
				else if (gameMap [i, j] == 2) {
					tileSquare = getGameTile (6);
				}
				else {
					tileSquare = getGameTile (7);
				}
			
				pos = new Vector3 (posX + counterLength, posY + counterHeight, -500);
				tileSquare.transform.position = pos;
				tiles.Add (tileSquare);
				counterLength += 0.5f;
				}
				counterHeight += 0.5f;
				counterLength = 0f;
			}
	}
		
	private GameObject getGameTile(int tileNum){

		GameObject gameTile;
		switch (tileNum) {

		case 0:
			gameTile = (GameObject)Instantiate (Resources.Load ("Prefab/RedTile"));
			break;
		case 1:
			gameTile = (GameObject)Instantiate (Resources.Load ("Prefab/BlueTile"));
			break;
		case 2:
			gameTile = (GameObject)Instantiate (Resources.Load ("Prefab/YellowTile"));
			break;
		case 3:
			gameTile = (GameObject)Instantiate (Resources.Load ("Prefab/GreenTile"));
			break;
		case 4:
			gameTile = (GameObject)Instantiate (Resources.Load ("Prefab/RedToBlue"));
			gameTile.GetComponent<Animator>().Play("R2B");

			break;
		case 5:
			gameTile = (GameObject)Instantiate (Resources.Load ("Prefab/BlueToGreen"));
			gameTile.GetComponent<Animator>().Play ("B2G");
			break;
		case 6:
			gameTile = (GameObject)Instantiate (Resources.Load ("Prefab/RedToBlue"));
			break;
		case 7:
			gameTile = (GameObject)Instantiate (Resources.Load ("Prefab/RedToBlue"));
			break;
		default:
			gameTile = (GameObject)Instantiate (Resources.Load ("Prefab/GreenTile"));
			break;

		}
		return gameTile;
	}
	public void getLevel(){
		var sr = new StreamReader(Application.dataPath + "/" + "Level1.txt");
		string fileContents = sr.ReadToEnd();
		sr.Close();
		int counter = 0;
		for (int i=0; i<16; i++) {
			for (int j=0; j<30; j++){
			gameMap[i,j] = int.Parse(fileContents[counter].ToString ());
			counter++;
			}
		}

	}
	private void OnGUI(){

	}
}
