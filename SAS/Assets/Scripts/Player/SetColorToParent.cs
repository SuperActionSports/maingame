using UnityEngine;
using System.Collections;

public class SetColorToParent : MonoBehaviour {
	
	private float t;
	public Color trc;
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
	
	public void ResetColor()
	{
		Renderer r = GetComponent<Renderer>();
		if (transform.parent != null)
		{
			
			Renderer rp = transform.parent.GetComponent<Renderer>();
			//r.material.color = Color.Lerp(Color.black,rp.material.color,t);
			r.material.color = rp.material.color;
		}
		else{
			//r.material.color = Color.Lerp(r.material.color,Color.black,t);
			r.material.color = Color.black;
		}
		TrailRenderer tr = GetComponent<TrailRenderer>();
		if (tr != null) 
		{
			//Debug.Log("Trail is now " + r.material.color); 
			tr.material.color = r.material.color;
			trc = tr.material.color;
		}
	}
	
	public void ResetColor(Color c)
	{
		Renderer r = GetComponent<Renderer>();
		//r.material.color = Color.Lerp(Color.black,c,t);
		r.material.color = c;
	}
}
