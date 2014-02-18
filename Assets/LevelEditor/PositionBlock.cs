using UnityEngine;
using System.Collections;

public enum Orientation{
	Horizontal,
	Vertical
}

public class PositionBlock : MonoBehaviour {

	float[] hori_limit2 = {4.25f,-7.5f,-4.25f,7.5f};
	float[] vert_limit2 = {4.0f,-7.75f,-4.0f,7.75f};
	float[] hori_limit4 = {4.25f,-7.5f,-4.25f,6.5f};
	float[] vert_limit4 = {3.0f,-7.75f,-4.0f,7.75f};

	public int orientation = (int)Orientation.Horizontal;
	//public int numBlocks = 2;
	public int numBlocks;
	public bool hasFocus;
	public static float oneBlock = 0.5f;
	public static float halfBlock = (float)(oneBlock/2);

	Color originalColor;

	// Use this for initialization
	void Start () {
		originalColor = transform.GetChild(0).gameObject.renderer.material.color;
	}
	
	// Update is called once per frame
	void Update () {

		if(hasFocus){

			foreach(Transform child in transform){
				child.renderer.material.color = Color.gray;
			}

			if(Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)){
				verticalPosition(oneBlock);
			}
			if(Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S)){
				verticalPosition(-oneBlock);
			}
			if(Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D)){
				horizontalPosition(oneBlock);
			}
			if(Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A)){
				horizontalPosition(-oneBlock);
			}
			if(Input.GetKeyDown(KeyCode.R)){
				rotate ();
			}
		}
		else{
			foreach(Transform child in transform){
				child.renderer.material.color = originalColor;
			}
		}
	}

	private void rotate(){

		if(orientation == (int)Orientation.Horizontal){
			transform.Rotate(new Vector3(0,0,90));
			Vector3 pos = transform.position;
			pos.x -= halfBlock;
			pos.y += halfBlock;
			clampAndMove(pos);
			orientation = (int)Orientation.Vertical;
		}
		else if(orientation == (int)Orientation.Vertical){
			transform.Rotate(new Vector3(0,0,-90));
			Vector3 pos = transform.position;
			pos.x += halfBlock;
			pos.y -= halfBlock;
			clampAndMove(pos);
			orientation = (int)Orientation.Horizontal;
		}
	}

	private void verticalPosition(float value){
		Vector3 pos = this.gameObject.transform.position;
		pos.y += value;
		clampAndMove(pos);
	}

	private void horizontalPosition(float value){
		Vector3 pos = this.gameObject.transform.position;
		pos.x += value;
		clampAndMove(pos);
	}

	public void clampAndMove(Vector3 newPos){

		if(orientation == (int)Orientation.Horizontal && numBlocks == 2){
			newPos.x = Mathf.Clamp(newPos.x, hori_limit2[1], hori_limit2[3]);
		}
		else if(orientation == (int)Orientation.Horizontal && numBlocks == 4){
			newPos.x = Mathf.Clamp(newPos.x, hori_limit4[1], hori_limit4[3]);
		}
		else if(orientation == (int)Orientation.Vertical && numBlocks == 2){
			newPos.x = Mathf.Clamp(newPos.x, vert_limit2[1], vert_limit2[3]);
		}
		else if(orientation == (int)Orientation.Vertical && numBlocks == 4){
			newPos.x = Mathf.Clamp(newPos.x, vert_limit4[1], vert_limit4[3]);
		}

		if(orientation == (int)Orientation.Horizontal && numBlocks == 2){
			newPos.y = Mathf.Clamp(newPos.y, hori_limit2[2], hori_limit2[0]);
		}
		else if(orientation == (int)Orientation.Horizontal && numBlocks == 4){
			newPos.y = Mathf.Clamp(newPos.y, hori_limit4[2], hori_limit4[0]);
		}
		else if(orientation == (int)Orientation.Vertical && numBlocks == 2){
			newPos.y = Mathf.Clamp(newPos.y, vert_limit2[2], vert_limit2[0]);
		}
		else if(orientation == (int)Orientation.Vertical && numBlocks == 4){
			newPos.y = Mathf.Clamp(newPos.y, vert_limit4[2], vert_limit4[0]);
		}

		transform.position = newPos;
	}

	public void testing(){
		Debug.Log("I should have focus " + hasFocus); 
	}
}
