using UnityEngine;

public sealed class PlayGameState :  State<GameManager> {
	
	bool isPlaying = false;
	//string nextState = "";
	
	static readonly PlayGameState instance = new PlayGameState();
	public static PlayGameState Instance {
		get 
		{
			return instance;
		}
	}
	static PlayGameState() { }
	private PlayGameState() { }

	//static int gridLength = 16;
	//static int gridHeight = 16;
	//Tile[,] grid = new Tile[gridLength,gridHeight];
	
	public override void Enter (GameManager g) {
		if (g.Location != Locations.playGameState) {
			g.ChangeLocation(Locations.playGameState);
			Play.Instance.playDisplayed = true;
			//Play.Instance.startSetup();//probably get rid of this and GAME all together
			Play.Instance.createTiles();
			Play.Instance.displayGrid();
		}
	}
	/*private void createTiles(){

		int[,] mazeData = Setup.Instance.getMazeData ();
		for(int i=0; i<gridLength; i++)
		{
			for(int j=0; j<gridHeight; j++)
			{
				Tile t = new Tile(i,j,mazeData[i,j]);
				grid[i,j] = t;
			}
		}
	}
	public void setBoard()
	{
				//Debug.Log ("setBoard");
				//Accordian.GetInstance().Deal();
				//Game.Instance.startSetup();
				float spacer1 = 0f;
				float spacer2 = 0f;
				for (int i=0; i<gridLength; i++) {
						for (int j=0; j<gridHeight; j++) {
								GameObject grassland = (GameObject)Instantiate (Resources.Load ("Grassland"));
								Vector3 pos = new Vector3 ((grid [i, j].point.x - spacer2) - 2, (grid [i, j].point.y - spacer1) - 3, 0);
								grassland.transform.position = pos;
								spacer1 += 0.45f;
						}
			
						spacer2 += 0.45f;
						spacer1 = 0.0f;
				}
		}*/
	//Execute is the same as update, its just being called from cardGameStateMachine
	public override void Execute (GameManager g) {
		if (Play.Instance.finishPressed)				
			SetNextState (g, "mainMenu");
		Play.Instance.finishPressed = false;
	}
	
	public override void Exit(GameManager g) {
		Play.Instance.playDisplayed = false;
		Play.Instance.clearGrid ();
		Debug.Log("Exiting Setup");
	}
	/*public void setIsPlayingAccordian(bool _play)
	{
		isPlaying = false;
		if(_play)
			isPlaying = true;
	}*/
	public override void SetNextState(GameManager g, string state)//This needs to be removed in all states once I get the GUI working
	{
		g.ChangeState (state);
	}
}

