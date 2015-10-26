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
		particles = psHolder.GetComponentInChildren<ParticleSystem> ();
		c.a = 1f;
		pos = position;
		dir = direction;
		direction.x = 0;
		RaycastHit hit;
		Physics.Raycast (position, direction, out hit);

		GameObject target = hit.collider.gameObject;
		GameObject splat = null;

		if (target.CompareTag("Stage")) {
			var splatRotation = Quaternion.FromToRotation (Vector3.forward, hit.normal);
			splat = Instantiate (splatter, hit.point, splatRotation) as GameObject;
			splat.transform.localEulerAngles = new Vector3(90, 0, 0);
		}
		else if (target.CompareTag("Wall")) {
			var splatRotation = Quaternion.FromToRotation (Vector3.forward, hit.normal);
			splat = Instantiate (splatter, hit.point, splatRotation) as GameObject;
			splat.transform.localEulerAngles = new Vector3(0, 0, 0);
		}
		GameObject pSystem = Instantiate(psHolder, position, Quaternion.identity) as GameObject;

		Debug.Log ("Direction: " + direction);
		pSystem.transform.rotation = Quaternion.LookRotation (direction);

		if (splat != null) {
			splat.GetComponent<SpriteRenderer>().color = c;
			splat.transform.localScale = new Vector3 (Random.Range (0.1f, 2f), Random.Range (0.1f, 2f), Random.Range (0.1f, 2f));	
			splat.transform.localEulerAngles = new Vector3(splat.transform.localEulerAngles.x,
			                                               splat.transform.localEulerAngles.y, 
			                                               Random.Range (0f, 360f));
		}

		//particles.startColor = c;
		//particles.Emit (Mathf.FloorToInt (Random.Range (3f, 8f)));

	}

	void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawRay (pos, dir);
	}

}
