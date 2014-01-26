using UnityEngine;
using System.Collections;

public class StandingState : PlayerState {

	public StandingState (PlayerController player) : base (player) {}

	private bool isIdle = true;

	public override void OnEnter ()
	{
		Animator animator = player.GetComponent<Animator>();
	}

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

		player.transform.localScale = new Vector3(1,1,1);

		if (isIdle) player.GetComponent<Animator>().Play("RoboRun");
		isIdle = false;
	}

	override public void Right () {
		float vx = player.rigidbody2D.velocity.x + player.groundAcceleration;
		vx = Mathf.Clamp(vx, -player.maxGroundVelocity, player.maxGroundVelocity);
		player.rigidbody2D.velocity = new Vector2(vx, player.rigidbody2D.velocity.y);

		player.transform.localScale = new Vector3(-1,1,1);
		
		if (isIdle) player.GetComponent<Animator>().Play("RoboRun");
		isIdle = false;
	}

	override public void Idle () {
		if (!isIdle) player.GetComponent<Animator>().Play("RoboStand");
		isIdle = true;
	}
	
}
