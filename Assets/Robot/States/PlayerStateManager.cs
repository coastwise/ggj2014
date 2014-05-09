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
		set { _currState = value; }
	}

	void Awake () {
		_player = GetComponent<PlayerController> ();
	}
}
