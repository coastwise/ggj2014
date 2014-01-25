using UnityEngine;
using System.Collections;

public class StompingState : StandingState {

	public StompingState (PlayerController player) : base (player) {}

	override public void Jump () {
		Debug.Log("Player " + player.joystick + " Stomping Jump");
		player.EnterState(typeof(JumpingState));
	}

	override public void OnEnter () {
		Debug.Log("stomping");
		player.StartCoroutine(WaitThenFall());
	}

	private IEnumerator WaitThenFall () {
		yield return new WaitForSeconds(0.2f);
		player.EnterState(typeof(FallingState));
	}

}
