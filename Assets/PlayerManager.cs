using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		int i = 1;
		foreach (GameObject go in GameObject.FindGameObjectsWithTag("Player")) 
			go.GetComponent<PlayerController>().Joystick = i++;

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
