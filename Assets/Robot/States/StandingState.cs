using UnityEngine;
using System.Collections;

public class StandingState : PlayerState {

	public StandingState (PlayerController player) : base (player) {}

	private bool isIdle = true;

	public override void OnEnter ()
	{
		player.MultiJump.enabled = true;
		Animator animator = player.GetComponent<Animator>();
	}

	override public void Jump () {

		RaycastHit2D hitL = Physics2D.Raycast((Vector2)(player.transform.position) + Vector2.up * 0.52f + Vector2.right * -0.2f,Vector2.up,0.06f, player.gameObject.layer-4);
		//Debug.DrawRay((Vector2)(player.transform.position) + Vector2.up * 0.52f + Vector2.right * -0.2f,Vector2.up);
		RaycastHit2D hitR = Physics2D.Raycast((Vector2)(player.transform.position) + Vector2.up * 0.52f + Vector2.right * 0.2f,Vector2.up,0.06f, player.gameObject.layer-4);
		//Debug.DrawRay((Vector2)(player.transform.position) + Vector2.up * 0.52f + Vector2.right * 0.2f,Vector2.up);


		if (hitL.collider != null) {
			Debug.Log("Collider name: " + hitL.collider.name);
			player.EnterState(typeof(StandingState));
		}
		else if (hitR.collider != null) {
			Debug.Log("Collider name: " + hitR.collider.name);
			player.EnterState(typeof(StandingState));
		} else {
			Debug.Log("Player " + player.Joystick + " Standing Jump");
			player.EnterState(typeof(JumpingState));
		}
	}

	override public void Throw () {
		Debug.Log("Player " + player.Joystick + " Standing Throw");
	}

	override public void Left () {
		float vx = player.rigidbody2D.velocity.x - player.GroundAcceleration;
		vx = Mathf.Clamp(vx, -player.MaximumGroundVelocity, player.MaximumGroundVelocity);
		player.rigidbody2D.velocity = new Vector2(vx, player.rigidbody2D.velocity.y);

		player.transform.localScale = new Vector3(-1,1,1);

		if (isIdle) player.GetComponent<Animator>().SetTrigger("WalkToRun");
		isIdle = false;
	}

	override public void Right () {
		float vx = player.rigidbody2D.velocity.x + player.GroundAcceleration;
		vx = Mathf.Clamp(vx, -player.MaximumGroundVelocity, player.MaximumGroundVelocity);
		player.rigidbody2D.velocity = new Vector2(vx, player.rigidbody2D.velocity.y);

		player.transform.localScale = new Vector3(1,1,1);
		
		if (isIdle) player.GetComponent<Animator>().SetTrigger("WalkToRun");
		isIdle = false;
	}

	override public void Idle () {
		if (!isIdle) player.GetComponent<Animator>().SetTrigger("RunToWalk");
		isIdle = true;
	}
	
}
