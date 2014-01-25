using UnityEngine;
using System.Collections;

public class StandingState : PlayerState {

	public StandingState (PlayerController player) : base (player) {}

	override public void Jump () {
		Debug.Log("Player " + player.joystick + " Standing Jump");
	}

	override public void Throw () {
		Debug.Log("Player " + player.joystick + " Standing Throw");
	}
	
}
