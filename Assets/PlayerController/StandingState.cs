using UnityEngine;
using System.Collections;

public class StandingState : PlayerState {

	public StandingState (PlayerController player) : base (player) {}

	override public void Jump () {
		Debug.Log("Player " + player.joystick + " Standing Jump");
		player.EnterState(typeof(JumpingState));
	}

	override public void Throw () {
		Debug.Log("Player " + player.joystick + " Standing Throw");
	}

	override public void Left () {
		player.rigidbody2D.velocity -= Vector2.right * 1.2f;
	}

	override public void Right () {
		player.rigidbody2D.velocity += Vector2.right * 1.2f;
	}
	
}
