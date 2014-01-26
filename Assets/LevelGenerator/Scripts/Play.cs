using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Play : Game 
{

	private static Play instance;
	
	public static Play Instance
	{
		get { return instance ?? (instance = new GameObject("Play").AddComponent<Play>()); }
		
	}

	List<Sprite> _tilesBlue = new List<Sprite>();
	List<Sprite> _tilesGreen = new List<Sprite>();
	List<Sprite> _tilesRed = new List<Sprite>();
	List<Sprite> _tilesYellow = new List<Sprite>();


	static int gridLength = 30;
	static int gridHeight = 16;

	public bool finishPressed = false;
	public bool playDisplayed = false;

	public bool switchTransition = true;

	GameObject[,] tiles;
	int[,] gameMap = new int[gridHeight,gridLength];


	public void clearGrid(){//figure out how to destroy grid tiles

	}
	private void startSwitchTransition(){

	}
	private void switchColours()
	{
		displayGrid();
	}
	public IEnumerator switchColoursTimer() {
		while(true)
		{
			if(switchTransition){
				switchColours();
				switchTransition = false;
				yield return new WaitForSeconds(3);
			}
			else{
				startSwitchTransition();
				switchTransition = true;
				yield return new WaitForSeconds(3);
			}
		}
	}

	public void createTiles () {

		Object[] tilesprites = Resources.LoadAll<Sprite>("Platforms/tiles");

		_tilesBlue.Add (tilesprites[8] as Sprite);
		_tilesBlue.Add (tilesprites[9] as Sprite);
		_tilesBlue.Add (tilesprites[10] as Sprite);
		_tilesBlue.Add (tilesprites[11] as Sprite);

		
		_tilesGreen.Add (tilesprites[4] as Sprite);
		_tilesGreen.Add (tilesprites[5] as Sprite);
		_tilesGreen.Add (tilesprites[6] as Sprite);
		_tilesGreen.Add (tilesprites[7] as Sprite);
		
		_tilesRed.Add (tilesprites[16] as Sprite);
		_tilesRed.Add (tilesprites[17] as Sprite);
		_tilesRed.Add (tilesprites[18] as Sprite);
		_tilesRed.Add (tilesprites[19] as Sprite);

		_tilesYellow.Add (tilesprites[12] as Sprite);
		_tilesYellow.Add (tilesprites[13] as Sprite);
		_tilesYellow.Add (tilesprites[14] as Sprite);
		_tilesYellow.Add (tilesprites[15] as Sprite);

	}


	public void createGrid() {
		tiles = new GameObject[gridHeight,gridLength];
		float counterLength = 0f;
		float counterHeight = 0f;
		float posX = -7.25f;
		float posY = -3.75f;
		for (int i=0; i<gridHeight; i++) {
			for(int j=0; j<gridLength; j++){
				GameObject tileSquare;
				Vector3 pos;
				//int random = Random.Range (0, 4);
				//gameMap[i,j] = random;
				
				tileSquare = new GameObject();
				tileSquare.AddComponent<SpriteRenderer>();
				pos = new Vector3 (posX+counterLength, posY+counterHeight, -500);
				tileSquare.transform.position = pos;
				tiles[i,j] = tileSquare;
				counterLength += 0.5f;
			}
			counterHeight += 0.5f;
			counterLength = 0f;
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

	public void displayGrid(){

		for (int i=0; i<gridHeight; i++) {
			for(int j=0; j<gridLength; j++){
				//int random = Random.Range (0, 4);
				//gameMap[i,j] = random;
		
			
				tiles[i,j].GetComponent<SpriteRenderer>().sprite = getSpriteRenderer (gameMap[i,j], Random.Range (0,4));
				tiles[i,j].name = tiles[i,j].GetComponent<SpriteRenderer>().sprite.name;
			
			}
		}
	}

	public void cycleGrid () {

	}

	private Sprite getSpriteRenderer(int random, int random2){

		Sprite gameTile;
		switch (random) {
		case 0: gameTile = _tilesRed[random2];
			break;
		case 1:
			gameTile = _tilesBlue[random2];
			break;
		case 2:
			gameTile = _tilesYellow[random2];
			break;
		default:
			gameTile = _tilesGreen[random2];
			break;

		}
		return gameTile;
	}

	private void OnGUI(){
		
	}
}
