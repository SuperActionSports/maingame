using UnityEngine;
using System.Collections;

public class FencingPaintSplatter : MonoBehaviour {

	public Vector3 endPos;
	public Vector3 endRot;
	private Projector projector;
	public float endClipPlane;
	// Use this for initialization
	void Start () {
		projector = GetComponent<Projector>();
		projector.enabled = true;
		//endPos += new Vector3(endPos.x * Random.Range(-1,1),endPos.y * Random.Range(-1,1),endPos.z * Random.Range(-1,1));
		//endRot += new Vector3(endRot.x * Random.Range(-1,1),endRot.y * Random.Range(-1,1),endRot.z * Random.Range(-1,1));
	}
	
	// Update is called once per frame
	void Update () {
		if (projector.enabled)
		{
			//transform.eulerAngles = Vector3.Lerp (transform.eulerAngles,endRot,Time.deltaTime/10);
			//transform.position = Vector3.Lerp(transform.position,endPos,Time.deltaTime/10);
			projector.farClipPlane = Mathf.Lerp(projector.farClipPlane,endClipPlane,Time.deltaTime/10f);
			//transform.localPosition = endPos;
			//transform.localEulerAngles = endRot;
		}
	}
	
	public void Activate(Vector3 placedPosition, Color c)
	{
		endPos += placedPosition;
		//endPos.z -= placedPosition.z;
		Material projectorMat = (Material) Instantiate (GetComponent<Projector> ().material);
		projectorMat.color = c;
		GetComponent<Projector>().material = projectorMat;
	}
	
}
