using UnityEngine;
using System.Collections;

public class SetColorToParent : MonoBehaviour {
	
	private float t;
	public Color trc;
	public Color c;
	// Use this for initialization
	void Start () {
	//ResetColor();
	}
	
	// Update is called once per frame
	void Update () {
	}
	
	public void ResetColor()
	{
		Renderer r = GetComponent<Renderer>();
		if (transform.parent != null)
		{	
			Renderer rp = transform.parent.GetComponent<Renderer>();
			r.material.color = rp.material.color;
		}
		else{
			r.material.color = Color.black;
		}
		TrailRenderer tr = GetComponent<TrailRenderer>();
		if (tr != null) 
		{
			tr.material.color = r.material.color;
			trc = tr.material.color;
		}
	}
	
	public void ResetColor(Color c)
	{
		Debug.Log(gameObject.name + " changing my color to " + c);
		Renderer r = GetComponent<Renderer>();
		r.material.color = c;
		this.c = c;
	}
}
