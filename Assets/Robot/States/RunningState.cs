using UnityEngine;
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
