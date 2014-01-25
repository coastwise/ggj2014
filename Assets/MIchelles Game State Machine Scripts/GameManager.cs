
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum Locations { mainMenu, setBoardState, playGameState };

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
		states.Add ("setBoardState", SetBoardState.Instance);
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
	/*private CardGameStateMachine<GameManager> StateMachine;
	
	public Locations Location = Locations.mainMenu;
	public void Awake()
	{
		Debug.Log ("Game manager waking");
		StateMachine = new CardGameStateMachine<GameManager>();
		StateMachine.Configure(this, MainMenuState.Instance);
	}
	public void ChangeState(State<GameManager> e) 
	{
		StateMachine.ChangeState(e);		
	}
	
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		StateMachine.Update();
	}
	public void ChangeLocation(Locations l) 
	{
		Location = l;
	}*/
}