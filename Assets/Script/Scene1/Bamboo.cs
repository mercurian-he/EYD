using UnityEngine;
using System.Collections;

public class Bamboo : MonoBehaviour {
	public GameObject bamboo;
	public ParticleSystem parti;
	public Vector3 growth;
	public int growtime_stage1 = 500;
	public int growtime_total = 1000;

	private Transform bamboo_transform;
	private int growcount;
	private Vector3 growth2;
	// Use this for initialization
	void Start () {
		bamboo_transform = bamboo.transform;
		growth2 = new Vector3 (0, growth.y, 0);
		growcount = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (growcount < growtime_total) {
			if(growcount < growtime_stage1){
				bamboo_transform.localScale += growth;

			}
			else{
				bamboo_transform.localScale += growth2;
			}
			growcount++;
		}


	}
}
