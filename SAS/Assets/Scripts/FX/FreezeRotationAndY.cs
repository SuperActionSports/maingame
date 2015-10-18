using UnityEngine;
using System.Collections;

public class FreezeRotationAndY : MonoBehaviour {

	private Vector3 frozenPosition;
	private Quaternion frozenQuaternion;
	// Use this for initialization
	void Start () {
	frozenPosition = transform.position;
	frozenQuaternion = transform.rotation;
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector3(transform.parent.transform.position.x, frozenPosition.y, transform.parent.transform.position.z);
		transform.rotation = frozenQuaternion;
	}
}
