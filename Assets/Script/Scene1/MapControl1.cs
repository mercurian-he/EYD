using UnityEngine;
using System.Collections;

public class MapControl1 : MonoBehaviour {

	public Texture map;
	public Texture point;

	private float mapSize;
	private float pointSize;
	private float pointSizeHalf;
	private float terrianSize = 1000;
	private float terrianSizeHalf = 500;
	private float ratio;

	private float mapStartX;
	private float mapStartY;

	private Vector3 position;
	private Vector2 pointPosition;

	// Use this for initialization
	void Start () {
				
		mapSize = Screen.width * 0.15f;

		mapStartX = Screen.width - mapSize;
		mapStartY = 0;

		pointSize = mapSize * 0.075f;
		pointSizeHalf = pointSize / 2f;
		ratio = mapSize / terrianSize;
	}
	
	// Update is called once per fram
	void Update () {
		position = transform.position;
		pointPosition.x = mapStartX + (position.z + terrianSizeHalf) * ratio;
		pointPosition.y = mapStartY + (position.x + terrianSizeHalf) * ratio;
		print (pointPosition);

	}

	void OnGUI(){
		GUI.Label (new Rect (Screen.width - mapSize, 0, mapSize, mapSize), map);
		GUI.Label (new Rect (pointPosition.x - pointSizeHalf, pointPosition.y - pointSizeHalf, pointSize, pointSize), point);

	}
}
