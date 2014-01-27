using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour {

	public int joystick;

	public List<Transform> spawnPoints;

	public GameObject _doublejumpExplosion;
	public bool _canDoublejump = false;

	//public int color;

	public AudioClip explosionSound;
	public AudioClip jumpSound;
	public AudioClip throwSound;
	public AudioClip wallJumpSound;

	public float groundAcceleration = 2.6f;
	public float maxGroundVelocity = 16f;

	public float instantaneousJumpVelocity = 10f;
	public float horizontalAirAcceleration = 0.3f;
	public float maxAirHorizontalVelocity = 16f;

	public float stompingJumpTimeout = 0.2f; // seconds

	public float respawnTimeout = 2f; // seconds

	public Vector2 wallJumpInstantaneousVelocityDir = Vector2.one * 10f;

	private bool _wallRight;
	public bool wallRight {
		get; private set;
	}

	public bool isInvincible = false;

	private Color cachedPlayerTint;

	private Dictionary<System.Type, PlayerState> states;
	private PlayerState currentState;

	// "is false when preceded by its quotation" is false when preceded by its quotation

	public int _fireableBoomerangs = 3;
	public GameObject _boomerangPrefab;

	void Start () {
		name = "Player " + joystick;

		cachedPlayerTint = gameObject.GetComponent<SpriteRenderer>().color;

		// initialize states once (no gc)
		states = new Dictionary<System.Type, PlayerState>();
		states.Add(typeof(StandingState), new StandingState(this));
		states.Add(typeof(JumpingState), new JumpingState(this));
		states.Add(typeof(StompingState), new StompingState(this));
		states.Add(typeof(FallingState), new FallingState(this));
		states.Add(typeof(WallSlidingState), new WallSlidingState(this));
		states.Add(typeof(DyingState), new DyingState(this)); 

		explosionSound = (AudioClip)Resources.Load("explosion");
		jumpSound = (AudioClip)Resources.Load("jump");
		throwSound = (AudioClip)Resources.Load ("boomerang1");
		wallJumpSound = (AudioClip)Resources.Load("walljump");
		
		// enter the initial state
		currentState = states[typeof(StandingState)];
		currentState.OnEnter();
	}

	public void EnterState (System.Type newState) {
		Debug.Log("Player " + joystick + " transitioning from " + currentState.GetType() + " to " + newState);
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

	public void SpawnDoublejumpExplosion() {
		Instantiate(_doublejumpExplosion, transform.position + (Vector3.down * 0.2f), Quaternion.identity);
	}

	void Update () {
		// check my input and call state methods
		if (Input.GetButtonDown("X_"+joystick) && _fireableBoomerangs > 0)
		{
			float xAxis = Input.GetAxis("L_XAxis_"+joystick);
			float yAxis = Input.GetAxis("L_YAxis_"+joystick);

			//Vector3 projectileDirection = EightProjectileDirection(xAxis, -yAxis);
			Vector3 projectileDirection = new Vector3(xAxis, -yAxis, 0.0f).normalized;

			if (projectileDirection != Vector3.zero)
			{
				float offset = 0.8f;
				if (yAxis == 0.0f)
					offset = 0.5f;
				GameObject boomerang = (GameObject)Instantiate(_boomerangPrefab, transform.position + (projectileDirection * offset), Quaternion.identity);
				boomerang.GetComponent<BoomerangController>().CreateBoomerang(this.gameObject, projectileDirection);
				_fireableBoomerangs -= 1;
			}
			gameObject.audio.PlayOneShot(throwSound);
		}

		if (Input.GetButtonDown("A_"+joystick))
		{
//			if ((currentState.GetType() == typeof(JumpingState) || currentState.GetType() == typeof(FallingState)) && _canDoublejump)
//			{
//				Debug.Log("FLAG");
//			}
			currentState.Jump();
		}

		if (Input.GetAxis("L_XAxis_"+joystick) > 0) {
			currentState.Right();
		} else if (Input.GetAxis("L_XAxis_"+joystick) < 0) {
			currentState.Left();
		} else {
			currentState.Idle();
		}

		currentState.Update();
	}

	Vector3 EightProjectileDirection (float x, float y) {
		if (x > 0.0f)
			x = 1.0f;
		else if (x < 0.0f)
			x = -1.0f;

		if (y > 0.0f)
			y = 1.0f;
		else if (y < 0.0f)
			y = -1.0f;

		return new Vector3(x,y,0).normalized;
	}

	public void FinishedExploding () {
		gameObject.GetComponent<SpriteRenderer>().color = cachedPlayerTint;
		gameObject.renderer.enabled = false;
	}

	public void Respawn () {
		isInvincible = true;
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
		isInvincible = false;
	}

}
