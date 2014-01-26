using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//Do not let setup move on to game if beginProblem or endProblem is true
//Display a message if beginProblem or endProblem is true;
//reset endProblem and beginProblem to false when moving to game

public class Setup : Game 
{
	/*public static Accordian Instance {get; private set;} //This set works but method doesnt work
	
	void Awake()
	{
		Instance = this;	
	}*/
	private static Setup instance;
	
	public static Setup Instance
	{
		 get { return instance ?? (instance = new GameObject("Setup").AddComponent<Setup>()); }
    
	}

	private void OnGUI () 
	{

	}

}