using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class PaintSplatter : MonoBehaviour {
	
	public Transform PaintSprite;
	public GameObject splatter;
	public Color c;
	
	public void Splatter(Vector3 position, Vector3 direction) {	
		c.a = 1f;

		RaycastHit hit;
		Physics.Raycast (position, direction, out hit);

		var splatRotation = Quaternion.FromToRotation (Vector3.up, hit.normal);
		GameObject splat = Instantiate (splatter, hit.point, splatRotation) as GameObject;

		splat.GetComponent<SpriteRenderer>().color = c;
	}
}
