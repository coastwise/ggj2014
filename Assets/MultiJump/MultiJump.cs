using UnityEngine;
using System.Collections;

public class MultiJump : MonoBehaviour {

	private int jumpCount = 0;

	private int maxJumpCount = 1;
	public int MaxJumpCount {
		get {
			return maxJumpCount;
		}
		set {
			if (value <= 0) maxJumpCount = 1;
			else 			maxJumpCount = value;
		}
	}

	public GameObject jumpExplosion;

	void OnEnable ()
	{
		jumpCount = 0;
	}

	public void SpawnDoublejumpExplosion() {
		Instantiate(jumpExplosion, 
		            transform.position + (Vector3.down * 0.2f), 
		            Quaternion.identity);
		IncrementJump ();
	}

	private void IncrementJump () {
		if (++jumpCount >= MaxJumpCount) {
			enabled = false;
		}
	}
}
