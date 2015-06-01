using UnityEngine;
using System.Collections;

public class LighterControl : MonoBehaviour {

	public static bool isUsed = false;
	private GameObject lighter;
	Component halo;
	private GameObject plane;

	private GameObject colliderObj = null;

	// Use this for initialization
	void Start () {
		lighter = GameObject.Find ("Lighter Using");
		halo = GameObject.Find ("Lighter Halo").GetComponent ("Halo");
		plane = GameObject.Find ("Collider Plane");
		isUsed = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (isUsed) {
			lighter.SetActive(true);
			plane.SetActive(true);
			
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast (ray, out hit, 150f)) {
				colliderObj = hit.collider.gameObject;
				if (colliderObj.name.Equals("Collider Plane")) {
					lighter.transform.position = hit.point;
					halo.GetType ().GetProperty ("enabled").SetValue (halo, false, null);
				}
				if (colliderObj.name.Equals("Hint_Paper")) {
					lighter.transform.position = hit.point;
					halo.GetType ().GetProperty ("enabled").SetValue (halo, true, null);
					if (Input.GetMouseButtonDown(0)){
						LevelControl1.state = LevelControl1.STATES.LighterUsed;
					}
				} else {
				}
			}

		} else {
			lighter.SetActive(false);
			plane.SetActive(false);
		}
	}
}
