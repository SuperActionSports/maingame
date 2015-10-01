using UnityEngine;
using System.Collections;

public class ShieldScript : MonoBehaviour {

	Collider collide;
	// Use this for initialization
	void Start () {
	collide = GetComponent<Collider>();
	Deactivate();
	}
	
	public void Activate()
	{
		collide.enabled = true;
	}	
	
	public void Deactivate()
	{
		collide.enabled = false;
	}
}
