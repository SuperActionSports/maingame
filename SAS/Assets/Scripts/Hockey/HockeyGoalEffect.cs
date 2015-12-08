using UnityEngine;
using System.Collections;

public class HockeyGoalEffect : MonoBehaviour {

	public ParticleSystem[] parts;
	public ParticleAnimator[] partAnimators;
	public ParticleSystemRenderer[] partRends;
	// Use this for initialization
	void Start () {
		

	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void PartyToDeath(Color color){
	Debug.Log("Parts is " + parts);
		if (parts.Length == 0) 
		{
			Debug.Log ("Parts is null now");
			parts = GetComponentsInChildren<ParticleSystem>();
		 	partRends = GetComponentsInChildren<ParticleSystemRenderer>();
		 }
		for(int i = 0; i < parts.Length; i++)
		{
			partRends[i].material.color = color;
			parts[i].startColor = color;
		}
		//transform.parent = null;
		foreach (ParticleSystem p in parts)
		{
			p.startColor = color;
			p.Play ();
		}
		Destroy(gameObject,10);
	}
}
	