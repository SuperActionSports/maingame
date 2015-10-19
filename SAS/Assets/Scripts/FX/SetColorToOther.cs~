using UnityEngine;
using System.Collections;

public class SetColorToOther : MonoBehaviour {

	public Color c;
	public Color c2;
	public ParticleSystem parts;
	public ParticleAnimator partAnimators;
	public ParticleSystemRenderer partRends;
	public bool hit;
	// Use this for initialization
	void Start () {
		c = Color.white;	
		parts = GetComponent<ParticleSystem>();
		partRends = GetComponent<ParticleSystemRenderer>();
		hit = false;
	}
	
	void Update()
	{
		if (hit)
		{
			ResetColor(c2);
			hit = false;
		}
		
	}
	
	public void ResetColor(Color color)
	{
		c = color;
		partRends.material.color = c;
		parts.startColor = c;	
	}
	
}
