using UnityEngine;
using System.Collections;

public class JumpingState : PlayerState {
	
	public JumpingState (PlayerController player) : base (player) {}

	public override void OnEnter () { 

		player.gameObject.rigidbody2D.velocity += Vector2.up * player.instantaneousJumpVelocity;
		player.GetComponent<Animator>().SetTrigger("Jump");

		player.PlaySound (SoundEffects.Jump);

		if(!player.MultiJump.enabled){
			player.GetComponent<Animator>().SetBool("Land", false);
		}
	}
	
	override public void Jump () {
		//Debug.Log("Player " + player.joystick + " Jumping Jump");



		if (player.MultiJump.enabled)
		{
			player.GetComponent<MultiJump>().SpawnDoublejumpExplosion();
			player.EnterState(typeof(JumpingState));
		}
	}
	
	override public void Throw () {
		Debug.Log("Player " + player.Joystick + " Jumping Throw");
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
		Debug.Log("Player " + player.Joystick + " Landed");
		player.EnterState(typeof(StandingState));

		player.GetComponent<Animator>().SetBool("Land", true);
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

			PlayerController other = coll.gameObject.GetComponent<PlayerController>();
			if (!other.isInvincible) other.EnterState(typeof(DyingState));

			player.EnterState(typeof(StompingState));
			player.killcount++;
			GameObject.Find ("killcount" + player.Joystick).GetComponent<GUIText>().text = "x" + player.killcount;
			GameObject.Find ("Win").GetComponent<WinCondition>().CheckWinner();

		}
	}

	public override void Update () {
		float vy = player.rigidbody2D.velocity.y;
		vy = Mathf.Clamp (vy, -player.maxAirVerticalVelocity, player.maxAirVerticalVelocity);
		player.rigidbody2D.velocity = new Vector2(player.rigidbody2D.velocity.x, vy);

		player.GetComponent<Animator>().SetFloat("y-velocity", player.rigidbody2D.velocity.y);
	}
	
}
