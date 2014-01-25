using UnityEngine;
using System.Collections;

public class StandingState : PlayerState {

	virtual public void Jump () {
		Debug.Log(player.joystick + " Standing Jump");
	}

	virtual public void Throw () {
		Debug.Log(player.joystick + " Standing Throw");
	}
	
}
