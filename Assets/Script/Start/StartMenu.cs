using UnityEngine;
using System.Collections;

public class StartMenu : MonoBehaviour {

	
	int screenWidth = Screen.width;
	int screenHeight = Screen.height;
	int buttonWidth = 50;
	int buttonHeight = 30;

	public GUISkin startSkin;
	public Texture textureBG;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnGUI(){


		//GUI.skin = startSkin;
		//GUI.DrawTexture (new Rect (0, 0, screenWidth, screenHeight), textureBG);

		if (GUI.Button (new Rect (10, 10, buttonWidth, buttonHeight), "Start")) {
			print("Start is pressed");
			//GameControl.gameState = GameControl.GAMESTATE.Level1;
			//int l = (int) GameControl.GAMESTATE.Level1;
			Application.LoadLevel(1);
		}


	}
}
