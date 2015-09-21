using UnityEngine;
using System.Collections;

public class PlayerColorScript : MonoBehaviour {

	public Color c;
	private Renderer rend;
	// Use this for initialization
	void Start () {
		rend = GetComponent<Renderer>();
		Debug.Log("Hello, I'm " + c);
	}
	
	// Update is called once per frame
	void Update () {
		rend.material.color = c;
	}
}
