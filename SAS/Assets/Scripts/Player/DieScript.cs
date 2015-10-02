using UnityEngine;
using System.Collections;

public class DieScript : MonoBehaviour {

	public GameObject ps;
	private ParticleSystem part;
	private ConfettiScript effect;
	// Use this for initialization
	void Start () {
		effect = transform.GetComponentInChildren<ConfettiScript>();
	}
	
	public void Play()
	{
		
	}
	
	public void Party()
	{
		transform.parent = null;
		effect.Party();
	}
		
	// Update is called once per frame
	void Update () {
	
	}
}
