﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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

	//public List<Tile> grid = new List<Tile> ();
	//Tile[,] grid = new Tile[gridLength,gridHeight];
	List <GameObject> tiles = new List<GameObject>();
	int[,] gameMap = new int[gridHeight,gridLength];
	//public override void startSetup()
	//{
	//	Debug.Log("playSetup");
		//createTiles ();
		//dealTiles ();


		/*
		 * //Place Prefab
		GameObject grassland = (GameObject)Instantiate(Resources.Load("Grassland"));
		Vector3 testPosition = new Vector3 (0, 0, 0);
		grassland.transform.position = testPosition;
		*/

	//}
	/*public void createTiles(){

		int[,] mazeData = Setup.Instance.getMazeData ();
		for(int i=0; i<gridLength; i++)
		{
			for(int j=0; j<gridHeight; j++)
			{
				Tile t = new Tile(i,j,mazeData[i,j]);
				grid[i,j] = t;
			}
		}
	}*/
	public void clearGrid(){//figure out how to destroy grid tiles

				foreach (GameObject tile in tiles) {
						Destroy (tile);
				}
		}
	private void startSwitchTransition(){
		Debug.Log ("transition");
		clearGrid ();

	}
	private void switchColours()
	{
		//clearGrid ();
		Debug.Log ("test");
		for (int i=0; i<gridHeight; i++) {
			for(int j=0; j<gridLength; j++){
				gameMap[i,j]++;
				if(gameMap[i,j]==4)
					gameMap[i,j]= 0;
			}
		}
		displayGrid ();
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
	public void randomGrid(){
		for (int i=0; i<gridHeight; i++) {
			for (int j=0; j<gridLength; j++){
				int random = Random.Range (0, 4);
				gameMap[i,j] = random;
			}
		}
	}
	public void displayGrid(){

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

			tileSquare = getGameTile (gameMap[i,j]);
			pos = new Vector3 (posX+counterLength, posY+counterHeight, -500);
			tileSquare.transform.position = pos;
				tiles.Add ( tileSquare);
			counterLength += 0.5f;
			}
			counterHeight += 0.5f;
			counterLength = 0f;
		}
		


		/*float spacer1 = 0f;
		float spacer2 = 0f;
		for (int i=0; i<gridLength; i++) {
			for (int j=0; j<gridHeight; j++) {
				GameObject tile;
				Vector3 pos;
				Debug.Log ("cost: " + grid[i,j].cost);
				switch(grid[i,j].cost){

				case 0:
					tile = (GameObject)Instantiate (Resources.Load ("Prefabs/OpenSpace"));
					//pos = new Vector3 ((grid [i, j].point.x - spacer2) - 2, (grid [i, j].point.y - spacer1) - 3, 0);
					//tile.transform.position = pos;
					//tiles.Add(tile);
					break;
					case 1:
					tile = (GameObject)Instantiate (Resources.Load ("Prefabs/Swamp"));

					break;
				case 2:
					tile = (GameObject)Instantiate (Resources.Load ("Prefabs/Grassland"));
				
					break;
					case 3:
					tile = (GameObject)Instantiate (Resources.Load ("Prefabs/Obstacle"));
			
					break;	
				case 4:
					tile = (GameObject)Instantiate (Resources.Load ("Prefabs/Start"));
				
					break;

				default:
					tile = (GameObject)Instantiate (Resources.Load ("Prefabs/End"));
				
					break;
					}

				pos = new Vector3 ((grid [i, j].point.x - spacer2) - 2, (grid [i, j].point.y - spacer1) - 3, -5);
				tile.transform.position = pos;
				tiles.Add(tile);
								spacer1 += 0.45f;
				}
			
						spacer2 += 0.45f;
						spacer1 = 0.0f;
			}*/
		}
	private GameObject getGameTile(int random){

		GameObject gameTile;
		switch (random) {
		case 0:
			gameTile = (GameObject)Instantiate (Resources.Load ("Prefab/RedTile"));
			break;
		case 1:
			gameTile = (GameObject)Instantiate (Resources.Load ("Prefab/BlueTile"));
			break;
		case 2:
			gameTile = (GameObject)Instantiate (Resources.Load ("Prefab/YellowTile"));
			break;
		default:
			gameTile = (GameObject)Instantiate (Resources.Load ("Prefab/GreenTile"));
			break;

		}
		return gameTile;
	}
	private void OnGUI(){

				

		/*if(playDisplayed){
				if(GUI.Button(new Rect(20,40,120,20), "Finish")) {
				//Application.LoadLevel(1);

					Debug.Log("Done Pressed");
					finishPressed = true;
				}
			}
		}
		public void clearGrid(){//figure out how to destroy grid tiles
		System.Array.Clear (grid, 0, grid.Length);
		foreach (GameObject tile in tiles) {
			Destroy (tile);
		}*/
	}
}
