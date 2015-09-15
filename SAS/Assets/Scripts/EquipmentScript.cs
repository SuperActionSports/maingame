using UnityEngine;
using System.Collections;

public class EquipmentScript : MonoBehaviour {

	private GameObject bat;
	public KeyCode attack;
    Animator anim;
	// Use this for initialization
	void Start () {
		bat = GetComponentInChildren<GameObject>();
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey(attack))
		{
            Debug.Log("Attack!");
            anim.SetTrigger("Attack");
		}
	}
}
