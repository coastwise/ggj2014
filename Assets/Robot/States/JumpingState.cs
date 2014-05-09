using UnityEngine;
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
		_fallingState = GetComponent<FallingState> ();
	}
	
	protected override void Start ()
	{
		
	}
	
	protected override void Update ()
	{
		
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
	/*
	public JumpingState (PlayerController player) : base (player) {}

	public override void OnEnter () { 

		player.gameObject.rigidbody2D.velocity += Vector2.up * player.InstantJumpVelocity;
		player.GetComponent<Animator>().SetTrigger("Jump");

		player.PlaySound (SoundEffects.Jump);

		if(!player.MultiJump.enabled){
			player.GetComponent<Animator>().SetBool("Land", false);
		}
	}
	
	override public void Jump () {
		//Debug.Log("Player " + player.joystick + " Jumping Jump");



		if (player.MultiJump.enabled)
		{
			player.GetComponent<MultiJump>().SpawnDoublejumpExplosion();
			player.EnterState(typeof(JumpingState));
		}
	}
	
	override public void Throw () {
		Debug.Log("Player " + player.Joystick + " Jumping Throw");
	}

	override public void Left () {
		float vx = player.rigidbody2D.velocity.x - player.AirAcceleration.x;
		vx = Mathf.Clamp(vx, -player.MaximumAirVelocity.x, player.MaximumAirVelocity.x);

		player.rigidbody2D.velocity = new Vector2(vx, player.rigidbody2D.velocity.y);
	}
	
	override public void Right () {
		float vx = player.rigidbody2D.velocity.x + player.AirAcceleration.x;
		vx = Mathf.Clamp(vx, -player.MaximumAirVelocity.x, player.MaximumAirVelocity.x);
		player.rigidbody2D.velocity = new Vector2(vx, player.rigidbody2D.velocity.y);
	}

	override public void HitFloor(){
		Debug.Log("Player " + player.Joystick + " Landed");
		player.EnterState(typeof(StandingState));

		player.GetComponent<Animator>().SetBool("Land", true);
	}

	override public void HitWall () {
		player.EnterState(typeof(WallSlidingState));
	}

	public override void HitPlayer (Collision2D coll) {
		// we don't care if we're on our way up
		if (player.rigidbody2D.velocity.y > 0) return;

		float deltaY = player.transform.position.y - coll.transform.position.y;
		Debug.Log("delta y " + deltaY);
		if (deltaY > 0.5f) {
			//coll.gameObject.GetComponent<PlayerController>();
			Debug.Log(player.name + " stomped " + coll.gameObject.name);

			PlayerController other = coll.gameObject.GetComponent<PlayerController>();
			if (!other.Invincible) other.EnterState(typeof(DyingState));

			player.EnterState(typeof(StompingState));
			player.IncrementKill();
			GameObject.Find ("killcount" + player.Joystick).GetComponent<GUIText>().text = "x" + player.KillCount;
			GameObject.Find ("Win").GetComponent<WinCondition>().CheckWinner();

		}
	}

	public override void Update () {
		float vy = player.rigidbody2D.velocity.y;
		vy = Mathf.Clamp (vy, -player.MaximumAirVelocity.y, player.MaximumAirVelocity.y);
		player.rigidbody2D.velocity = new Vector2(player.rigidbody2D.velocity.x, vy);

		player.GetComponent<Animator>().SetFloat("y-velocity", player.rigidbody2D.velocity.y);
	}
	*/
}
