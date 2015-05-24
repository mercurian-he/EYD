using UnityEngine;
using System.Collections;

public class GlobalManager : MonoBehaviour {

	public enum GAMESTATE {
		Start = 0,
		Level1 = 1,
		Level2 = 2,
		Level3 = 3,
		Level4 = 4,
		Finish = 5
	}

	public static GAMESTATE gameState;

	// Use this for initialization
	void Start () {
		print("Start");
	
	}
	
	// Update is called once per frame
	void Update () {
		print("Update");
		int l = (int)gameState;
		print (l);
		Application.LoadLevel(l);
	
	}
}

