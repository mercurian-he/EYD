using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour {

	private float sensitivityY = 5f;
	
	private float minimumY = -60f;
	private float maximumY = 60f;

	private float rotationY = 0f;


	public float scaleSpeed = 10f;
	private float scale = 0f;
	private float scaleMax;


	// Use this for initialization
	void Start () {
		scaleMax = scaleSpeed * 4;
	}
	
	// Update is called once per frame
	void Update () {

		//look up and down
		rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
		rotationY = Mathf.Clamp (rotationY, minimumY, maximumY);

		transform.localEulerAngles = new Vector3(-rotationY, 0, 0);


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
