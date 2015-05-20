using UnityEngine;
using System.Collections;

public class LevelControl1 : MonoBehaviour {

	public enum STATES
	{
		Start = 0,
		YellowGet,
		GreenGet,
		BlueGet,
		RedGet,
		LighterGet,
		Finish
	};

	public static STATES state;

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

		/*collections*/
		redLens = GameObject.Find("red");
		blueLens = GameObject.Find("blue");
		greenLens = GameObject.Find("green");
		yellowLens = GameObject.Find("yellow");
		lighter = GameObject.Find("lighter");

		redLens.SetActive (false);
		blueLens.SetActive (false);
		greenLens.SetActive (false);
		lighter.SetActive (false);

		/*invisible objects*/
		redVisibles = GameObject.FindGameObjectsWithTag("Red Visible");
		blueVisibles = GameObject.FindGameObjectsWithTag("Blue Visible");
		greenVisibles = GameObject.FindGameObjectsWithTag("Green Visible");
		yellowVisibles = GameObject.FindGameObjectsWithTag("Yellow Visible");

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

		/*other relative objects*/
		beetle = GameObject.Find("beetle");

	}
	
	// Update is called once per frame
	void Update () {
		switch (state) {
		case STATES.YellowGet:
			greenLens.SetActive(true);
			foreach (GameObject go in yellowVisibles) {
				go.SetActive(true);
			}
			break;
		case STATES.GreenGet:
			blueLens.SetActive(true);
			foreach (GameObject go in greenVisibles){
				go.SetActive(true);
			}
			beetle.SetActive(false);
			break;
		case STATES.BlueGet:
			redLens.SetActive(true);
			foreach (GameObject go in blueVisibles){
				go.SetActive(true);
			}
			break;
		case STATES.RedGet:
			lighter.SetActive(true);
			foreach (GameObject go in redVisibles){
				go.SetActive(true);
			}
			break;
		case STATES.LighterGet:
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
}
