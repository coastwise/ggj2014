using UnityEngine;
using System.Collections;

public class JumpExplosion : MonoBehaviour
{
	private float _timer = 0.4f;

	void Update()
	{
		if (_timer <= 0.0f)
			Destroy(this.gameObject);
		_timer -= Time.deltaTime;
	}
}