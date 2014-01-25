using UnityEngine;
using System.Collections;

public class WallSlidingState : FallingState {

	public WallSlidingState (PlayerController c) : base (c) {}

	override public void Jump () {
		Debug.Log("WallJump!");
	}

}
