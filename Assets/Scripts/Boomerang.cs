using UnityEngine;
using System.Collections;

public class Boomerang : MonoBehaviour {

	public GameObject _origin;
	Vector3 _velocity;
	// Use this for initialization
	void Start () {
		_velocity = new Vector3(14,0,0);
	}
	
	// Update is called once per frame
	void Update () {
		Rotate (540);
		BasicReturn ();
	}

	/// <summary>
	/// Rotates the object at a given angle per second
	/// </summary>
	/// <param name="angle">Angle.</param>
	void Rotate (float angle) {
		transform.Rotate(Vector3.forward, -angle * Time.deltaTime);
	}

	/// <summary>
	/// Basics the return.
	/// </summary>
	void BasicReturn () {
		Vector3 delta = _origin.transform.position - transform.position;

		Vector3 moreDelta = delta * 1.1f + _velocity;

		if (moreDelta.magnitude > 14) {
		moreDelta.Normalize();
		moreDelta *= 14;
		}
		_velocity = moreDelta;

		transform.Translate(_velocity * Time.deltaTime, Space.World);
	}
}
