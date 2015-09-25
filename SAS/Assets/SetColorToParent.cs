using UnityEngine;
using System.Collections;

public class SetColorToParent : MonoBehaviour {

	
	Color color;
	public Color pColor;
	// Use this for initialization
	void Start () {
		UpdateColor();
	}
	
	// Update is called once per frame
	void Update () {
	UpdateColor();
	}
	
	void UpdateColor()
	{
		Renderer r = transform.parent.GetComponent<Renderer>();
		color = r.material.color;
		Renderer s = GetComponent<Renderer>();
		s.material.color = color;
	}
}
