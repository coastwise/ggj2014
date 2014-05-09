using UnityEngine;
using System.Collections;

public class StompingState : PlayerState {

	[SerializeField]
	private float _stompJumpTimeout = 0.2f; // seconds
	public float StompJumpTimeout {
		get { return _stompJumpTimeout; }
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
	//public StompingState (PlayerController player) : base (player) {}

	//float countdown;
	/*
	override public void Jump () {
		Debug.Log("Player " + player.Joystick + " Stomping Jump");
		player.EnterState(typeof(JumpingState));
	}

	override public void OnEnter () {
		countdown = player.StompJumpTimeout; // seconds
	}

	override public void HitFloor(){
		player.EnterState(typeof(StandingState));
	}

	override public void Update () {
		countdown -= Time.deltaTime;
		if (countdown < 0) {
			player.EnterState(typeof(FallingState));
		}
	}*/

}
