using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour {

	private bool viewControlOn = true;

	private float mouseSensitivityY = 5f;
	
	private float minimumY = -60f;
	private float maximumY = 60f;

	private float rotationY = 0f;
	private float rotationBaseY = 0f;

	public float scaleSpeed = 10f;
	private float scale = 0f;
	private float scaleMax;

	private float shelfWidth;


	// Use this for initialization
	void Start () {
		scaleMax = scaleSpeed * 4;

		ObjShelf os = (ObjShelf) GameObject.Find("Main Camera").GetComponent("ObjShelf");
		shelfWidth = os.getShelfWidth ();
	}
	
	// Update is called once per frame
	void Update () {

		//bool movable = Input.mousePosition.x > shelfWidth || Input.mousePosition.x < 0;
		//if (!movable)
		//	return;

		//look up and down
		if (Input.GetKeyDown (KeyCode.Space)) {
			viewControlOn = !viewControlOn;
		}
		if (viewControlOn) {
			rotationY += Input.GetAxis ("Mouse Y") * mouseSensitivityY;
			rotationY = Mathf.Clamp (rotationY, minimumY, maximumY);
			transform.localEulerAngles = new Vector3 (-rotationY, 0, 0);
		}


		//scale up and down
		float scroll = Input.GetAxis("Mouse ScrollWheel") * scaleSpeed;
		float newScale = scale + scroll;
		if (newScale > 0 && newScale < scaleMax) {
			transform.Translate (Vector3.forward * scroll);
			scale = newScale;
		}

	}

	void OnGUI(){
		//GUI.Label (new Rect (10, 10, 100, 30), scale.ToString ());
	}

}
