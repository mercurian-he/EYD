﻿using UnityEngine;
using System.Collections;

public class ObjShelf1 : MonoBehaviour {

	/*shelf*/
	private const float shelfHeight = 2484f;
	private const float shelfWidth = 802f;
	private const float heightTop = 28f;
	private const float heightBottom = 66f;
	private const float heightEmpty = 478f;

	private float moveSpeed = 5.0f;

	public static bool shelfOpen = false;
	private float shelfPosition;

	private float ratio;

	private float top;
	private float height;
	private float width;
	private float heightTopRatio;
	private float heightBottomRatio;
	private float heightEmptyRatio;

	public static float widthAccess;

	/*mouse*/
	private int mousePosition;
	public static int isUsed;	//start from 1, 0 is not used
	public static int paperPosition;

	/*textures*/
	public GUISkin skin;
	public static int objNum = 0;
	public static int objStart = 0;
	public static string[] objNames = new string[10];
	Texture[] textures = new Texture[10];
	public Texture textureTop;
	public Texture textureBottom;
	public Texture textureEmpty;
	public Texture textureBlueLens;
	public Texture textureGreenLens;
	public Texture textureRedLens;
	public Texture textureYellowLens;
	public Texture textureLighter;
	public Texture texturePaper;
	public Texture textureMP;


	void Awake(){
		
		top = Screen.height * 0.05f;
		height = Screen.height * 0.9f;
		ratio = height / shelfHeight;
		width = ratio * shelfWidth;
		heightTopRatio = ratio * heightTop;
		heightBottomRatio = ratio * heightBottom;
		heightEmptyRatio = ratio * heightEmpty;

		widthAccess = width;

		shelfOpen = false;
		shelfPosition = -width;

	}

	// Use this for initialization
	void Start () {
		
		//use
		mousePosition = 0;
		isUsed = 0;

		if (Application.loadedLevel == 1) {
			//textures
			for (int i = 0; i < 10; i++)
				objNames [i] = "";
		}
	}


	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown (KeyCode.Space)) {
			shelfOpen = !shelfOpen;
		}
		if (shelfOpen && shelfPosition < 0) {
			shelfPosition += moveSpeed;
			if (shelfPosition > 0) shelfPosition = 0f;
		}
		if (!shelfOpen && shelfPosition > -width) {
			shelfPosition -= moveSpeed;
			if (shelfPosition < -width) shelfPosition = -width;
		}

		for (int i = 0; i < 5; i++) {
			textures[i] = getTextureByName(objNames[objStart + i]);
		}

		
		if (!shelfOpen) {
			return;
		}

		mousePosition = 0;
		float mouseX = Input.mousePosition.x;
		float mouseY = Screen.height - Input.mousePosition.y;
		if (0 < mouseX && mouseX < width && top + heightTopRatio < mouseY && mouseY < top + heightTopRatio + heightEmptyRatio * 5) {
			mousePosition = (int) Mathf.Floor((mouseY - top - heightTopRatio) / heightEmptyRatio) + 1;
			if (Input.GetMouseButtonDown(0)){
				PlayerControl1.selectingObj = true;
				PlayerControl11.selectingObj = true;
				string objName = objNames[objStart + mousePosition - 1];
				if (objName.Equals("paper")){
					if (paperPosition > 0){
						PaperControl.isClosing = true;
						PaperControl.isOpen = false;
						paperPosition = 0;
					}else {
						PaperControl.isOpening = true;
						PaperControl.isOpen = true;
						paperPosition = objStart + mousePosition;
					}
				} else if (!objName.Equals("")){
					//print ("use");
					if (isUsed == objStart + mousePosition){
						isUsed = 0;
					} else {
						isUsed = objStart + mousePosition;
					}
				} else {
					//print ("not use");
					isUsed = 0;
				}
			}
		}
		
		if (Application.loadedLevel == 1) {
			if (isUsed > 0) {
				string name = objNames [isUsed - 1];

				LevelControl1.STATES state = LevelControl1.STATES.Start;
				if (name.Equals ("red")) {
					state = LevelControl1.STATES.RedUsed;
				}
				if (name.Equals ("green")) {
					state = LevelControl1.STATES.GreenUsed;
				}
				if (name.Equals ("blue")) {
					state = LevelControl1.STATES.BlueUsed;
				}
				if (name.Equals ("yellow")) {
					state = LevelControl1.STATES.YellowUsed;
				}
				if (name.Equals ("lighter")) {
					state = LevelControl1.STATES.LighterPicked;
				}
				if (LevelControl1.state == LevelControl1.STATES.LighterUsed){
					state = LevelControl1.STATES.LighterUsed;
				}

				LevelControl1.state = state;

			} else {
				LevelControl1.state = LevelControl1.STATES.Start;
			}
		}

		if (Application.loadedLevel == 2) {

		}
	}

	void OnGUI () {

		if (shelfPosition < -width)
			return;

		GUI.skin = skin;

		GUI.DrawTexture (new Rect (shelfPosition, top, width, heightTopRatio), textureTop);
		GUI.DrawTexture (new Rect (shelfPosition, top + heightTopRatio, width, heightEmptyRatio), textures[0]);
		GUI.DrawTexture (new Rect (shelfPosition, top + heightTopRatio + heightEmptyRatio, width, heightEmptyRatio), textures[1]);
		GUI.DrawTexture (new Rect (shelfPosition, top + heightTopRatio + heightEmptyRatio * 2, width, heightEmptyRatio), textures[2]);
		GUI.DrawTexture (new Rect (shelfPosition, top + heightTopRatio + heightEmptyRatio * 3, width, heightEmptyRatio), textures[3]);
		GUI.DrawTexture (new Rect (shelfPosition, top + heightTopRatio + heightEmptyRatio * 4, width, heightEmptyRatio), textures[4]);
		GUI.DrawTexture (new Rect (shelfPosition, top + heightTopRatio + heightEmptyRatio * 5, width, heightBottomRatio), textureBottom);

		
		//GUI.backgroundColor = Color.blue;
		if (isUsed == 0 && mousePosition > 0) {
			GUI.Box (new Rect (shelfPosition, top + heightTopRatio + heightEmptyRatio * (mousePosition - 1), width, heightEmptyRatio), "");
		}
		if (isUsed > 0 && objStart < isUsed && isUsed < objStart + 6) {
			GUI.Box (new Rect (shelfPosition, top + heightTopRatio + heightEmptyRatio * (isUsed - 1 - objStart), width, heightEmptyRatio), "");
		}
		if (paperPosition > 0 && objStart < paperPosition && paperPosition < objStart + 6) {
			GUI.Box (new Rect (shelfPosition, top + heightTopRatio + heightEmptyRatio * (paperPosition - 1 - objStart), width, heightEmptyRatio), "");
		}

		GUI.color = Color.white;
		GUI.skin.button.fontSize = 30;
		if (GUI.Button (new Rect (shelfPosition, 0, width, top), "▲")) {
			if (objStart > 0) objStart--;
		}
		if (GUI.Button (new Rect (shelfPosition, Screen.height - top, width, top), "▼")) {
			if (objStart < 5) objStart++;
		}

		//GUI.Label (new Rect (0, 0, 40, 30), Screen.height.ToString ());
		//GUI.Label (new Rect (0, 40, 40, 30), Screen.width.ToString ());
		//GUI.Label (new Rect (0, 80, 40, 30), ratio.ToString ());

	}


	public static void addCollection(string name){
		if (objNum < 10) {
			objNames[objNum] = name;
			objNum++;
		}
	}
	public static void deleteCollection(string name){
		int i;
		for (i = 0; i < 10; i++) {
			if (objNames[i].Equals(name))
				break;
		}
		if (isUsed == i + 1)
			isUsed = 0;
		if (paperPosition == i + 1)
			paperPosition = 0;
		if (i == 10)
			return;
		for (; i < 9; i++) {
			objNames[i] = objNames[i + 1];
		}
		objNum--;
	}
	public static void closePaper(){
		paperPosition = 0;
	}
	public static float getShelfWidth(){
		return widthAccess;
	}

	
	Texture getTextureByName(string name){

		if (name.Equals (""))
			return textureEmpty;
		if (name.Equals ("red"))
			return textureRedLens;
		if (name.Equals ("green"))
			return textureGreenLens;
		if (name.Equals ("blue"))
			return textureBlueLens;
		if (name.Equals ("yellow"))
			return textureYellowLens;
		if (name.Equals ("lighter"))
			return textureLighter;
		if (name.Equals ("paper"))
			return texturePaper;
		if (name.Equals ("missingPiece"))
			return textureMP;
		
		return textureEmpty;
	}
}

