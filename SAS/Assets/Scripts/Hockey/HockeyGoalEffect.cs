using UnityEngine;
using System.Collections;

public class HockeyGoalEffect : MonoBehaviour {

	public ParticleSystem[] parts;
	public ParticleAnimator[] partAnimators;
	public ParticleSystemRenderer[] partRends;
	// Use this for initialization
	void Start () {
		parts = GetComponentsInChildren<ParticleSystem>();
		partRends = GetComponentsInChildren<ParticleSystemRenderer>();

	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void PartyToDeath(Color color){
		for(int i = 0; i < parts.Length; i++)
		{
			partRends[i].material.color = color;
			parts[i].startColor = color;
		}
		//transform.parent = null;
		foreach (ParticleSystem p in parts)
		{
			p.Play ();
			
		}
		Destroy(gameObject,10);
	}
}
	