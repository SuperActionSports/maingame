using UnityEngine;
using System.Collections;

public class CUBERTDANCE : MonoBehaviour {
	private float tOffset;
	public float height = 2;
	private float yOffset;
	// Use this for initialization
	void Start () {
		tOffset = Random.Range(0,360);
		yOffset = transform.position.y;
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector3(transform.position.x,Dance()+yOffset,transform.position.z);
	}
	
	float Dance()
	{
		return height*Mathf.Sin(Time.time + tOffset);
	}
}
