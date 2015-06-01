using UnityEngine;
using System.Collections;

public class PaperControl : MonoBehaviour {

	private ObjShelf objShelf;
	private GameObject paper;
	private GameObject colliderObj = null;

	private float scaleSpeed = 2f;
	private float scaleMin = 0f;
	private float scaleMax = 40f;

	public static bool inShelf = false;
	public static bool isOpen = true;
	public static bool isOpening = false;
	public static bool isClosing = false;

	// Use this for initialization
	void Start () {
		objShelf = (ObjShelf) GameObject.Find("Main Camera").GetComponent("ObjShelf");
		paper = GameObject.Find ("Hint_Paper");
	}
	
	// Update is called once per frame
	void Update () {

		
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit hit;
		if (Physics.Raycast (ray, out hit, 10f)) {
			colliderObj = hit.collider.gameObject;
			if (colliderObj.name.Equals("Hint_Paper")){
				ObjControl.selectingObj = true;
				if (Input.GetMouseButtonDown(0) && !LighterControl.isUsed){
					isClosing = true;
				}
			} 

		}

		if (isOpening) {
			openPaper();
		}
		if (isClosing) {
			closePaper();
			isOpen = false;
		}
	}

	void openPaper(){
		//paper.SetActive(true);
		float curScale = paper.transform.localScale.x;
		if (curScale < scaleMax) {
			curScale += scaleSpeed;
			if (curScale >= scaleMax){
				curScale = scaleMax;
				isOpening = false;
			}
		}
		paper.transform.localScale = new Vector3(curScale, curScale, curScale);


	}

	void closePaper() {
		float curScale = paper.transform.localScale.x;
		if (curScale > scaleMin) {
			curScale -= scaleSpeed;
			if (curScale <= scaleMin){
				//paper.SetActive(false);
				objShelf.closePaper();
				isClosing = false;
			}
		}
		paper.transform.localScale = new Vector3(curScale, curScale, curScale);

		if (!inShelf) {
			objShelf.addCollection ("paper"); 
			inShelf = true;
		}
	}
}
