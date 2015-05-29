using UnityEngine;
using System.Collections;

public class YellowAnim : MonoBehaviour {


	//Butterfly
	public float butt_wing_delta_theta = 0.05f;
	public GameObject butterfly;

	private int butt_count;
	private int butt_max = 30;
	private int butt_add;

	private Transform leftwing;
	private Transform rightwing;
	private Vector3 left_unit = new Vector3(0.362f,-0.925f,-0.110f);
	private Vector3 right_unit = new Vector3(-0.777f,0.594f,-0.203f);


	//Matis
	public GameObject mantis;
	public float matis_speed = 0.1f;
	
	private Transform head;
	private Transform headtip;
	private Camera cam;
	private float distance_2;
	private const float max_dist = 2500;

	//Beetle
	public GameObject beetle;
	public float beetle_speed;

	private int beetle_count;
	private int beetle_add;
	private int beetle_wait;
	private int beetle_shakecount;
	private const int shaketime = 2;	//shake 3 times
	private const int beetle_maxwait = 200;
	private const int beetle_maxcount = 7;

	// Use this for initialization
	void Start () {
		//Butterfly
		butt_count = 0;
		butt_add = 1;
		leftwing = butterfly.transform.FindChild("leftwing");
		rightwing = butterfly.transform.FindChild("rightwing");

		//Matis
		cam = Camera.main;
		head = mantis.transform.FindChild ("head");

		//Beatle
		beetle_shakecount = 0;
		beetle_count = 0;
		beetle_add = 1;
		beetle_wait = 0;
	}
	
	// Update is called once per frame
	void Update () {

		//butterfly
		leftwing.Rotate (left_unit * butt_wing_delta_theta * butt_add);
		rightwing.Rotate (right_unit * butt_wing_delta_theta * butt_add);
		if (butt_count == butt_max)
			butt_add = -1;
		if (butt_count == -butt_max)
			butt_add = 1;
		butt_count += butt_add;


		//matis
		Vector3 dest = cam.transform.position - head.transform.position;
		Vector3 newDir = Vector3.RotateTowards (head.forward, dest,matis_speed * Time.deltaTime,0.0f);
		//Debug.DrawRay (head.position, newDir, Color.red);
		head.rotation = Quaternion.LookRotation (newDir);

		//beetle
		if (beetle_wait == 50) {
			beetle.transform.Translate (new Vector3(0,0,beetle_speed*beetle_add));
			if(beetle_count == beetle_maxcount){
				beetle_add = -1;
			}
			if(beetle_count == -beetle_maxcount){
				beetle_add = 1;
				beetle_shakecount ++;
			}
			if(beetle_shakecount == 2){
				beetle_shakecount = 0;
				beetle_wait = 0;
			}
			beetle_count += beetle_add;
		} else {
			beetle_wait ++;
		}
	}
}
