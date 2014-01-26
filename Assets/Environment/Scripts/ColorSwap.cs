using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ColorSwap : MonoBehaviour {

	GameObject[] 	tiles;
	Sprite[] sprites;
	int[]			tileColors;

	float colorTimer = 2;
	
	List<Sprite> _tilesBlue = new List<Sprite>();
	List<Sprite> _tilesGreen = new List<Sprite>();
	List<Sprite> _tilesRed = new List<Sprite>();
	List<Sprite> _tilesYellow = new List<Sprite>();

	// Use this for initialization
	void Start () {
		tiles = GameObject.FindGameObjectsWithTag ("tile");
		sprites = Resources.LoadAll<Sprite> ("tiles");

		tileColors = new int[tiles.Length];
		for (int i = 0; i < tiles.Length; i++) {
			Sprite sr = tiles[i].GetComponent<SpriteRenderer>().sprite;
			string tilenumber = sr.name.Split('_')[1];
			tileColors[i] = int.Parse(tilenumber);
		}
	}
	
	// Update is called once per frame
	void Update () {

		colorTimer -= Time.deltaTime;

		if (colorTimer < 0) {

			SwapColor ();
			colorTimer = 2;
		}
	}

	void SwapColor () {
		for (int i = 0; i < tiles.Length; i++) {


			tileColors[i] += 4;
			
			if (tileColors[i] > 19) {
				tileColors[i] = 4;
			}
			
			tiles[i].GetComponent<SpriteRenderer>().sprite = sprites[tileColors[i]];
			
			if (tileColors[i] > 16) {
				tiles[i].transform.parent.gameObject.layer = LayerMask.NameToLayer("red terrain");
				tiles[i].transform.gameObject.layer = LayerMask.NameToLayer("red terrain");
			} else if (tileColors[i] > 12) {
				tiles[i].transform.parent.gameObject.layer = LayerMask.NameToLayer("yellow terrain");
				tiles[i].transform.gameObject.layer = LayerMask.NameToLayer("yellow terrain");
			} if (tileColors[i] > 8) {
				tiles[i].transform.parent.gameObject.layer = LayerMask.NameToLayer("blue terrain");
				tiles[i].transform.gameObject.layer = LayerMask.NameToLayer("blue terrain");
			} if (tileColors[i] > 4) {
				tiles[i].transform.parent.gameObject.layer = LayerMask.NameToLayer("green terrain");
				tiles[i].transform.gameObject.layer = LayerMask.NameToLayer("green terrain");
			} 


		}
	}



}
