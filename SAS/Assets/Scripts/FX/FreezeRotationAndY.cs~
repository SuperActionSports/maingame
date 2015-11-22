using UnityEngine;
using System.Collections;

public class FreezeRotationAndY : MonoBehaviour {

	public float yOffset;
	private Quaternion frozenQuaternion;
	private Light l;
	// Use this for initialization
	void Start () {
	yOffset = transform.parent.transform.position.y - transform.localPosition.y;
	frozenQuaternion = transform.rotation;
 	l = GetComponent<Light>();
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector3(transform.parent.transform.position.x, transform.parent.transform.position.y + 3, transform.parent.transform.position.z);
		//transform.localPosition = new Vector3(transform.localPosition.x,yOffset,transform.localPosition.z);
		transform.rotation = frozenQuaternion;
		if (l != null)
		{
			l.intensity = Mathf.Clamp(transform.parent.transform.position.y-2, 0, 8);
		}
	}
}
