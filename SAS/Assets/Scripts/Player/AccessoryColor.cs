using UnityEngine;
using System.Collections;

public class AccessoryColor : MonoBehaviour {

	private float t;
	private TrailRenderer tr;
	public Color color;
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
		if (transform.parent != null)
		{
			//Debug.Log("Trail has parent");
			Renderer rp = transform.parent.GetComponent<Renderer>();
			color = rp.material.color;
		}
		else{
			color = Color.gray;
		}
		r.material.color = color;
	}
	
	public void ResetColor(Color c)
	{
		Debug.Log(transform.gameObject.name + " is resetting color to " + c);
		Renderer r = tr.GetComponent<Renderer>();
		//r.material.color = Color.Lerp(Color.black,c,t);
		tr.material.color = c;
	}
}
