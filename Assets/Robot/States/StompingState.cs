using UnityEngine;
using System.Collections;

public class StompingState : StandingState {

	public StompingState (PlayerController player) : base (player) {}

	float countdown;

	override public void Jump () {
		Debug.Log("Player " + player.Joystick + " Stomping Jump");
		player.EnterState(typeof(JumpingState));
	}

	override public void OnEnter () {
		countdown = player.StompJumpTimeout; // seconds
	}

	override public void HitFloor(){
		player.EnterState(typeof(StandingState));
	}

	override public void Update () {
		countdown -= Time.deltaTime;
		if (countdown < 0) {
			player.EnterState(typeof(FallingState));
		}
	}

}
