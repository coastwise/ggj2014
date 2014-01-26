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
	
	public override void Enter (GameManager g) {
		if (g.Location != Locations.playGameState) {
			g.ChangeLocation(Locations.playGameState);
			Play.Instance.playDisplayed = true;
			Debug.Log ("Entering PlayGame");
			Play.Instance.getLevel();
			Play.Instance.displayGrid(1);
			Play.Instance.StartCoroutine ("switchColoursTimer");
		}
	}
	
	//Execute is the same as update, its just being called from cardGameStateMachine
	public override void Execute (GameManager g) {
		if (Play.Instance.finishPressed)				
			SetNextState (g, "mainMenu");
		Play.Instance.finishPressed = false;

	}
	
	public override void Exit(GameManager g) {
		Play.Instance.playDisplayed = false;
		Play.Instance.clearGrid ();
		Debug.Log("Exiting PLAY");
	}
	public override void SetNextState(GameManager g, string state)//This needs to be removed in all states once I get the GUI working
	{
		g.ChangeState (state);
	}
}

