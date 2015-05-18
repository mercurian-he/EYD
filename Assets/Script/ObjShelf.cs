using UnityEngine;
using System.Collections;

public class ObjShelf : MonoBehaviour {

	private const float shelfHeight = 2484f;
	private const float shelfWidth = 802f;
	private const float heightTop = 28f;
	private const float heightBottom = 66f;
	private const float heightEmpty = 478f;

	private float ratio;

	private float top;
	private float height;
	private float width;
	private float heightTopRatio;
	private float heightBottomRatio;
	private float heightEmptyRatio;


	public GUISkin skin;
	int objNum = 0;
	int objStart = 0;
	string[] objNames = new string[10];
	Texture[] textures = new Texture[10];
	public Texture textureTop;
	public Texture textureBottom;
	public Texture textureEmpty;
	public Texture textureBlueLens;
	public Texture textureGreenLens;
	public Texture textureRedLens;
	public Texture textureYellowLens;
	public Texture textureLighter;

	// Use this for initialization
	void Start () {

		top = Screen.height * 0.05f;
		height = Screen.height * 0.9f;
		ratio = height / shelfHeight;
		width = ratio * shelfWidth;
		heightTopRatio = ratio * heightTop;
		heightBottomRatio = ratio * heightBottom;
		heightEmptyRatio = ratio * heightEmpty;

		//textures
		for (int i = 0; i < 10; i++)
			objNames [i] = "";
	}


	
	// Update is called once per frame
	void Update () {
		for (int i = 0; i < 5; i++) {
			textures[i] = getTextureByName(objNames[objStart + i]);
		}
	}

	void OnGUI () {


		GUI.skin = skin;
		GUI.DrawTexture (new Rect (0, top, width, heightTopRatio), textureTop);
		GUI.DrawTexture (new Rect (0, top + heightTopRatio, width, heightEmptyRatio), textures[0]);
		GUI.DrawTexture (new Rect (0, top + heightTopRatio + heightEmptyRatio, width, heightEmptyRatio), textures[1]);
		GUI.DrawTexture (new Rect (0, top + heightTopRatio + heightEmptyRatio * 2, width, heightEmptyRatio), textures[2]);
		GUI.DrawTexture (new Rect (0, top + heightTopRatio + heightEmptyRatio * 3, width, heightEmptyRatio), textures[3]);
		GUI.DrawTexture (new Rect (0, top + heightTopRatio + heightEmptyRatio * 4, width, heightEmptyRatio), textures[4]);
		GUI.DrawTexture (new Rect (0, top + heightTopRatio + heightEmptyRatio * 5, width, heightBottomRatio), textureBottom);


		GUI.color = Color.white;
		GUI.skin.button.fontSize = 30;
		if (GUI.Button (new Rect (0, 0, width, top), "▲")) {
			if (objStart > 0) objStart--;
		}
		if (GUI.Button (new Rect (0, Screen.height - top, width, top), "▼")) {
			if (objStart < 5) objStart++;
		}

		//GUI.Label (new Rect (0, 0, 40, 30), Screen.height.ToString ());
		//GUI.Label (new Rect (0, 40, 40, 30), Screen.width.ToString ());
		//GUI.Label (new Rect (0, 80, 40, 30), ratio.ToString ());

	}


	public void addCollection(string name){
		if (objNum < 10) {
			objNames[objNum] = name;
			objNum++;
		}
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
		
		return textureEmpty;
	}
}
