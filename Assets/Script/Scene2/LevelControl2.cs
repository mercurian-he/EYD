using UnityEngine;
using System.Collections;

public class LevelControl2 : MonoBehaviour {

	public enum STATES
	{
		Start = 0,
		Finish
	};

	public static STATES state;



	// Use this for initialization
	void Start () {
		state = STATES.Start;

	}
	
	// Update is called once per frame
	void Update () {
		switch (state) {
		case STATES.Finish:
			break;

		}
	}
}
