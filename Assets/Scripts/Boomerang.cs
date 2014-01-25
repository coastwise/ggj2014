using UnityEngine;
using System.Collections;

public class Boomerang : MonoBehaviour {

	public GameObject _origin;
	public Vector3 _spawnPoint;
	public Vector3 _throwDirection;
	public float _speed;
	public float _spawnTime;
	Vector3 _velocity;

	public delegate void BoomerangUpdate();

	public BoomerangUpdate boomerangUpdate;
	// Use this for initialization
	void Start () {
	}

	public void Init(float speed, Vector3 spawnPoint, Vector3 throwDirection) {
		transform.position = _spawnPoint;
		_speed = speed;
		_spawnPoint = spawnPoint;
		_throwDirection = throwDirection;
		_spawnTime = Time.time;
		transform.localRotation = Quaternion.identity;
		boomerangUpdate = SineGo;
	}
	
	// Update is called once per frame
	void Update () {
		Rotate (720);
		boomerangUpdate ();
	}

	/// <summary>
	/// Rotates the object at a given angle per second
	/// </summary>
	/// <param name="angle">Angle.</param>
	void Rotate (float angle) {
		transform.Rotate(Vector3.forward, -angle * Time.deltaTime);
	}

	void SineReturn () {
		Vector3 delta = _origin.transform.position - transform.position;
		transform.Translate (delta.normalized * _speed * Time.deltaTime, Space.World);
	}


	void SineGo () {

		transform.Translate(_throwDirection * _speed * Mathf.Sin((Time.time - _spawnTime) * 4f + Mathf.Deg2Rad * 90) * Time.deltaTime, Space.World);


		if (Mathf.Sin (Time.time - _spawnTime) < Mathf.Sin (Time.time - _spawnTime - Time.deltaTime)) {
			// Change boomerangs collision flags to include player
			boomerangUpdate = SineReturn;
		}
	}

}
