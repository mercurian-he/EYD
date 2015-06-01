using UnityEngine;
using System.Collections;

public class PlayerControl11 : MonoBehaviour {

	public static bool selectingObj = false;
	
	private CharacterController controller;
	
	private bool viewControlOn = true;
	
	private float mouseSensitivityX = 2f;
	private float keySensitivityX = 2f;
	
	public float translateSpeed = 0.8f;
	public float baseHeight = 20f;
	
	private float jolt = 0f;
	private float joltMax;
	private float joltDelta;
	
	private float shelfWidth;
	
	// Use this for initialization
	void Start () {
		
		controller = GetComponent<CharacterController> ();
		
		joltMax = translateSpeed * 2f;
		joltDelta = joltMax / 10f;

		shelfWidth = ObjShelf1.getShelfWidth ();
	}
	
	// Update is called once per frame
	void Update () {
		
		//rotation
		viewControlOn = true;
		if (ObjShelf1.shelfOpen || MissingPieceControl1.isOpen) {
			viewControlOn = false;
		}
		float rotationX = transform.localEulerAngles.y;
		//mouse
		if (viewControlOn) {
			rotationX += Input.GetAxis("Mouse X") * mouseSensitivityX;
		}
		//keyboard
		if (Input.GetKey (KeyCode.LeftArrow)) {
			rotationX += -keySensitivityX;
		}
		if (Input.GetKey (KeyCode.RightArrow)) {
			rotationX += keySensitivityX;
		}
		transform.localEulerAngles = new Vector3 (0, rotationX, 0);
		
		
		
		//move
		
		if (selectingObj) {
			if (Input.GetMouseButtonDown(0)){
				selectingObj = false;
			}
			return;
		}
		
		bool movable = Input.mousePosition.x > shelfWidth || Input.mousePosition.x < 0;
		if (Input.GetMouseButton(0) && ObjShelf1.shelfOpen && !movable)
			return;
		
		if (Input.GetMouseButton (0) || Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow)) {
			
			//walk forward
			if (Input.GetKey(KeyCode.DownArrow)){
				//transform.Translate(-Vector3.forward * translateSpeed);
				Vector3 moveDirection = transform.TransformDirection(-Vector3.forward);
				controller.Move(moveDirection * translateSpeed);
				
			} else {
				//transform.Translate(Vector3.forward * translateSpeed);
				Vector3 moveDirection = transform.TransformDirection(Vector3.forward);
				controller.Move(moveDirection * translateSpeed);
			}
			
			//jolt
			jolt += joltDelta;
			if (jolt > joltMax || jolt < -joltMax) joltDelta = -joltDelta;
			
			//terrian
			Vector3 newPosition = transform.position;
			float x = newPosition.x;
			float y = newPosition.y;
			float terrianHeight = 40;
			terrianHeight = Terrain.activeTerrain.SampleHeight(newPosition);
			
			newPosition.y = baseHeight + jolt + terrianHeight;
			transform.position = newPosition;
			
		}
	}
}
