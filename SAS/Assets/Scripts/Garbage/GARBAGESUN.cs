using UnityEngine;
using System.Collections;

public class GARBAGESUN : MonoBehaviour {

	public float rate = 0.5f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate(rate,0,0);
	}
}
