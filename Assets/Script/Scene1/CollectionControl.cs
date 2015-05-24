﻿using UnityEngine;
using System.Collections;

public class CollectionControl : MonoBehaviour {

	private ObjShelf objShelf;
	private GameObject colliderObj = null;
	Component halo = null;
	
	// Use this for initialization
	void Start () {
		objShelf = (ObjShelf) GameObject.Find("Main Camera").GetComponent("ObjShelf");
	}


	// Update is called once per frame
	void Update () {
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit hit;
		bool flag = true;
		if (Physics.Raycast (ray, out hit, 150f)) {
			colliderObj = hit.collider.gameObject;
			if (colliderObj.tag.Equals ("Collections")) {
				halo = colliderObj.GetComponent ("Halo");
				halo.GetType ().GetProperty ("enabled").SetValue (halo, true, null);
				if (Input.GetMouseButton (1)) {
					objShelf.addCollection (colliderObj.name); 
					Destroy (colliderObj);
				}
			} else
				flag = false;
		} else
			flag = false;
		if (!flag){
			if (halo != null) {
				halo.GetType ().GetProperty ("enabled").SetValue (halo, false, null);
				halo = null;
			}
			colliderObj = null;
		}
	}

	void OnGUI(){
		GUI.color = Color.white;
		GUI.skin.label.fontSize = 30;
		if (colliderObj != null && colliderObj.tag.Equals ("Collections")) {
			///GUI.Label (new Rect (Screen.width - 110, 0, 100, 50), colliderObj.name);
			//GUI.Label (new Rect (Screen.width - 110, 60, 100, 50), colliderObj.tag);
		}
	}
}
