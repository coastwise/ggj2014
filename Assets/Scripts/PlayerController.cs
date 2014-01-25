using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public Boomerang _boomerang;
	bool hasBoomerang = true;
	public GameObject spawnPointBoomerang;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Space) && hasBoomerang) {
			InitBoomerang();
			EnableBoomerang();
		}
	}

	void InitBoomerang() {
		_boomerang.Init(transform.localScale.x * 20,
		                transform.position + (Vector3.right * transform.localScale.x * 1.25f),
		                Vector3.right * transform.localScale.x);
	}

	void EnableBoomerang () {
		_boomerang.gameObject.SetActive(true);
		hasBoomerang = false;
	}

	void OnTriggerEnter2D (Collider2D collider) {
		if (collider.gameObject.Equals(_boomerang.gameObject)) {
			_boomerang.gameObject.SetActive(false);
			hasBoomerang = true;
		}
	}
}
