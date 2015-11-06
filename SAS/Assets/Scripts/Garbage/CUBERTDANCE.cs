using UnityEngine;
using System.Collections;

public class CUBERTDANCE : MonoBehaviour {
	private float tOffset;
	public float height = 2;
	private float yOffset;
	public bool hats = false;
	public Transform attachPoint;
	public GameObject hat;
	// Use this for initialization
	void Start () {
		tOffset = Random.Range(0,360);
		yOffset = transform.position.y;
		//attachPoint = transform.FindChild("AttachPoint");
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector3(transform.position.x,Dance()+yOffset,transform.position.z);
		if (hats && Input.GetKeyDown(KeyCode.Space))
		{
			GameObject h = Instantiate(hat,transform.position,Quaternion.identity) as GameObject;
			h.transform.parent = attachPoint;
			h.transform.localScale = new Vector3(1,1,1);
			h.transform.localPosition = new Vector3(0,0,0);
		}
	}
	
	float Dance()
	{
		return height*Mathf.Sin(Time.time + tOffset);
	}
}
