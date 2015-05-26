using UnityEngine;
using System.Collections;

public class ObjControl : MonoBehaviour {

	private bool viewControlOn = true;

	private float sensitivityX = 2f;
	public float translateSpeed = 0.8f;
	public float baseHeight = 120f;

	private float jolt = 0f;
	private float joltMax;
	private float joltDelta;

	private float shelfWidth;

	// Use this for initialization
	void Start () {
		joltMax = translateSpeed * 2f;
		joltDelta = joltMax / 10f;

		ObjShelf os = (ObjShelf) GameObject.Find("Main Camera").GetComponent("ObjShelf");
		shelfWidth = os.getShelfWidth ();
	}
	
	// Update is called once per frame
	void Update () {
		
		bool movable = Input.mousePosition.x > shelfWidth || Input.mousePosition.x < 0;
		if (!movable)
			return;

		//rotation
		float rotationX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivityX;
		if (Input.GetKeyDown (KeyCode.Space)) {
			viewControlOn = !viewControlOn;
		}
		if (viewControlOn) {
			transform.localEulerAngles = new Vector3 (0, rotationX, 0);
		}

		//move
		if (Input.GetMouseButton (0)) {

			//walk forward
			transform.Translate(Vector3.forward * translateSpeed);

			//jolt
			jolt += joltDelta;
			if (jolt > joltMax || jolt < -joltMax) joltDelta = -joltDelta;

			//terrian
			Vector3 newPosition = transform.position;
			float x = newPosition.x;
			float y = newPosition.y;
			float terrianHeight = 40;
			if (x >=0 && x < 500 && y <0 && y > -500)
				terrianHeight = Terrain.activeTerrains[0].SampleHeight(newPosition);
			if (x <0 && x > -500 && y <0 && y > -500)
				terrianHeight = Terrain.activeTerrains[1].SampleHeight(newPosition);
			if (x >=0 && x < 500 && y >=0 && y < 500)
				terrianHeight = Terrain.activeTerrains[2].SampleHeight(newPosition);
			if (x <0 && x > -500 && y >=0 && y < 500)
				terrianHeight = Terrain.activeTerrains[3].SampleHeight(newPosition);

			newPosition.y = baseHeight + jolt + terrianHeight;
			transform.position = newPosition;

		}
	}

	void OnGUI(){
		//GUI.Label (new Rect (10, 10, 100, 30), Terrain.activeTerrain.SampleHeight (transform.position).ToString ());
		//GUI.Label (new Rect (40, 10, 100, 30), shelfWidth.ToString());
		//GUI.Label (new Rect (40, 40, 100, 30), Input.mousePosition.x.ToString());

	}
}
