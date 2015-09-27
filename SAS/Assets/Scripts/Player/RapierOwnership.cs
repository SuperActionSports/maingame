using UnityEngine;
using System.Collections;

public class RapierOwnership : MonoBehaviour {

	public bool owned;
	public bool available;
	public bool hasHit;
	public GameObject parent;
	// Use this for initialization
	void Start () {
		hasHit = false;
		available = false;
		owned = true;
		Transform grand = transform.parent;
		while (grand.name != "Fencer")
		{
			grand = grand.transform.parent;
		}
		parent = grand.transform.gameObject;
		
	}
	
	// Update is called once per frame
	void Update () {
		available = !owned && hasHit;
	}
	
	public bool Available()
	{
		return !owned && hasHit && parent == null;
	}
	
	void OnTriggerEnter(Collider other)
	{
		if((other.CompareTag("Stage") || other.CompareTag("Player")) && other.gameObject != parent)
		{
			hasHit = true;
			GetComponent<EquipmentScript>().setArmed(false);
			GetComponent<Renderer>().material.color = Color.black;
			GetComponent<CapsuleCollider>().enabled = false;
		}
	}
	
	public void resetOwnership()
	{
		if (!owned && transform.parent != null)
		{
			owned = true;
			Transform grand = transform.parent;
			while (grand.name != "Fencer")
			{
				grand = grand.transform.parent;
			}
			parent = grand.transform.gameObject;
		}
		else
		parent = null;
		owned = false;
		//hasHit = true;
		GetComponent<SetColorToParent>().ResetColor();
	}
	
}
