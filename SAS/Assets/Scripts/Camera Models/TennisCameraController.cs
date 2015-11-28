using UnityEngine;
using System.Collections;

public class TennisCameraController : MonoBehaviour {
	
	private Vector3 oldPosition;
	public bool shake;
	public float shakeLength;
	public Vector3 shakeMagnitude;
	public float shakeIntensity;
	private Camera cam;
	public float debugLerp;
	
	void Start()
	{
		cam = Camera.main;
		oldPosition = cam.transform.position;
	}
	
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			shake = true;
		}
		if (shake)
		{
			shake = false;
			PlayShake();
		}
		cam.transform.position = Vector3.Lerp(cam.transform.position, oldPosition, Time.deltaTime * debugLerp);
	}
	
	// Use this for initialization
	public void PlayShake()
	{
		//oldPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
		StopAllCoroutines();
		StartCoroutine("Shake");
	}
	
	public void PlayShake(Vector3 impact)
	{
		oldPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
		if (impact.x < 0 && shakeMagnitude.x > 0)
		{
			shakeMagnitude.x *= -1;
		}
		if (impact.x > 0 && shakeMagnitude.x < 0)
		{
			shakeMagnitude.x *= -1;
		}
		StopAllCoroutines();
		StartCoroutine("Shake");
	}
	
	IEnumerator Shake()
	{
		
		float elapsed = 0.0f;
		
		Vector3 oldPosition = Camera.main.transform.position;
		
		while (elapsed < shakeLength)
		{
			
			elapsed += Time.deltaTime;
			
			float percentComplete = elapsed / shakeLength;
			float damper = 1.0f - Mathf.Clamp(4.0f * percentComplete - 3.0f, 0.0f, 1.0f);
			
			// map value to [-1, 1]
			float x = Random.Range(-1,1) * 2.0f - 1.0f;
			float y = Random.Range(2, 4) * 2.0f - 1.0f;
			x *= shakeMagnitude.x * damper;
			y *= shakeMagnitude.y * damper;
			
			Camera.main.transform.position = new Vector3(x + oldPosition.x, y + oldPosition.y, oldPosition.z);
			
			yield return null;
		}
	}
}