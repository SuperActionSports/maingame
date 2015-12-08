using UnityEngine;
using System.Collections;

public class PerlinAudienceController : MonoBehaviour {
	
	private float t;		// Time in the lerp
	private float tLimitOrigin = 0.5f;
	private float tLimit;	// Lerp break [0,1]
	private Vector3 origin;
	private Vector3 goTo;
	//private int interval;
	private bool returning = false;
	public float speedMod = 1;
	public float interestFade = 0;
	public float interestFadeStart = 0;
	public float interestLimit = 1;
	public Color color = Color.white;
	private Renderer rend;
	// Use this for initialization
	void Start () {
		ResetTLimit();
		origin = transform.position;
		goTo = GenerateNewGoTo();
		rend = GetComponent<Renderer>();
		ChangeColor(color);
		
	}
	
	// Update is called once per frame
	void Update () {
		t += Time.deltaTime * speedMod;
		interestFade += Time.deltaTime;
		if (t < tLimit)
		{
			//Debug.Log("Moving to " + goTo);
			transform.position = Vector3.Lerp(transform.position,goTo,t);
		}	
		else if (returning)
		{
			t = 0;
			//Debug.Log("New Position");
			goTo = GenerateNewGoTo();
			returning = false;
		}
		else
		{
			t = 0;
			//Debug.Log("Returning");
			goTo = origin;
			returning = true;
		}		
		if (interestFade > interestFadeStart + interestLimit)
		{
			speedMod = 1;
		}	
	}
	
	Vector3 GenerateNewGoTo()
	{
		if (Random.Range(0,100) > 70)
		{
			ResetTLimit();
		}
		float intervalX = (int)Random.Range(0,100);
		float intervalY = (int)Random.Range(0,100);
		float moveTo = Mathf.PerlinNoise(intervalX,intervalY);
		//Debug.Log("Using " + intervalX + ", " + intervalY + " moving to " + moveTo);
		return new Vector3 (
			origin.x + Random.Range(-.05f,.05f),
			1.5f * moveTo + origin.y,
			origin.z + Random.Range(-.05f,.05f)		
			);
	}
	
	public void SmallEvent()
	{
		interestFadeStart = Time.time;
		interestFade = 0;
		speedMod = 4;
	}
	
	public void BigEvent()
	{
		interestFadeStart = Time.time;
		interestFade = 0;
		speedMod = 5;
	}
	
	private void ResetTLimit()
	{
		tLimit = tLimitOrigin + Random.Range(-0.3f,0.3f);
	}
	
	public void ChangeColor(Color newColor)
	{
		rend.material.color = newColor;
	}
}
