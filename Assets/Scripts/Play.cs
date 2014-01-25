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
	

	static int gridLength = 30;
	static int gridHeight = 16;

	private bool firstSwitch = false;
	public bool finishPressed = false;
	public bool playDisplayed = false;

	public List<GameObject> tiles = new List<GameObject> ();
	//Tile[,] grid = new Tile[gridLength,gridHeight];
	//GameObject[,] tiles = new GameObject[gridHeight,gridLength];

	int[,] currentGrid = new int[gridHeight,gridLength];
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

	private void switchColours()
	{
		Debug.Log ("test test");
		/*for (int i=0; i<gridHeight; i++) {
			for (int j=0; j<gridLength; j++){
				int temp = currentGrid[i,j];
				temp++;
				if(temp == 4)
					temp = 0;
				currentGrid[i,j] = temp;
			}
		}*/

		Animator an;
		int temp;
		if(!firstSwitch){
	
		foreach (GameObject t in tiles) {
						an = t.GetComponent<Animator> ();

						temp = an.GetInteger ("Colour");
				Vector3 pos = t.transform.position;
				Debug.Log (pos);
			Debug.Log ("temp: " + temp);
			if(temp == 98)
			temp =3;
			else if(temp == 3)
					temp = 2;
			else if(temp == 99)
				temp =0;
			else
				temp = 1;
			
				Debug.Log ("temp2: " + temp);
				//an.SetInteger("Colour", temp);
			}

			firstSwitch = true;
						
		}

				
		//displayGrid ();
	}
	public IEnumerator switchColoursTimer() {
		while(true)//make this end when game over!!!!
		{
			switchColours();
			yield return new WaitForSeconds(3);
		}
	}

	public void createGrid(){
				float counterLength = 0f;
				float counterHeight = 0f;
				float posX = -7.25f;
				float posY = -3.75f;
				for (int i=0; i<gridHeight; i++) {
						for (int j=0; j<gridLength; j++) {
								GameObject tileSquare;
								Vector3 pos;
								tileSquare = getGameTile (currentGrid [i, j]);
								pos = new Vector3 (posX + counterLength, posY + counterHeight, -500);
								tileSquare.transform.position = pos;
								tiles.Add(tileSquare);
								counterLength += 0.5f;
						}
						counterHeight += 0.5f;
						counterLength = 0f;
				}
	}
	public void createRandomLayout(){
				for (int i=0; i<gridHeight; i++) {
					for(int j=0; j<gridLength; j++){
						int random = Random.Range (0, 4);
						currentGrid[i,j] = random;
					}
				}


		/*
		}*/
		


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
	private GameObject getGameTile(int t){
		GameObject gameTile = (GameObject)Instantiate (Resources.Load ("Tile"));
		//GameObject gameTile;
		//Debug.Log ("get tile #" + t);

		Animator an;
		an = gameTile.GetComponent<Animator>();
		switch (t) {
		case 0:
			an.SetInteger("Colour",99);
			break;
		case 1:
			an.SetInteger("Colour",98);
			break;
		case 2:
			an.SetInteger("Colour",3);
			break;
		default:
			//an.SetInteger("Colour",3);
			break;

		}

		//GameObject gameTile = (GameObject)Instantiate (Resources.Load ("Tile"));
		//SpriteRenderer renderer = gameObject.GetComponent<SpriteRenderer>();
		//Sprite sprite = new Sprite();
		//renderer.sprite = sprite;
		//Sprite sprite = new Sprite();
		//Sprite bTexture = Resources.Load<Sprite> ("blueColour");
		//sprite.texture =
		/*switch (t) {
				case 0:
						//gameTile.renderer.material = redTexture;
					gameTile.renderer = Resources.Load("redColour", typeof(Texture2D)) as Texture2D;
			break;
				case 1:
			gameTile.renderer.material.mainTexture = Resources.Load("blueColour", typeof(Texture2D)) as Texture2D;
			break;
				case 2:
			gameTile.renderer.material.mainTexture = Resources.Load("yellowColour", typeof(Texture2D)) as Texture2D;
			break;
				default:
			gameTile.renderer.material.mainTexture = Resources.Load("greenColour", typeof(Texture2D)) as Texture2D;
			break;
						
				}*/
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
