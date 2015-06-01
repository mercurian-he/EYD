using UnityEngine;
using System.Collections;

public class MissingPieceControl1 : MonoBehaviour {

	private GameObject missingPiece;
	private GameObject colliderObj = null;

	private float scaleSpeed = 0.1f;
	private float scaleMin = 0f;
	private float scaleMax = 10f;

	public static bool isOpen = true;
	private bool isClosing = false;


	// Use this for initialization
	void Start () {
		missingPiece = GameObject.Find ("Missing_Piece1");
		isOpen = true;
	}
	
	// Update is called once per frame
	void Update () {
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit hit;
		if (Physics.Raycast (ray, out hit, 10f)) {
			colliderObj = hit.collider.gameObject;
			if (colliderObj.name.Equals("Missing_Piece1")){
				PlayerControl11.selectingObj = true;
				if (Input.GetMouseButtonDown(0)){
					isClosing = true;
				}
			} 
		}
		if (isClosing) {
			getPiece();
		}

	}

	void getPiece(){
		float curScale = missingPiece.transform.localScale.x;
		if (curScale > scaleMin) {
			curScale -= scaleSpeed;
			if (curScale <= scaleMin){
				isClosing = false;
				isOpen = false;
				ObjShelf1.addCollection ("missingPiece"); 
				Destroy(missingPiece);
			}
		}
		missingPiece.transform.localScale = new Vector3(curScale, curScale, curScale);


	}
}
