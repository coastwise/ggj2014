using UnityEngine;
using System.Collections;

public class FallingState : JumpingState {
	
	public FallingState (PlayerController player) : base (player) {}
	
	public override void OnEnter () {
		// don't jump
	}
	
}
