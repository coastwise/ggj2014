using UnityEngine;

//public sealed class MainMenuState :  State<GameManager> {
public class MainMenuState :  State<GameManager> {
	//string nextState = "playAccordianState";
	
	
	static readonly MainMenuState instance = new MainMenuState();
	public static MainMenuState Instance {

		get 
		{
			//if(instance == null)
			//{	
				return instance;
			//}
		}
	}
	static MainMenuState() { }
	public MainMenuState() { }
	string selectedGame;
		
	public override void Enter (GameManager g) {
		if (g.Location != Locations.mainMenu) {
			Debug.Log("EnterMainMenu");
			g.ChangeLocation(Locations.mainMenu);
		}
	}
	
	//public override void Execute (GameManager g) 
	public override void Execute(GameManager g)
	{
		Debug.Log("InMainMenu");
		
				//g.ChangeState(PlayAccordianState.Instance);
		//g.ChangeState (nextState);
		SetNextState (g, "setBoardState");
	}
	public override void Exit(GameManager g) {
		Debug.Log("Exiting MainMenu");
	}
	public override void SetNextState(GameManager g, string state)
	{
		g.ChangeState (state);
	}
}

