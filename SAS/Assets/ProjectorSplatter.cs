using UnityEngine;
using System.Collections;

public class ProjectorSplatter : MonoBehaviour {

	public Color c;

	void Start () {
		c = Color.white;
	}

	// Update is called once per frame
	void Update () {
		GetComponent<Projector> ().material.color = c;
	}
}
