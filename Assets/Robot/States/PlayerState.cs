using UnityEngine;
using System;
using System.Collections;

[RequireComponent(typeof(PlayerController))]
[RequireComponent(typeof(PlayerStateManager))]
[RequireComponent(typeof(MultiJump))]
public class PlayerState : MonoBehaviour {

	protected PlayerController _player;
	protected MultiJump _multiJump;
	
	protected Func<bool>[] 	_exitActions;
	protected PlayerState		_exitState; // will be set by exit actions

	protected virtual void PerformAction () {}

	protected virtual void Awake () {}
	protected virtual void Start () {}
	protected virtual void Update () {}
	protected virtual void FixedUpdate () {}
	protected virtual void OnEnable () {}
	protected virtual void OnDisable () {}
}
