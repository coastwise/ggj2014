
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum Locations { mainMenu, endGameState, playGameState };

public class GameManager : MonoBehaviour {
	private GameStateMachine<GameManager> StateMachine;
	private Dictionary<string, State<GameManager>> states;
	public Locations Location = Locations.mainMenu;
	
	void Awake()
	{
		
		Debug.Log ("Game manager waking");
		StateMachine = new GameStateMachine<GameManager>();
		StateMachine.Configure(this, MainMenuState.Instance);
		
		states = new Dictionary<string, State<GameManager>>();
		states.Add ("mainMenu", new MainMenuState());
		states.Add ("endGameState", EndGameState.Instance);
		states.Add ("playGameState", PlayGameState.Instance);
		
		//Accordian.GetInstance();
		
		
	}
	
	public void ChangeState(string state)
	{
		StateMachine.ChangeState(states[state]);
	}
	void Update () 
	{
		StateMachine.Update();
	}
	public void ChangeLocation(Locations l) 
	{
		Location = l;
	}

}