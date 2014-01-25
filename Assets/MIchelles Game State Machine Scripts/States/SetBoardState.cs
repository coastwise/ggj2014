using UnityEngine;

public sealed class SetBoardState :  State<GameManager> {
	
	bool isPlaying = false;
	//string nextState = "";
	
	static readonly SetBoardState instance = new SetBoardState();
	public static SetBoardState Instance {
		get 
		{
			return instance;
		}
	}
	static SetBoardState() { }
	private SetBoardState() { }
	
		
	public override void Enter (GameManager g) {
		if (g.Location != Locations.setBoardState) {
			Debug.Log("Time to Set Board State");
			g.ChangeLocation(Locations.setBoardState);
			isPlaying = true;
			//Accordian.GetInstance().Deal ();
			setBoard();

		}
	}
	public void setBoard()
	{
		Debug.Log ("setBoard");
		//Accordian.GetInstance().Deal();
		Setup.Instance.setupDisplayed = true;
		//Setup.Instance.startSetup();
		Setup.Instance.clearMazeData ();
		Setup.Instance.setDisplayButtons();
	}
	//Execute is the same as update, its just being called from cardGameStateMachine
	public override void Execute (GameManager g) {
		//Debug.Log("Now Playing accordian");
		//if(!isPlaying)	
		//g.ChangeState(nextState);
		if (Setup.Instance.startPressed)				
			SetNextState (g, "playGameState");
		Setup.Instance.startPressed = false;
	}
	
	public override void Exit(GameManager g) {
		Setup.Instance.resetButtons ();
		Setup.Instance.setupDisplayed = false;
		Debug.Log("Exiting set board");
	}
	/*
	public void setIsPlayingAccordian(bool _play)
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
