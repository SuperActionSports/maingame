using UnityEngine;
using System.Collections;

public class SetColorToParent : MonoBehaviour {
	
	private float t;
	// Use this for initialization
	void Start () {
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
	
	void ResetColor()
	{
		Renderer r = GetComponent<Renderer>();
		Renderer rp = transform.parent.GetComponent<Renderer>();
		r.material.color = Color.Lerp(Color.black,rp.material.color,t);
	}
}
