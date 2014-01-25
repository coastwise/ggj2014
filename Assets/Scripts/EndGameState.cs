using UnityEngine;

public sealed class EndGameState :  State<GameManager> {

	
	static readonly EndGameState instance = new EndGameState();
	public static EndGameState Instance {
		get 
		{
			return instance;
		}
	}
	static EndGameState() { }
	private EndGameState() { }
	
	public override void Enter (GameManager g) {
		if (g.Location != Locations.endGameState) {
			g.ChangeLocation(Locations.endGameState);
			//EndGame.Instance.playDisplayed = true;
		}
	}
	public override void Execute (GameManager g) {
		//if (EndGame.Instance.finishPressed)				
			SetNextState (g, "mainMenu");
		//EndGame.Instance.finishPressed = false;
	}
	
	public override void Exit(GameManager g) {
		Play.Instance.playDisplayed = false;
		Debug.Log("Exiting Endgame");
	}

	public override void SetNextState(GameManager g, string state)//This needs to be removed in all states once I get the GUI working
	{
		g.ChangeState (state);
	}
}


