using UnityEngine;
using System.Collections;

public class FallingState : JumpingState {
	
	public FallingState (PlayerController player) : base (player) {}
	
	public override void OnEnter () {
		player.GetComponent<Animator>().SetTrigger("Fall");
	}
	
}
