using UnityEngine;
using System.Collections;

public class WaterballControl : MonoBehaviour {
	public float rotatespeed = 1;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate (new Vector3 (0, rotatespeed, 0));
	}
}
