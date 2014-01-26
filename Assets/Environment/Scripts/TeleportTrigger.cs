using UnityEngine;
using System.Collections;

public class TeleportTrigger : MonoBehaviour {

	public Transform destination;

	private Vector3 delta;

	void Start () {
		delta = destination.position - transform.position;
	}

	void OnTriggerEnter2D (Collider2D other) {
		other.gameObject.transform.position += delta;
	}

}
