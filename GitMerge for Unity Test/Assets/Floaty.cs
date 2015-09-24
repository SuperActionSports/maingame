using UnityEngine;
using System.Collections;

public class Floaty : MonoBehaviour {
    
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = new Vector3(4, 1 * Mathf.Sin(Time.time)+2, 0);
	}
}
