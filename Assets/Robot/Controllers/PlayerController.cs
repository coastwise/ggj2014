using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour {

	private MultiJump _multiJump;
	public MultiJump MultiJump {
		get { return _multiJump; }
	}

	private int _joystick;
	public int Joystick
	{
		get { return _joystick; }
		set { _joystick = value;}
	}

	public List<Transform> spawnPoints;

	private AudioClip[] sounds;
	
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

	[SerializeField]
	private float _instantJumpVel = 8f; // float instead of vector because straight jump (only Y)
	public float InstantJumpVelocity {
		get { return _instantJumpVel; }
	}

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

	[SerializeField]
	private float _stompJumpTimeout = 0.2f; // seconds
	public float StompJumpTimeout {
		get { return _stompJumpTimeout; }
	}

	[SerializeField]
	private float _respawnTimeout = 2f; // seconds
	public float RespawnTimeout {
		get { return _respawnTimeout; }
	}

	[SerializeField]
	private Vector2 _wallJumpInstantVelocity = Vector2.one * 5f;
	public Vector2 InstantWallJumpVelocity {
		get { return _wallJumpInstantVelocity; }
		set { _wallJumpInstantVelocity = value; }
	}


	private bool _wallRight;
	public bool wallRight {
		get; private set;
	}

	[SerializeField]
	private bool _isInvincible = false;
	public bool Invincible {
		get { return _isInvincible; }
		set { _isInvincible = value; }
	}


	private Color _cachedPlayerTint;

	private Dictionary<System.Type, PlayerState> states;
	private PlayerState currentState;

	// "is false when preceded by its quotation" is false when preceded by its quotation

	[SerializeField]
	private int _fireableBoomerangs = 3;
	public int FireableBoomerangs {
		get { return _fireableBoomerangs; }
		set { _fireableBoomerangs = value; }
	}

	public GameObject _boomerangPrefab;

	[SerializeField]
	private int _killcount = 0;
	public int KillCount {
		get { return _killcount; }
	}

	void Awake () {
		_multiJump = GetComponent<MultiJump> ();
		sounds = new AudioClip[4];
		sounds [(int)SoundEffects.Explosion] = 	(AudioClip)Resources.Load ("explosion");
		sounds [(int)SoundEffects.Jump] = 		(AudioClip)Resources.Load ("jump");
		sounds [(int)SoundEffects.Throw] = 		(AudioClip)Resources.Load ("boomerang1");
		sounds [(int)SoundEffects.Explosion] = 	(AudioClip)Resources.Load ("walljump");

	}

	void Start () {

        Debug.Log("player controller called");

		_cachedPlayerTint = gameObject.GetComponent<SpriteRenderer>().color;

		// initialize states once (no gc)
		states = new Dictionary<System.Type, PlayerState>();
		states.Add(typeof(StandingState), new StandingState(this));
		states.Add(typeof(JumpingState), new JumpingState(this));
		states.Add(typeof(StompingState), new StompingState(this));
		states.Add(typeof(FallingState), new FallingState(this));
		states.Add(typeof(WallSlidingState), new WallSlidingState(this));
		states.Add(typeof(DyingState), new DyingState(this)); 
		
		// check who's playing
		if(!CharacterSelect.yellowPlayer && gameObject.layer == LayerMask.NameToLayer("yellow player")){
			gameObject.SetActive(false);
			Debug.Log("no yellow");
		}
		else if(CharacterSelect.yellowPlayer && gameObject.layer == LayerMask.NameToLayer("yellow player")){
			Joystick = 1;
		}
		else if(!CharacterSelect.greenPlayer && gameObject.layer == LayerMask.NameToLayer("green player")){
			gameObject.SetActive(false);
			Debug.Log("no green");
		}
		else if(CharacterSelect.greenPlayer && gameObject.layer == LayerMask.NameToLayer("green player")){
			Joystick = 2;
		}
		else if(!CharacterSelect.redPlayer && gameObject.layer == LayerMask.NameToLayer("red player")){
			gameObject.SetActive(false);
			Debug.Log("no red");
		}
		else if(CharacterSelect.redPlayer && gameObject.layer == LayerMask.NameToLayer("red player")){
			Joystick = 3;
			Debug.Log("joystick: " + Joystick);
		}
		else if(!CharacterSelect.bluePlayer && gameObject.layer == LayerMask.NameToLayer("blue player")){
			gameObject.SetActive(false);
			Debug.Log("no blue");
		}
		else if(CharacterSelect.bluePlayer && gameObject.layer == LayerMask.NameToLayer("blue player")){
			Joystick = 4;
		}

		// enter the initial state
		currentState = states[typeof(StandingState)];
		currentState.OnEnter();
	}

	public void EnterState (System.Type newState) {
		Debug.Log("Player " + Joystick + " transitioning from " + currentState.GetType() + " to " + newState);
		currentState.OnExit();
		currentState = states[newState];
		currentState.OnEnter();
	}

	void OnCollisionEnter2D(Collision2D coll){

		if(coll.gameObject.tag == "floor") {
			bool wall = false;
			bool floor = false;

			foreach (ContactPoint2D contact in coll.contacts) {
				//Debug.Log(name + " contact normal " + contact.normal);
				if (contact.normal == Vector2.up) {
					floor = true;
				} 
				else if (contact.normal == Vector2.right || contact.normal == -Vector2.right) {
					wallRight = contact.normal != Vector2.right;
					wall = true;
				}
				else{
					currentState.Idle();
				}
			}

			if (floor) {
				currentState.HitFloor();
			} else if (wall) {
				currentState.HitWall();
			} // else ceiling
		}

		if (coll.gameObject.tag == "Player") {
			currentState.HitPlayer(coll);
		}
	}



	void Update () {

		// check my input and call state methods
		if (Input.GetButtonDown("X_"+Joystick) && _fireableBoomerangs > 0)
		{
			float xAxis = Input.GetAxis("L_XAxis_"+Joystick);
			if (xAxis > 0.0f)
				xAxis = 1.0f;
			else if (xAxis < 0.0f)
				xAxis = -1.0f;

			float yAxis = Input.GetAxis("L_YAxis_"+Joystick);
			if (yAxis > 0.0f)
				yAxis = 1.0f;
			else if (yAxis < 0.0f)
				yAxis = -1.0f;

			Vector3 projectileDirection = new Vector3(xAxis, -yAxis, 0.0f).normalized;
			if (projectileDirection == Vector3.zero)
				projectileDirection += Vector3.right * gameObject.transform.localScale.x;

				float offset = 0.8f;
				if (yAxis == 0.0f)
					offset = 0.5f;
				GameObject boomerang = (GameObject)Instantiate(_boomerangPrefab, transform.position + (projectileDirection * offset), Quaternion.identity);
				boomerang.GetComponent<BoomerangController>().CreateBoomerang(this.gameObject, projectileDirection);
				_fireableBoomerangs -= 1;

			PlaySound(SoundEffects.Throw);
		}

		if (Input.GetButtonDown("A_"+Joystick))
		{
//			if ((currentState.GetType() == typeof(JumpingState) || currentState.GetType() == typeof(FallingState)) && _canDoublejump)
//			{
//				Debug.Log("FLAG");
//			}
			currentState.Jump();
		}

		if (Input.GetAxis("L_XAxis_"+Joystick) > 0) {
			currentState.Right();
		} else if (Input.GetAxis("L_XAxis_"+Joystick) < 0) {
			currentState.Left();
		} else {
			currentState.Idle();
		}

		currentState.Update();
	}

	public void FinishedExploding () {
		gameObject.GetComponent<SpriteRenderer>().color = _cachedPlayerTint;
		gameObject.renderer.enabled = false;
	}

	public void Respawn () {
		_isInvincible = true;
		gameObject.renderer.enabled = true;
		transform.position = spawnPoints[Random.Range(0,spawnPoints.Count)].position;
		StartCoroutine(BlinkForAWhile());
	}

	public IEnumerator BlinkForAWhile () {
		for(int i = 0; i < 60; i++) {
			yield return null;
			gameObject.renderer.enabled = false;
			yield return null;
			gameObject.renderer.enabled = true;
		}
		_isInvincible = false;
	}

	public void PlaySound (SoundEffects sfx) {
		gameObject.audio.PlayOneShot (sounds [(int)sfx]);
	}

	public void IncrementKill () {
		_killcount++;
	}
}

public enum SoundEffects {
	Explosion,
	Jump,
	Throw,
	WallJump
};
