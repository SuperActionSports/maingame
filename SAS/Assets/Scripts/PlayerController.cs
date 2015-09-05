using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public Color c1;
	private Color c2;
	private bool complete;
	private Renderer rend;
	private float colorLerpT;
	
	public KeyCode left;
	public KeyCode right;
	public KeyCode up;
	public KeyCode down;
	[Range(1,20)]
	public float speedMagnitude;
	// Use this for initialization
	void Start () {
		rend = GetComponent<Renderer>();
		//rend.material.color = c;
		speedMagnitude = 10f;
		complete = false;
		colorLerpT = 0;
	}
	
	// Update is called once per frame
	void Update () {
	Vector3 speed = new Vector3(0,0,0);
		if (Input.GetKey(left)) {
			speed.x -= speedMagnitude;
		}
		if (Input.GetKey(right)) {
			speed.x += speedMagnitude;
		}
		if (Input.GetKey(up)) {
			speed.y += speedMagnitude;
		}
		if (Input.GetKey(down)) {
			speed.y -= speedMagnitude;
		}
		transform.position += speed*Time.deltaTime;
		
		colorLerpT += Time.deltaTime;
		if (complete) {
			rend.material.color = Color.Lerp(c2,c1,colorLerpT);
			if (colorLerpT >= 1) {
				complete = false;
				colorLerpT = 0;
			}
		}
		else {
			rend.material.color = Color.Lerp(c1,c2,colorLerpT);
			if (colorLerpT >= 1) {
				complete = true;
				colorLerpT = 0;
			}
		}
	}
}
