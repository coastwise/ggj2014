using UnityEngine;
using System.Collections;

public class WallSlidingState : FallingState {

	public bool right;

	public WallSlidingState (PlayerController c) : base (c) {}

	override public void Jump () {
		Vector2 jump = player.wallJumpInstantaneousVelocityDir;
		if (player.wallRight) {
			jump = new Vector2(jump.x * -1, jump.y);
		}

		player.gameObject.rigidbody2D.velocity += jump;

		player.EnterState(typeof(FallingState));
		player.GetComponent<Animator>().SetTrigger("Jump");

		player.gameObject.audio.PlayOneShot(player.wallJumpSound);
	}

}
