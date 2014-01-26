using UnityEngine;
using System.Collections;

public class EndGame : MonoBehaviour {
	private static EndGame instance;
	
	public static EndGame Instance
	{
		get { return instance ?? (instance = new GameObject("EndGame").AddComponent<EndGame>()); }
		
	}
	
}


