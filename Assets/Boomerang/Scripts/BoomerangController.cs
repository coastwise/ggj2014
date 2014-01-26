using UnityEngine;
using System.Collections;

public class BoomerangController : MonoBehaviour
{
	private Vector3 _rotation = new Vector3(0.0f, 0.0f, -3200.0f);

	public bool _isActive = true;
	private bool _returning;
	private Vector3 _direction;

	private GameObject _owner;

	private float _spawnTime;

	public void CreateBoomerang(GameObject parent, Vector3 direction)
	{
		_owner = parent;
		this.gameObject.layer = parent.layer; /* red weapon layer is 4 higher than red player */
		_direction = direction;
		rigidbody2D.gravityScale = 0.005f;
		_spawnTime = Time.time;
		_returning = false;
		_isActive = true;
	}

	void Update()
	{
		if (!_isActive) return;

		transform.Rotate(_rotation * Time.deltaTime);
		if (!_returning)
		{
			float sin = Mathf.Sin(((Time.time - _spawnTime) * 4.0f) + Mathf.Deg2Rad * 90.0f);
			transform.Translate(_direction * sin * Time.deltaTime * 24.0f, Space.World);
			Debug.Log(_direction);
			if (sin <= 0)
				_returning = true;
		}
		else
			transform.Translate((_owner.transform.position - transform.position).normalized * Time.deltaTime * 12.0f, Space.World);
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "Player") {
			PlayerController player = collision.gameObject.GetComponent<PlayerController>();
			if (!_isActive || (_returning && collision.gameObject.Equals(_owner)) ) {
				player._fireableBoomerangs += 1;
				Destroy(this.gameObject);
			} else {
				if (!player.isInvincible) player.EnterState(typeof(DyingState));
				rigidbody2D.gravityScale = 0.1f;
				_isActive = false;
			}
		}
		else
		{
			//this.gameObject.layer = 20; // Become pickupable
			rigidbody2D.gravityScale = 0.1f;
			_isActive = false;
		}
	}
}