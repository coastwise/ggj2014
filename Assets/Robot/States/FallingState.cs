using UnityEngine;
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
}
