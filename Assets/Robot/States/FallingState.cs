using UnityEngine;
using System;
using System.Collections;

public class FallingState : PlayerState {
	/*
	
	public override void OnEnter () {
		player.GetComponent<Animator>().SetTrigger("Fall");
	}
	*/
	[SerializeField]
	private Vector2 _airAccel = new Vector2(0,0.1f);
	public Vector2 AirAcceleration {
		get { return _airAccel; }
		set { _airAccel = value; }
	}
	public float HorizontalAirAcceleration { 
		get { return _airAccel.x; }
	}
	public float VerticalAirAcceleration {
		get { return _airAccel.y; }
	}
	
	[SerializeField]
	private Vector2 _maxAirVel = new Vector2 (8, 14);
	public Vector2 MaximumAirVelocity {
		get { return _maxAirVel; }
		set { _maxAirVel = value; }
	}
	public float MaximumHorizontalAirVelocity {
		get { return _maxAirVel.x; }
	}
	public float MaximumVerticalAirVelocity {
		get { return _maxAirVel.y; }
	}
	
	protected override void Awake ()
	{
		base.Awake ();
		_exitActions = new Func<bool>[] { Throw, Jump };
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
		Left 	(Input.GetAxis ("L_XAxis_" + _player.Joystick) < 0);
		Right 	(Input.GetAxis ("L_XAxis_" + _player.Joystick) > 0);
		
		float vy = rigidbody2D.velocity.y;
		vy = Mathf.Clamp (vy, -MaximumAirVelocity.y, MaximumAirVelocity.y);
		rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, vy);
	}

	protected override void OnCollisionEnter2D (Collision2D coll)
	{
		if (enabled == false) return;

		if(coll.gameObject.tag == "floor") {
			bool wall = false;
			bool floor = false;
			
			foreach (ContactPoint2D contact in coll.contacts) {
				//Debug.Log(name + " contact normal " + contact.normal);
				if (contact.normal == Vector2.up) {
					_exitState = GetComponent<StandingState>();
					_manager.Transition(this, _exitState);
					return;
				} 
				else if (contact.normal == Vector2.right || contact.normal == -Vector2.right) {
					GetComponent<WallSlidingState>().WallDirection = contact.normal != Vector2.right;
					_exitState = GetComponent<WallSlidingState>();
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
		
		if (coll.gameObject.tag == "Player") {
			HitPlayer(coll);
		}
	}

	void Left (bool condition) {
		if (condition) {
			float vx = rigidbody2D.velocity.x - AirAcceleration.x;
			vx = Mathf.Clamp (vx, -MaximumAirVelocity.x, MaximumAirVelocity.x);
			
			rigidbody2D.velocity = new Vector2 (vx, rigidbody2D.velocity.y);
		}
	}
	
	void Right (bool condition) {
		if (condition) {
			float vx = rigidbody2D.velocity.x + AirAcceleration.x;
			vx = Mathf.Clamp (vx, -MaximumAirVelocity.x, MaximumAirVelocity.x);
			rigidbody2D.velocity = new Vector2 (vx, rigidbody2D.velocity.y);
		}
	}

	bool Throw ()
	{
		if (Input.GetButtonDown("X_"+_player.Joystick) && _player.FireableBoomerangs > 0)
		{
			//_exitState = GetComponent<ThrowingState>();
			GetComponent<ThrowingState>().enabled = true;
			//return true;
		}
		return false;
	}

	bool Jump ()
	{
		if (Input.GetButtonDown ("A_" + _player.Joystick) &&
		    _multiJump.enabled)
		{
			_exitState = GetComponent<JumpingState>();
			return true;
		}
		return false;
	}

	private void HitPlayer (Collision2D coll) {
		// we don't care if we're on our way up
		if (rigidbody2D.velocity.y > 0) return;
		
		float deltaY = transform.position.y - coll.transform.position.y;
		Debug.Log("delta y " + deltaY);
		if (deltaY > 1f) {
			//coll.gameObject.GetComponent<PlayerController>();
			Debug.Log(_player.name + " stomped " + coll.gameObject.name);
			
			PlayerController other = coll.gameObject.GetComponent<PlayerController>();
			if (!other.Invincible) {
				other.PlayerStateManager.Transition(other.PlayerStateManager.CurrentState,
				                                    other.GetComponent<DyingState>());
				return;
			}
			_player.IncrementKill();
			
			_exitState = GetComponent<StompingState>();
			_manager.Transition(this, _exitState);
			
			GameObject.Find ("killcount" + _player.Joystick).GetComponent<GUIText>().text = "x" + _player.KillCount;
			GameObject.Find ("Win").GetComponent<WinCondition>().CheckWinner();
		}
	}
}

