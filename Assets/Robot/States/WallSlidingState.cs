using UnityEngine;
using System.Collections;

public class WallSlidingState : PlayerState {

	
	[SerializeField]
	private Vector2 _wallJumpInstantVelocity = Vector2.one * 5f;
	public Vector2 InstantWallJumpVelocity {
		get { return _wallJumpInstantVelocity; }
		set { _wallJumpInstantVelocity = value; }
	}
	
	
	protected override void Awake ()
	{
		
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
	public bool right;

	public WallSlidingState (PlayerController c) : base (c) {}

	override public void Jump () {
		Vector2 jump = player.InstantWallJumpVelocity;
		if (player.wallRight) {
			jump = new Vector2(jump.x * -1, jump.y);
		}

		player.gameObject.rigidbody2D.velocity += jump;

		player.EnterState(typeof(FallingState));
		player.GetComponent<Animator>().SetTrigger("Jump");

		player.PlaySound (SoundEffects.WallJump);
	}
*/
}
