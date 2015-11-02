using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class PaintSplatter : MonoBehaviour {
	
	public Transform PaintSprite;
	public GameObject[] givenPaintSprites = new GameObject[3];
	public GameObject psHolder;
	public ParticleSystem particles;
	public Color c;
	
	public void Splatter(Vector3 position, Vector3 direction) {	
		// Set the color's alpha to 1 so it appears as a solid color
		c.a = 1f;
		//dir = new Vector3(direction.x+Random.Range (-15f, 15f), direction.y-10f, direction.z+Random.Range (-15f, 15f));
		//direction = new Vector3 (direction.x, direction.y, direction.z);
		
		CreateParticleSystem (position, direction);
		GenerateSplats (position, direction);
	}

	public void CreateParticleSystem(Vector3 position, Vector3 direction)
	{
		GameObject pSystem = Instantiate (psHolder, position, Quaternion.identity) as GameObject;
		particles = pSystem.GetComponentInChildren<ParticleSystem>();
		particles.startColor = c;
		
		Debug.Log ("Direction: " + direction);
		pSystem.transform.rotation = Quaternion.LookRotation (direction);
	}
	
	public void GenerateSplats(Vector3 position, Vector3 direction)
	{
		RaycastHit hit;
		Physics.Raycast (position, direction, out hit);
		GameObject target = hit.collider.gameObject;

		GenerateTrail (position, hit.point);
		int amountOfSplats = Random.Range (1, 4);

		GameObject[] splats = new GameObject[amountOfSplats];
		Debug.Log ("amountOfSplats: " + amountOfSplats);
		
		for (int i = 0; i < amountOfSplats; i++)  {
			// stop tecxture flickering by moving splats up pout of surface
			var splatLocation = hit.point+hit.normal*.1f;
			var splatRotation = Quaternion.FromToRotation (Vector3.forward, hit.normal);
			
			// Instantiate paint splat
			splats[i] = Instantiate(givenPaintSprites[Random.Range (0, givenPaintSprites.Length)], splatLocation, splatRotation) as GameObject;

			if (target.CompareTag ("Stage")) 
			{
				splats[i].transform.localEulerAngles = new Vector3 (90, 0, 0);
			} 
			else if (target.CompareTag ("Wall")) 
			{
				if (Mathf.Abs (hit.normal.z) > Mathf.Abs (hit.normal.x)) { splats[i].transform.localEulerAngles = new Vector3 (0, 0, 0); }
			}
			
			if (splats[i] != null) {
				splats[i].GetComponent<SpriteRenderer> ().color = c;
				// Set a semi-random scale and rotation of the object
				splats[i].transform.localScale = new Vector3 (Random.Range (1f, 4f), Random.Range (1f, 4f), Random.Range (1f, 4f));	
				splats[i].transform.localEulerAngles = new Vector3 (splats[i].transform.localEulerAngles.x,
				                                                    splats[i].transform.localEulerAngles.y, 
				                                                    Random.Range (0f, 360f));
			}
		}
	}
	
	public void GenerateTrail (Vector3 position, Vector3 point)
	{
		for(float i = 0.2f; i < point.magnitude; i = i + 0.05f)
		{
			Vector3 randomPoint = Vector3.Lerp(position, point, i);
			RaycastHit hit;
			Physics.Raycast (randomPoint, new Vector3(Random.Range (-0.5f, 0.5f), -1, 0), out hit);

			var splatLocation = hit.point+hit.normal*.1f;
			var splatRotation = Quaternion.FromToRotation (Vector3.forward, hit.normal);

			GameObject splat = Instantiate(givenPaintSprites[Random.Range (0, givenPaintSprites.Length)], splatLocation, splatRotation) as GameObject;
			splat.transform.localEulerAngles = new Vector3 (90, 0, 0);
			splat.GetComponent<SpriteRenderer> ().color = c;
		}
	}
}