using UnityEngine;
using System;
using System.Collections;

[RequireComponent(typeof(FallingState))]
public class JumpingState : PlayerState {

	private FallingState _fallingState;

	[SerializeField]
	private float _instantJumpVel = 8f; // float instead of vector because straight jump (only Y)
	public float InstantJumpVelocity {
		get { return _instantJumpVel; }
	}
	
	protected override void Awake ()
	{
		base.Awake ();
		_fallingState = GetComponent<FallingState> ();
		_exitActions = new Func<bool>[] { Throw, Jump, Falling };

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
		Debug.Log ("Entered Jumping State");
		gameObject.rigidbody2D.velocity += Vector2.up * InstantJumpVelocity;
		_player.PlaySound (SoundEffects.Jump);

		_multiJump.IncrementJump ();
    }
    
	protected override void OnDisable ()
    {
        
    }
    
    protected override void PerformAction ()
	{
		Left 	(Input.GetAxis ("L_XAxis_" + _player.Joystick) < 0);
		Right 	(Input.GetAxis ("L_XAxis_" + _player.Joystick) > 0);

		float vy = rigidbody2D.velocity.y;
		vy = Mathf.Clamp (vy, -_fallingState.MaximumAirVelocity.y, _fallingState.MaximumAirVelocity.y);
		rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, vy);
	}

	protected override void OnCollisionEnter2D (Collision2D coll)
	{
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
			float vx = rigidbody2D.velocity.x - _fallingState.AirAcceleration.x;
			vx = Mathf.Clamp (vx, -_fallingState.MaximumAirVelocity.x, _fallingState.MaximumAirVelocity.x);

			rigidbody2D.velocity = new Vector2 (vx, rigidbody2D.velocity.y);
		}
	}

	void Right (bool condition) {
		if (condition) {
			float vx = rigidbody2D.velocity.x + _fallingState.AirAcceleration.x;
			vx = Mathf.Clamp (vx, -_fallingState.MaximumAirVelocity.x, _fallingState.MaximumAirVelocity.x);
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

	bool Jump () {
		if (Input.GetButtonDown ("A_" + _player.Joystick) &&
		    _multiJump.enabled)
		{
			_exitState = this;
			return true;
		}
		return false;
	}

	bool Falling () {
		if (rigidbody2D.velocity.y < 0) {
			_exitState = GetComponent<FallingState>();
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
	/*




	public override void Update () {

	}
	*/
}
