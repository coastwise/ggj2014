using UnityEngine;
using System;
using System.Collections;

public class RunningState : PlayerState {

	[SerializeField]
	private float _groundAccel = 2.6f; // float instead of vector because on ground (only X)
	public float GroundAcceleration {
		get { return _groundAccel;}
	}
	
	[SerializeField]
	private float _maxGroundVel = 8f; // float instead of vector because on ground (only X)
	public float MaximumGroundVelocity {
		get { return _maxGroundVel; }
	}
	
	protected override void Awake ()
	{
		base.Awake ();
		_exitActions = new Func<bool>[]{ Stop, Throw, Jump };
	}

	protected override void Start ()
	{
		
	}
	
	protected override void Update ()
	{
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
		Debug.Log ("Entered Running State");
    }
    
	protected override void OnDisable ()
    {
        
    }
    
    protected override void PerformAction ()
	{
		float vx = rigidbody2D.velocity.x;
		if (Input.GetAxis ("L_XAxis_" + _player.Joystick) < 0) {
			vx -= GroundAcceleration;
		} else if (Input.GetAxis ("L_XAxis_" + _player.Joystick) > 0) {
			vx += GroundAcceleration;
		} else {

		}

		vx = Mathf.Clamp(vx, -MaximumGroundVelocity, MaximumGroundVelocity);
		rigidbody2D.velocity = new Vector2(vx, rigidbody2D.velocity.y);

		if (rigidbody2D.velocity.x > 0) {
			transform.localScale = new Vector3(1,1,1);
		} else if (rigidbody2D.velocity.x < 0) {
			transform.localScale = new Vector3(-1,1,1);
		}


	}

	bool Stop () {

		// if x velocity is 0 and on the ground
		if (rigidbody2D.velocity.x == 0) {
			_exitState = GetComponent<StandingState>();
			return true;
		}

		return false;
	}

	bool Throw () {
		if (Input.GetButtonDown("X_"+_player.Joystick) && _player.FireableBoomerangs > 0)
		{
			//_exitState = GetComponent<ThrowingState>();
			GetComponent<ThrowingState>().enabled = true;
			//return true;
		}
		
		
		//Debug.Log("Player " + _player.Joystick + " StandingState: Throw");
		return false;
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
}
