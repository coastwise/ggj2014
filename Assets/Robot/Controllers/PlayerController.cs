using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour {

	public int joystick;

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

	private Dictionary<System.Type, PlayerState> states;
	private PlayerState currentState;

	// "is false when preceded by its quotation" is false when preceded by its quotation

	public int _fireableBoomerangs = 3;
	public GameObject _boomerangPrefab;

	void Start () {
		name = "Player " + joystick;

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

	void Update () {
		// check my input and call state methods
		if (Input.GetButtonDown("X_"+joystick) && _fireableBoomerangs > 0)
		{
			//GameObject boomerang = (GameObject)Instantiate(_boomerangPrefab, transform.position, Quaternion.identity);

			if (Input.GetAxis("L_XAxis_"+joystick) > 0)
			{
				GameObject boomerang = (GameObject)Instantiate(_boomerangPrefab, transform.position + Vector3.right, Quaternion.identity);
				boomerang.GetComponent<BoomerangController>().CreateBoomerang(this.gameObject, Vector3.right);
				_fireableBoomerangs -= 1;
			}
			else if (Input.GetAxis("L_XAxis_"+joystick) < 0)
			{
				GameObject boomerang = (GameObject)Instantiate(_boomerangPrefab, transform.position + Vector3.left, Quaternion.identity);
				boomerang.GetComponent<BoomerangController>().CreateBoomerang(this.gameObject, Vector3.left);
				_fireableBoomerangs -= 1;
			}
			gameObject.audio.PlayOneShot(throwSound);
		}

		if (Input.GetButtonDown("A_"+joystick)) {
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
}
