using UnityEngine;
using System.Collections;

public class LevelControl1 : MonoBehaviour {

	public enum STATES
	{
		Start = 0,
		YellowUsed,
		GreenUsed,
		BlueUsed,
		RedUsed,
		LighterUsed,
		Finish
	};

	public static STATES state;

	private GameObject directionalLight;
	private Color lightColor;

	private GameObject redLens;
	private GameObject blueLens;
	private GameObject greenLens;
	private GameObject yellowLens;
	private GameObject lighter;

	private GameObject[] redVisibles;
	private GameObject[] blueVisibles;
	private GameObject[] greenVisibles;
	private GameObject[] yellowVisibles;

	/*other relative objects*/
	private GameObject beetle;


	// Use this for initialization
	void Start () {
		state = STATES.Start;

		directionalLight = GameObject.Find("Directional Light");
		lightColor = directionalLight.GetComponent<Light> ().color;

		/*collections*/
		redLens = GameObject.Find("red");
		blueLens = GameObject.Find("blue");
		greenLens = GameObject.Find("green");
		yellowLens = GameObject.Find("yellow");
		lighter = GameObject.Find("lighter");

		/*invisible objects*/
		redVisibles = GameObject.FindGameObjectsWithTag("Red Visible");
		blueVisibles = GameObject.FindGameObjectsWithTag("Blue Visible");
		greenVisibles = GameObject.FindGameObjectsWithTag("Green Visible");
		yellowVisibles = GameObject.FindGameObjectsWithTag("Yellow Visible");


		beetle = GameObject.Find("beetle");


		initObjs ();

	}
	
	// Update is called once per frame
	void Update () {

		initObjs ();

		switch (state) {
		case STATES.Start:
			if (yellowLens){
				yellowLens.SetActive(true);
			}
			break;
		case STATES.YellowUsed:
			directionalLight.GetComponent<Light>().color = Color.yellow;
			if (greenLens){
				greenLens.SetActive(true);
			}
			foreach (GameObject go in yellowVisibles) {
				go.SetActive(true);
			}
			break;
		case STATES.GreenUsed:
			directionalLight.GetComponent<Light>().color = Color.green;
			if (blueLens){
				blueLens.SetActive(true);
			}
			foreach (GameObject go in greenVisibles){
				go.SetActive(true);
			}
			beetle.SetActive(false);
			break;
		case STATES.BlueUsed:
			directionalLight.GetComponent<Light>().color = Color.blue;
			if (redLens){
				redLens.SetActive(true);
			}
			foreach (GameObject go in blueVisibles){
				go.SetActive(true);
			}
			break;
		case STATES.RedUsed:
			directionalLight.GetComponent<Light>().color = Color.red;
			if (lighter){
				lighter.SetActive(true);
			}
			foreach (GameObject go in redVisibles){
				go.SetActive(true);
			}
			break;
		case STATES.LighterUsed:
			break;
		case STATES.Finish:
			break;

		}
	}

	void OnGUI(){
		if (GUI.Button (new Rect(Screen.width - 210, Screen.height - 40, 200, 30), "Return to Menu")){
			Application.LoadLevel(0);
		}
	}

	void initObjs(){

		directionalLight.GetComponent<Light> ().color = lightColor;
		
		if (yellowLens){
			yellowLens.SetActive(false);
		}
		if (greenLens){
			greenLens.SetActive(false);
		}
		if (blueLens){
			blueLens.SetActive(false);
		}
		if (redLens){
			redLens.SetActive(false);
		}
		if (lighter){
			lighter.SetActive(false);
		}

		
		foreach (GameObject go in redVisibles) {
			go.SetActive(false);
		}
		foreach (GameObject go in blueVisibles) {
			go.SetActive(false);
		}
		foreach (GameObject go in greenVisibles) {
			go.SetActive(false);
		}
		foreach (GameObject go in yellowVisibles) {
			go.SetActive(false);
		}



	}
}
