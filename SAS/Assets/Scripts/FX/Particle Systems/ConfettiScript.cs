using UnityEngine;
using System.Collections;

public class ConfettiScript : MonoBehaviour {

	public KeyCode replay;
	public Color c;
	public ParticleSystem[] parts;
	public ParticleAnimator[] partAnimators;
	public ParticleSystemRenderer[] partRends;
	private bool played;
	public float countdown;
	// Use this for initialization
	void Start () {
		parts = GetComponentsInChildren<ParticleSystem>();
		partRends = GetComponentsInChildren<ParticleSystemRenderer>();
		countdown = 10;
		played = false;
	}
	
	// Update is called once per frame
	void Update () {
		 if (Input.GetKeyDown(replay))
		 {
		 	Party();
		 }
		 if (played)
		 {
		 	played = false;
		 }
	}
	
	public void Party()
	{
		transform.parent = null;
		foreach (ParticleSystem p in parts)
		{
			p.Play ();
			
		}
		played = true;
	}
	
	public void Party(Color c, Vector3 pos)
	{	transform.localPosition = new Vector3(pos.x,pos.y,pos.z);
		for(int i = 0; i < parts.Length; i++)
		{
			partRends[i].material.color = c;
			parts[i].startColor = c;
		}
		foreach (ParticleSystem p in parts)
		{
			p.Play ();
			
		}
		played = true;
		//Destroy(gameObject,10);
	}
	
	public void Party(Color c){
		for(int i = 0; i < parts.Length; i++)
		{
			partRends[i].material.color = c;
			parts[i].startColor = c;
		}
		//transform.parent = null;
		foreach (ParticleSystem p in parts)
		{
			p.Play ();
			
		}
		played = true;
		//Destroy(gameObject,10);
	}
	
	public void PartyToDeath(Color color, float offset){
		Debug.Log("Party to death: " + color);
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
		played = true;
		Destroy(gameObject,10);
	}
	
	public void PartyToDeath(Color color){
		parts = GetComponentsInChildren<ParticleSystem>();
		partRends = GetComponentsInChildren<ParticleSystemRenderer>();
		countdown = 10;
		played = false;
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
		played = true;
		Destroy(gameObject,10);
	}
}
