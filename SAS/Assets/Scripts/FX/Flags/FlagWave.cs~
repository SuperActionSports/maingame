using UnityEngine;
using System.Collections;

public class FlagWave : MonoBehaviour {

	public float range;
	private Vector3 startingPos;
	public float speed = 1;
	// Use this for initialization
	void Start () {
		startingPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector3(startingPos.x + range * Mathf.Sin(Time.time * speed),startingPos.y, startingPos.z);	
	}
}
