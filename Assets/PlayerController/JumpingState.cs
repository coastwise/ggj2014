using UnityEngine;
using System.Collections;

public class JumpingState : PlayerState {
	
	public JumpingState (PlayerController player) : base (player) {}

	public override void OnEnter () {
		player.gameObject.rigidbody2D.velocity += Vector2.up * 10;
	}
	
	override public void Jump () {
		Debug.Log("Player " + player.joystick + " Jumping Jump");
	}
	
	override public void Throw () {
		Debug.Log("Player " + player.joystick + " Jumping Throw");
	}
	
}
