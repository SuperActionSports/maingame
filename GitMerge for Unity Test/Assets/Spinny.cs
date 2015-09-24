using UnityEngine;
using System.Collections;

public class Spinny : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate(Time.deltaTime*50, 0, Time.deltaTime*-60, 0);
	}
}
