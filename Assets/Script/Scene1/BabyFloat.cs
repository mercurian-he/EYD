using UnityEngine;
using System.Collections;

public class BabyFloat : MonoBehaviour {

	public GameObject baby;
	public float floatspeed = 0.05f;

	private int count;
	private int add;
	private const int max = 20;
	// Use this for initialization
	void Start () {
		count = 0;
		add = 1;
	}
	
	// Update is called once per frame
	void Update () {
		baby.transform.Translate (new Vector3 (0,0,floatspeed * add)); //don't know why it becomes the z axis
		if (count == max)	//4 times
			add = -1;
		if (count == -max)
			add = 1;
		count += add;
	}
}
