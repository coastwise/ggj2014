using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour {

	public int joystick;

	private Dictionary<System.Type, PlayerState> states;
	private PlayerState currentState;

	void Start () {
		// initialize states once (no gc)
		states = new Dictionary<System.Type, PlayerState>();
		states.Add(typeof(StandingState), new StandingState(this));

		// enter the initial state
		currentState = states[typeof(StandingState)];
		currentState.OnEnter();
	}

	public void EnterState (System.Type newState) {
		currentState.OnExit();
		currentState = states[newState];
		currentState.OnEnter();
	}
	
	void Update () {
		// check my input and call state methods
		if (Input.GetButtonDown("A_"+joystick)) {
			currentState.Jump();
		}

		if (Input.GetButtonDown("X_"+joystick)) {
			currentState.Throw();
		}
	}
}
