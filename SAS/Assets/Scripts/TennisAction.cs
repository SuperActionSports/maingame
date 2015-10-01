using UnityEngine;
using System.Collections;

public class TennisAction : MonoBehaviour {

	public GameObject tennisBall;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Space)) {
			tennisBall.SetActive(true);
		}

	}
}
