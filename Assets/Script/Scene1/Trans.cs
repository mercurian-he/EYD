using UnityEngine;
using System.Collections;

public class Trans : MonoBehaviour {

	public ParticleSystem gate_dest;
	public GameObject player;
	public Quaternion cam_rotation;
	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnTriggerEnter(Collider col){
		if (col.gameObject.name == "Player") {
			player.transform.position = gate_dest.transform.position;
			player.transform.rotation = Quaternion.Slerp (transform.rotation,cam_rotation,Time.deltaTime);
		}  
	}
}
