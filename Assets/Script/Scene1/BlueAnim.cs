using UnityEngine;
using System.Collections;

public class BlueAnim : MonoBehaviour {
	public GameObject bluebird;
	public float rotate_speed = 0.02f;

	private Transform bird_transform;
	// Use this for initialization
	void Start () {
		bird_transform = bluebird.transform;
	}
	
	// Update is called once per frame
	void Update () {
		bluebird.transform.Rotate (new Vector3 (0, rotate_speed, 0));
	}
}
