using UnityEngine;
using System;
using System.Collections;

public class WallSlidingState : PlayerState {

	
	[SerializeField]
	private Vector2 _wallJumpInstantVelocity = Vector2.one * 5f;
	public Vector2 InstantWallJumpVelocity {
		get { return _wallJumpInstantVelocity; }
		set { _wallJumpInstantVelocity = value; }
	}

	private bool _wallDirection; // left false, right true
	public bool WallDirection {
		get { return _wallDirection; }
		set { _wallDirection = value; }
	}

	private bool isOnWall = false;
	
	
	protected override void Awake ()
	{
		base.Awake ();
		_exitActions = new Func<bool>[] { Jump };
	}
	
	protected override void Start ()
	{
		
	}
	
	protected override void Update ()
	{
		foreach (Func<bool> f in _exitActions) {
			if (f()) _manager.Transition(this, _exitState);
		}

		PerformAction ();
	}
	
	protected override void FixedUpdate ()
	{
		
	}
	
	protected override void OnEnable ()
	{
		
    }
    
	protected override void OnDisable ()
    {
        
    }
    
    protected override void PerformAction ()
	{
		
	}

	protected override void OnCollisionEnter2D (Collision2D coll)
	{
		if (enabled == false) return;

		if(coll.gameObject.tag == "floor") {
			
			foreach (ContactPoint2D contact in coll.contacts) {

				if (contact.normal == Vector2.right || contact.normal == -Vector2.right) {

					isOnWall = true;
				}

			}
		}
	}

	protected override void OnCollisionExit2D (Collision2D coll)
	{
		
		if (enabled == false) return;

		if(coll.gameObject.tag == "floor") {
			
			foreach (ContactPoint2D contact in coll.contacts) {
				//Debug.Log(name + " contact normal " + contact.normal);
				if (contact.normal == Vector2.up) {
					_exitState = GetComponent<FallingState>();
					_manager.Transition(this, _exitState);
					return;
				} 
				else if (contact.normal == Vector2.right || contact.normal == -Vector2.right) {
					GetComponent<WallSlidingState>().WallDirection = contact.normal != Vector2.right;
					_exitState = GetComponent<FallingState>();
					_manager.Transition(this, _exitState);
					return;
				}
				else{
					_exitState = GetComponent<FallingState>();
					_manager.Transition(this, _exitState);
					return;
				}
			}
		}
	}

	bool Jump ()
	{
		if (Input.GetButtonDown ("A_" + _player.Joystick)) {
			Vector2 jump = InstantWallJumpVelocity;
			if (WallDirection) {
					jump = new Vector2 (jump.x * -1, jump.y);
			}

			gameObject.rigidbody2D.velocity += jump;
			_player.PlaySound (SoundEffects.WallJump);

			_exitState = GetComponent<FallingState> ();
			return true;
		}

		return false;
	}




}
