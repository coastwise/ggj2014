using UnityEngine;
using System.Collections;

public class ThrowingState : PlayerState {

	protected override void Awake ()
	{
		base.Awake ();
	}

	protected override void Start ()
	{
	}

	protected override void Update ()
	{
		//_exitState = _manager.PreviousState;
		//_manager.Transition (this, _exitState);
		this.enabled = false;
	}

	protected override void FixedUpdate ()
	{
	}

	protected override void OnEnable ()
	{
		float xAxis = Input.GetAxis("L_XAxis_"+_player.Joystick);
		if (xAxis > 0.0f)
			xAxis = 1.0f;
		else if (xAxis < 0.0f)
			xAxis = -1.0f;
		
		float yAxis = Input.GetAxis("L_YAxis_"+_player.Joystick);
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
		GameObject boomerang = (GameObject)Instantiate(_player._boomerangPrefab, transform.position + (projectileDirection * offset), Quaternion.identity);
		boomerang.GetComponent<BoomerangController>().CreateBoomerang(this.gameObject, projectileDirection);
		_player.FireableBoomerangs -= 1;
		
		_player.PlaySound(SoundEffects.Throw);
	}

	protected override void OnDisable ()
	{
	}

	protected override void PerformAction ()
	{

	}

}
