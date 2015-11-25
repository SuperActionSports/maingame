using UnityEngine;
using System.Collections;

public class BallValueCounter : MonoBehaviour {

	public Texture[] numbers;
	public int currentValue = 0;
	private Animator anim;
	private Light l;
	public bool nextTrigger;
	// Use this for initialization
	void Start () {
	anim = GetComponent<Animator>();
	l = GetComponent<Light>();
	l.cookie = numbers[0];
	nextTrigger = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Space))
		{
			nextTrigger = true;
		}
		if (nextTrigger)
		{
			BallHitBrick();		
			nextTrigger = false;
		}
		
	}
	
	public void IncrementValueDisplay()
	{
		if (currentValue < numbers.Length - 1){ currentValue++;}
		l.cookie = numbers[currentValue];
	}
	
	public void BallHitBrick()
	{
		Debug.Log("Current val: " + currentValue);
		if ((currentValue < numbers.Length - 1))
		{
			anim.SetTrigger("Play");
		}
		else
		{
			anim.SetTrigger("Stutter");
		}
	}
	
	public void Reset()
	{
		currentValue = 0;
		l.cookie = numbers[currentValue];
	}
}
