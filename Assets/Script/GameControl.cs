using UnityEngine;
using System.Collections;

public class GameControl : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnGUI(){
		GUI.skin.button.fontSize = 20;
		if (GUI.Button (new Rect (Screen.width - 100, Screen.height - 50, 100, 50), "Quit")) {
			Application.Quit();
		}
	}
}
