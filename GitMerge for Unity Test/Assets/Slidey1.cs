using UnityEngine;
using System.Collections;

public class Slidey1 : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    transform.position = new Vector3 (4*Mathf.Sin(Time.time*4), 5,7);
	}
}
