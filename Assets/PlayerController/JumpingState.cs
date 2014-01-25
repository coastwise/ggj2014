﻿using UnityEngine;
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

	override public void Left () {
		float vx = Mathf.Clamp(player.rigidbody2D.velocity.x - 0.3f, -16, 16);
		player.rigidbody2D.velocity = new Vector2(vx, player.rigidbody2D.velocity.y);
	}
	
	override public void Right () {
		float vx = Mathf.Clamp(player.rigidbody2D.velocity.x + 0.3f, -16, 16);
		player.rigidbody2D.velocity = new Vector2(vx, player.rigidbody2D.velocity.y);
	}

	override public void HitFloor(){
		Debug.Log("Player " + player.joystick + " Landed");
		player.EnterState(typeof(StandingState));
	}

	public override void HitPlayer (Collision2D coll) {
		// we don't care if we're on our way up
		if (player.rigidbody2D.velocity.y > 0) return;

		float deltaY = player.transform.position.y - coll.transform.position.y;
		Debug.Log("delta y " + deltaY);
		if (deltaY > 0.5f) {
			//coll.gameObject.GetComponent<PlayerController>();
			Debug.Log(player.name + " stomped " + coll.gameObject.name);
		}
	}
	
}
