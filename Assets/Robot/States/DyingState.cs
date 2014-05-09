using UnityEngine;
using System.Collections;

public class DyingState : PlayerState {
	
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

	protected override void OnCollisionEnter2D (Collision2D coll)
	{
		
		if (enabled == false) return;
	}

	protected override void OnCollisionExit2D (Collision2D coll)
	{
		
		if (enabled == false) return;
	}

	/*
	public DyingState (PlayerController player) : base (player) {}

	float countdown;

	public override void OnEnter () {
		player.GetComponent<Animator>().SetTrigger("Explode");
		countdown = player.RespawnTimeout;

		player.collider2D.enabled = false;
		player.rigidbody2D.isKinematic = true;

		player.PlaySound (SoundEffects.Explosion);

		player.FireableBoomerangs = 0;
	}

	override public void Update () {
		countdown -= Time.deltaTime;
		if (countdown < 0) {
			Debug.Log("Respawn");
			player.collider2D.enabled = true;
			player.rigidbody2D.isKinematic = false;
			player.FireableBoomerangs = 3;
			player.EnterState(typeof(StandingState));
			player.GetComponent<Animator>().SetTrigger("Respawn");
			player.Respawn();
		}
	}
	*/
}
