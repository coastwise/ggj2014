using UnityEngine;
using System.Collections;

public class StandingState : PlayerState {

	public StandingState (PlayerController player) : base (player) {}

	virtual public void Jump () {
		Debug.Log(player.joystick + " Standing Jump");
	}

	virtual public void Throw () {
		Debug.Log(player.joystick + " Standing Throw");
	}
	
}
