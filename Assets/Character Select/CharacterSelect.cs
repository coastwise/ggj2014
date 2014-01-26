using UnityEngine;
using System.Collections;

public class CharacterSelect : MonoBehaviour {

	int p1char;
	float[] timers = {1,1,1,1};
	bool[]	canChange = {true,true,true,true};

	public int P1char {
		get {
			return p1char;
		}
		set {
			setp1char(value);
		}
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if(canChange[0]) {
			if (Input.GetAxis("L_XAxis_1") > 0) {
				
			} else if (Input.GetAxis("L_XAxis_1") < 0) {
				
			} 
		}

		if(canChange[1]) {
			if (Input.GetAxis("L_XAxis_2") > 0) {
				
			} else if (Input.GetAxis("L_XAxis_2") < 0) {
				
			} 
		}
		if(canChange[2]) {
			if (Input.GetAxis("L_XAxis_3") > 0) {
				
			} else if (Input.GetAxis("L_XAxis_3") < 0) {
				
			} 
		}
		if(canChange[3]) {
			if (Input.GetAxis("L_XAxis_4") > 0) {
				
			} else if (Input.GetAxis("L_XAxis_4") < 0) {
				
			} 
		}

	}

	void setp1char (int value) {

		//if (canChange) {
			   
		//} else {
		//	p1char += value;
		//}



	}
}
