using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour {

	public int joystick;

	private Dictionary<System.Type, PlayerState> states;
	private PlayerState currentState;

	// "is false when preceded by its quotation" is false when preceded by its quotation

	void Start () {
		// initialize states once (no gc)
		states = new Dictionary<System.Type, PlayerState>();
		states.Add(typeof(StandingState), new StandingState(this));
		states.Add(typeof(JumpingState), new JumpingState(this));
		states.Add(typeof(StompingState), new StompingState(this));
		states.Add(typeof(FallingState), new FallingState(this));

		// enter the initial state
		currentState = states[typeof(StandingState)];
		currentState.OnEnter();
	}

	public void EnterState (System.Type newState) {
		currentState.OnExit();
		currentState = states[newState];
		currentState.OnEnter();
	}

	void OnCollisionEnter2D(Collision2D coll){
		if(coll.gameObject.tag == "floor"){
			currentState.HitFloor();
		}

		if (coll.gameObject.tag == "Player") {
			currentState.HitPlayer(coll);
		}
	}

	void Update () {
		// check my input and call state methods
		if (Input.GetButtonDown("A_"+joystick)) {
			currentState.Jump();
		}

		if (Input.GetButtonDown("X_"+joystick)) {
			currentState.Throw();
		}

		if (Input.GetAxis("L_XAxis_"+joystick) > 0) {
			currentState.Right();
		} else if (Input.GetAxis("L_XAxis_"+joystick) < 0) {
			currentState.Left();
		}

		currentState.Update();
	}
}
