using UnityEngine;
using System.Collections;



public class WaterballControl : MonoBehaviour {

	public float rotatespeed = 1;
	public GameObject waterball;
	public ParticleSystem rise_water;

	private float cur_r;
	public const float MAXRADIUS = 250.0f;
	public const float EXPANDSPEED = 1f;
	private short expand;	//expand or shrink

	public float m_SpeedU = -0.1f;
	public float m_SpeedV = -0.1f;

	// Use this for initialization
	void Start () {
		rise_water.enableEmission = false;
		waterball.SetActive (false);
		cur_r = MAXRADIUS;
		expand = -1;
	}
	
	// Update is called once per frame
	void Update () {

		// Activate expand state
		if (Input.GetKeyDown(KeyCode.A)) {
			Debug.Log("Input:A");
			float time = (waterball.transform.position.y-rise_water.transform.position.y) / rise_water.startSpeed;
			Invoke ("activeExpand",time);
			rise_water.enableEmission = true;
		}
		//Activate shrink state
		if (Input.GetKeyDown(KeyCode.S)) {
			Debug.Log("Input.S");
			expand = 0;
			rise_water.enableEmission = false;
		}

		//process
		if (expand > 0) {	//expand the ball
				if (cur_r < MAXRADIUS) {	//animation
					waterball.transform.localScale += new Vector3 (EXPANDSPEED, EXPANDSPEED, EXPANDSPEED);
					cur_r += EXPANDSPEED;
				}
				else{
					rise_water.enableEmission = false;
				}
		} else if(expand == 0) {	//shrink to nothing
				if (cur_r > 0 ) {			//animation
					waterball.transform.localScale -= new Vector3 (EXPANDSPEED, EXPANDSPEED, EXPANDSPEED);
					cur_r -= EXPANDSPEED;
				}
				else{
					waterball.SetActive (false);
					expand = -1;
				}
		}

		//Waterball Animation
		//waterball.transform.Rotate (new Vector3 (0, rotatespeed, 0));
		float newOffsetU = Time.time * m_SpeedU;
		float newOffsetV = Time.time * m_SpeedV;
		Renderer renderer;
		renderer = waterball.GetComponent <Renderer>();
		if (renderer) {
			renderer.material.mainTextureOffset = new Vector2(newOffsetU,newOffsetV);
		}

	}

	//Update the states to enable expand
	void activeExpand()
	{
		waterball.transform.localScale = new Vector3(0.0f,0.0f,0.0f);
		waterball.SetActive (true);
		cur_r = 0;
		expand = 1;
	}

}
