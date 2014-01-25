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
		float vx = player.rigidbody2D.velocity.x - player.groundAcceleration;
		vx = Mathf.Clamp(vx, -player.maxGroundVelocity, player.maxGroundVelocity);
		player.rigidbody2D.velocity = new Vector2(vx, player.rigidbody2D.velocity.y);
	}

	override public void Right () {
		float vx = player.rigidbody2D.velocity.x + player.groundAcceleration;
		vx = Mathf.Clamp(vx, -player.maxGroundVelocity, player.maxGroundVelocity);
		player.rigidbody2D.velocity = new Vector2(vx, player.rigidbody2D.velocity.y);
	}
	
}
