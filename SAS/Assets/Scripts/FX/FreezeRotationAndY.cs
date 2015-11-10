using UnityEngine;
using System.Collections;

public class FreezeRotationAndY : MonoBehaviour {

	private Vector3 frozenPosition;
	private Quaternion frozenQuaternion;
	private Light l;
	// Use this for initialization
	void Start () {
	frozenPosition = transform.position;
	frozenQuaternion = transform.rotation;
 	l = GetComponent<Light>();
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector3(transform.parent.transform.position.x, transform.parent.transform.position.y, transform.parent.transform.position.z);
		transform.rotation = frozenQuaternion;
		if (l != null)
		{
			l.intensity = Mathf.Clamp(transform.parent.transform.position.y-2, 0, 8);
		}
	}
}
