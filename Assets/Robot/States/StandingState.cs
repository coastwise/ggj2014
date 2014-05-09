using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class StandingState : PlayerState {


	protected override void Awake ()
	{
		base.Awake ();
		_exitActions = new Func<bool>[]{ Jump, Left, Right, Throw };
		_exitState = null;
	}

	protected override void Start ()
	{

	}

	protected override void Update ()
	{
		// loop through actions that will trigger an exit out of this state
		foreach (Func<bool> f in _exitActions) {
			if (f()) _manager.Transition (this, _exitState);
		}

		PerformAction ();
	}

	protected override void FixedUpdate ()
	{
	}
	
	protected override void OnEnable ()
	{
		_multiJump.enabled = true;
		SetAnimation ();
	}

	protected override void OnDisable ()
	{
		_exitState = null;
	}

	protected override void PerformAction ()
	{

	}

	void SetAnimation ()
	{
		// trigger animation
		// Will check a StateManager object for last state and use proper transition animation
	}


	bool Jump () {
		if (Input.GetButtonDown ("A_" + _player.Joystick)) {
			RaycastHit2D hitL = Physics2D.Raycast((Vector2)(transform.position) + Vector2.up * 0.52f + Vector2.right * -0.2f,Vector2.up,0.06f, gameObject.layer-4);
			RaycastHit2D hitR = Physics2D.Raycast((Vector2)(transform.position) + Vector2.up * 0.52f + Vector2.right * 0.2f,Vector2.up,0.06f, gameObject.layer-4);
			if (hitL.collider == null && hitR.collider == null) {
				_exitState = GetComponent<JumpingState>();
				return true;
			} else {
				Debug.Log ("Tried to jump but obstacle above you, no point exiting state.");
			}
		}
		return false;
	}

	bool Throw () {
		//Debug.Log("Player " + _player.Joystick + " StandingState: Throw");
		return false;
	}

	bool Left () {
		
		//Debug.Log("Player " + _player.Joystick + " StandingState: Left");
		/*
		float vx = rigidbody2D.velocity.x - _player.GroundAcceleration;
		vx = Mathf.Clamp(vx, -_player.MaximumGroundVelocity, _player.MaximumGroundVelocity);
		rigidbody2D.velocity = new Vector2(vx, rigidbody2D.velocity.y);

		transform.localScale = new Vector3(-1,1,1);
		*/
		return false;
	}

	bool Right () {
		
		//Debug.Log("Player " + _player.Joystick + " StandingState: Right");
		/*
		float vx = rigidbody2D.velocity.x + _player.GroundAcceleration;
		vx = Mathf.Clamp(vx, -_player.MaximumGroundVelocity, _player.MaximumGroundVelocity);
		rigidbody2D.velocity = new Vector2(vx, rigidbody2D.velocity.y);

		transform.localScale = new Vector3(1,1,1);
		*/
		return false;
	}

	void Idle () {
		//Debug.Log("Player " + _player.Joystick + " StandingState: Idle");
	}
	
}
