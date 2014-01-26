using UnityEngine;
using System.Collections;

public class JumpingState : PlayerState {
	
	public JumpingState (PlayerController player) : base (player) {}

	public override void OnEnter () {
		player.gameObject.rigidbody2D.velocity += Vector2.up * player.instantaneousJumpVelocity;
		player.GetComponent<Animator>().Play("RoboStand");
	}
	
	override public void Jump () {
		Debug.Log("Player " + player.joystick + " Jumping Jump");
	}
	
	override public void Throw () {
		Debug.Log("Player " + player.joystick + " Jumping Throw");
	}

	override public void Left () {
		float vx = player.rigidbody2D.velocity.x - player.horizontalAirAcceleration;
		vx = Mathf.Clamp(vx, -player.maxAirHorizontalVelocity, player.maxAirHorizontalVelocity);
		player.rigidbody2D.velocity = new Vector2(vx, player.rigidbody2D.velocity.y);
	}
	
	override public void Right () {
		float vx = player.rigidbody2D.velocity.x + player.horizontalAirAcceleration;
		vx = Mathf.Clamp(vx, -player.maxAirHorizontalVelocity, player.maxAirHorizontalVelocity);
		player.rigidbody2D.velocity = new Vector2(vx, player.rigidbody2D.velocity.y);
	}

	override public void HitFloor(){
		Debug.Log("Player " + player.joystick + " Landed");
		player.EnterState(typeof(StandingState));
	}

	override public void HitWall () {
		player.EnterState(typeof(WallSlidingState));
	}

	public override void HitPlayer (Collision2D coll) {
		// we don't care if we're on our way up
		if (player.rigidbody2D.velocity.y > 0) return;

		float deltaY = player.transform.position.y - coll.transform.position.y;
		Debug.Log("delta y " + deltaY);
		if (deltaY > 0.5f) {
			//coll.gameObject.GetComponent<PlayerController>();
			Debug.Log(player.name + " stomped " + coll.gameObject.name);
			player.EnterState(typeof(StompingState));
		}
	}
	
}
