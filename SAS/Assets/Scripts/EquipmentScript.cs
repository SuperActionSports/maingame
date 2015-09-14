using UnityEngine;
using System.Collections;

public class EquipmentScript : MonoBehaviour {

	private GameObject bat;
	public KeyCode attack;
	// Use this for initialization
	void Start () {
		bat = GetComponentInChildren<GameObject>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey(attack))
		{
			
		}
	}
}
