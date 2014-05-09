using UnityEngine;
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
			
			if (Input.GetButtonDown("B_1") ||
			    Input.GetButtonDown("B_2") ||
			    Input.GetButtonDown("B_3") ||
			    Input.GetButtonDown("B_4")) {
				
				Time.timeScale = 1;	
				Application.LoadLevel("CharacterSelect");
				
			}

		}
	}

	public void CheckWinner () {
		foreach (PlayerController player in players) {
			if (player.KillCount >= counter) {
				winner = player;

				playerWinnerText.text = "Player " + winner.Joystick;

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
