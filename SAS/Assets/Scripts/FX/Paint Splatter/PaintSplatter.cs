using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class PaintSplatter : MonoBehaviour {
	
	public Transform PaintSprite;
	public GameObject splatter;
	public GameObject psHolder;
	public ParticleSystem particles;
	public Color c;
	public Vector3 pos;
	public Vector3 dir;
	
	public void Splatter(Vector3 position, Vector3 direction) {	
		c.a = 1f;
		pos = position;
		dir = direction;

		CreateParticleSystem ();

		GenerateSplats ();
	}

	public void CreateParticleSystem()
	{
		GameObject pSystem = Instantiate (psHolder, pos, Quaternion.identity) as GameObject;
		particles = pSystem.GetComponentInChildren<ParticleSystem>();
		particles.startColor = c;
		
		Debug.Log ("Direction: " + dir);
		pSystem.transform.rotation = Quaternion.LookRotation (dir);
	}

	public void GenerateSplats()
	{
		RaycastHit hit;
		Physics.Raycast (pos, dir, out hit);
		
		GameObject target = hit.collider.gameObject;

		int amountOfSplats = Random.Range (1, 4);
		GameObject[] splats = new GameObject[amountOfSplats];
		Debug.Log ("amountOfSplats: " + amountOfSplats);
		
		for (int i = 0; i < amountOfSplats; i++) 
		{
			float zSpot = Random.Range (hit.point.z - 1f, hit.point.z + 1f);
			var splatRotation = Quaternion.FromToRotation (Vector3.forward, hit.normal);
			splats[i] = Instantiate (splatter, new Vector3(hit.point.x, hit.point.y, zSpot), splatRotation) as GameObject;

			Debug.Log ("Hit.point: " + hit.point);

			if (target.CompareTag ("Stage")) 
			{
				splats[i].transform.localEulerAngles = new Vector3 (90, 0, 0);
			} 
			else if (target.CompareTag ("Wall")) 
			{
				splats[i].transform.localEulerAngles = new Vector3 (0, 0, 0);
			}
			
			if (splats[i] != null) {
				splats[i].GetComponent<SpriteRenderer> ().color = c;
				splats[i].transform.localScale = new Vector3 (Random.Range (0.1f, 2f), Random.Range (0.1f, 2f), Random.Range (0.1f, 2f));	
				splats[i].transform.localEulerAngles = new Vector3 (splats[i].transform.localEulerAngles.x,
				                                                    splats[i].transform.localEulerAngles.y, 
				                                                    Random.Range (0f, 360f));
			}
		}
	}

	void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawRay (pos, dir);
	}

}
