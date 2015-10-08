using UnityEngine;
using System.Collections;

public class GARBAGEMOVE : MonoBehaviour {
	
	private float myparents;
	private float hope;
	// Use this for initialization
	void Start () {
	myparents = 0;
	hope = -1;
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector3(transform.position.x -1f * Time.deltaTime,transform.position.y,transform.position.z);
	}
}
