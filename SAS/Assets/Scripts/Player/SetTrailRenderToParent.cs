using UnityEngine;
using System.Collections;

public class SetTrailRenderToParent : MonoBehaviour {

	private float t;
	private TrailRenderer tr;
	// Use this for initialization
	void Start () {
		tr = GetComponent<TrailRenderer>();
		ResetColor();
	}
	
	// Update is called once per frame
	void Update () {
		if (t < 1)
		{
			t+=Time.deltaTime;
			ResetColor();
		}
	}
	
	public void ResetColor()
	{
		Renderer r = GetComponent<Renderer>();
		tr.material.color = r.material.color;
	}
}
