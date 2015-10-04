using UnityEngine;
using System.Collections;

public class HockeyPlayerDiskController : MonoBehaviour {

	public GameObject DiskSelf;
	// Use this for initialization
	void Start () {
		DiskSelf = GameObject.Find ("Disk");
	}
	
	// Update is called once per frame
	void Update () {
		DiskSelf.transform.position = transform.parent.transform.position;
	
	}
}
