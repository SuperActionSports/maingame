using UnityEngine;
using System.Collections;

public class SetColorToOther : MonoBehaviour {

	public Color c;
	public ParticleSystem parts;
	public ParticleAnimator partAnimators;
	public ParticleSystemRenderer partRends;
	// Use this for initialization
	void Start () {
		c = Color.white;	
		parts = GetComponent<ParticleSystem>();
		partRends = GetComponent<ParticleSystemRenderer>();
	}
	
	public void ResetColor(Color color)
	{
		c = color;
		partRends.material.color = c;
		parts.startColor = c;	
	}
	
}
