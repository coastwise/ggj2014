﻿using UnityEngine;
using System.Collections;

public class WinCondition : MonoBehaviour {

	public PlayerController[] players;

	public PlayerController winner;

	public SpriteRenderer winTint;
	public GUIText playerWinnerText;
	public GUIText winnerText;
	public GUIText newGameText;

	public int counter;

	bool hasWon = false;


	// Use this for initialization
	void Start () {
		winTint.enabled = false;
		playerWinnerText.enabled = false;
		winnerText.enabled = false;
		newGameText.enabled = false;

	}
	
	// Update is called once per frame
	void Update () {
		if (hasWon) {

			if (Input.GetButtonDown("Start_1") ||
			    Input.GetButtonDown("Start_2") ||
			    Input.GetButtonDown("Start_3") ||
			    Input.GetButtonDown("Start_4")) {

				Time.timeScale = 1;	
				Application.LoadLevel(StageSelect.chosenLevel);

			}


		}
	}

	public void CheckWinner () {
		foreach (PlayerController player in players) {
			if (player.killcount >= counter) {
				winner = player;

				playerWinnerText.text = "Player " + winner.joystick;

				TriggerGameEnd();
			}
		}
	}

	public void TriggerGameEnd () {
		Time.timeScale = 0;
		winTint.enabled = true;
		playerWinnerText.enabled = true;
		winnerText.enabled = true;
		newGameText.enabled = true;
		hasWon = true;
	}

}
