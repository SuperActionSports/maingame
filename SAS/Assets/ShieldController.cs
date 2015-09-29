using UnityEngine;
using System.Collections;

public class ShieldController : MonoBehaviour {

	Collider shield;
	GameObject ownedRapier;
	// Use this for initialization
	void Start () {
		shield = GetComponent<Collider>();
		ownedRapier = shield.transform.parent.FindChild("RapierHand/Rapier").gameObject;
		if (ownedRapier == null)
		{
			Debug.Log("I don't see any rapier!");
		}
	}
	
	public void Activate()
	{
		Debug.Log("SHIELDS ON");
		shield.enabled = true;
	}
	
	public void Deactivate()
	{
		Debug.Log("SHIELDS OFF");
		shield.enabled = false;
	}
	
	void OnTriggerEnter(Collider other)
	{
		if (other.transform.gameObject != ownedRapier && shield.enabled && other.CompareTag("Equipment"))
		{
			other.GetComponent<RapierScript>().Stop ();
			Deactivate();
		}
		
	}
}
