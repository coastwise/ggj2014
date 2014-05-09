using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PlayerController))]
public class PlayerStateManager : MonoBehaviour {

	private PlayerController _player;

	private PlayerState _prevState;
	public PlayerState PreviousState {
		get { return _prevState; }
	}

	private PlayerState _currState;
	public PlayerState CurrentState {
		get { return _currState; }
	}

	void Awake () {
		_player = GetComponent<PlayerController> ();
		if (_currState == null) {
			// do a linq search and automatically find the enabled playerstate
			_currState = GetComponent<StandingState>();
		}
	}

	public void Transition (PlayerState current, PlayerState next)
	{
		_prevState = current;
		_currState = next;
		current.enabled = false;
		next.enabled = true;
	}
}
