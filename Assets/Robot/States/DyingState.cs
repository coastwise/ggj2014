using UnityEngine;
using System.Collections;

public class DyingState : PlayerState {
	
	public DyingState (PlayerController player) : base (player) {}

	float countdown;

	public override void OnEnter () {
		player.GetComponent<Animator>().SetTrigger("Explode");
		countdown = player.respawnTimeout;

		player.collider2D.enabled = false;
		player.rigidbody2D.isKinematic = true;

		player.gameObject.audio.PlayOneShot(player.explosionSound);

		player._fireableBoomerangs = 0;
	}

	override public void Update () {
		countdown -= Time.deltaTime;
		if (countdown < 0) {
			Debug.Log("Respawn");
			player.collider2D.enabled = true;
			player.rigidbody2D.isKinematic = false;
			player._fireableBoomerangs = 3;
			player.EnterState(typeof(StandingState));
			player.GetComponent<Animator>().SetTrigger("Respawn");
		}
	}

}
