using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		int i = 1;
        Debug.Log("PlayerManager called");
        foreach (GameObject go in GameObject.FindGameObjectsWithTag("Player")){
            PlayerController p = go.GetComponent<PlayerController>();
            p.Joystick = i++;
            p.name = "Player " + p.Joystick;
        }
	}
	// Update is called once per frame
	void Update () {
	
	}
}
