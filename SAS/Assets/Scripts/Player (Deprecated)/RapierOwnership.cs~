using UnityEngine;
using System.Collections;

public class RapierOwnership : MonoBehaviour {

	public bool owned;
	public bool available;
	public bool hasHit;
	public GameObject parent;
	private SphereCollider pickUpCollider;
	private SetColorToParent colorScript;
	AudioSource sound;
	// Use this for initialization
	void Start () {
		sound = GetComponentInParent<AudioSource>();
		hasHit = false;
		available = false;
		owned = true;
		Transform grand = transform.parent;
		while ((grand != null) && grand.name != "Fencer")
		{
			grand = grand.transform.parent;
		}
		parent = grand.transform.gameObject;
		pickUpCollider = GetComponent<SphereCollider>();
		pickUpCollider.enabled = false;
		colorScript = GetComponent<SetColorToParent>();
	}
	
	// Update is called once per frame
	void Update () {
		available = !owned && hasHit;
	}
	
	public void setArmed(bool armed)
	{
		GetComponent<CapsuleCollider>().enabled = armed;
	}
	
	public bool Available()
	{
		return !owned && hasHit;
	}
	
	void OnTriggerEnter(Collider other)
	{
		if(hasHit == false && (other.CompareTag("Stage") || other.CompareTag("Player")) && other.gameObject != parent)
		{
			hasHit = true;
			setArmed(false);
			GetComponent<Renderer>().material.color = Color.black;
			GetComponent<CapsuleCollider>().enabled = false;
			pickUpCollider.enabled = true;
		}
		else if (hasHit && other.CompareTag("Player") && !other.GetComponent<PlayerControllerMatt>().armed)
		{
			other.GetComponent<PlayerControllerMatt>().PickUp(this.transform.gameObject);
			//Destroy (GetComponent<EquipmentThrow>());
			//Destroy(GetComponent<Rigidbody>());
			pickUpCollider.enabled = false;
			colorScript.ResetColor();
		}
		if (other.CompareTag("Player") && !Available())
		{
			PlayerControllerMatt victim = other.GetComponent<PlayerControllerMatt>();
			if (victim.alive && GetComponent<RapierOwnership>().hasHit)
			{	
				sound.Play();
				victim.Kill(new Vector3 (transform.position.x * -1, transform.position.y,transform.position.z));
				//	count++;
				//SetScoreText();
			}
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
		{
			transform.parent = null;
			owned = false;
			GetComponent<SetColorToParent>().ResetColor();
		}
	}
	
}
