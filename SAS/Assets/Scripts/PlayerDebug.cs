using UnityEngine;
using System.Collections;

public class PlayerDebug : MonoBehaviour {

	Vector3 ave;
	Vector3 far1x, far2x, far1y, far2y;
	public Transform[] players;
	// Use this for initialization
	void Start () {
		ave = new Vector3(0,0,0);
		players = GetComponentsInChildren<Transform>();
	}
	
	// Update is called once per frame
	void LateUpdate () {
		float x = 0, y = 0;
		float maxX = 0, maxY = 0;
		
		far1x = new Vector3 (0,0,0);
		far2x = new Vector3 (0,0,0);
		far1y = new Vector3 (0,0,0);
		far2y = new Vector3 (0,0,0);
		foreach (Transform t in players) {
			if (t.CompareTag("Player")) {
				x += t.position.x;
				y += t.position.y;
				foreach (Transform f in players) {
						if (Mathf.Abs(t.position.x-f.position.x) > maxX) {
							if (f.CompareTag("Player")) {
								maxX = t.position.x-f.position.x;
								far1x = t.position;
								far2x = f.position;
							}
						}
						if (Mathf.Abs(t.position.y-f.position.y) > maxY) {
							if (f.CompareTag("Player")) {
							maxY = t.position.y-f.position.y;
							far1y = t.position;
							far2y = f.position;
							}
						}
				}
			}
		}
		x /= players.Length - 1;
		y /= players.Length - 1;
//		Debug.Log(GetComponentsInChildren<Transform>().Length);
		ave = new Vector3(x,y,0);
	}
	
	
	void OnDrawGizmos() {
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(ave,1f);
		Gizmos.DrawLine(far1x,far2x);
		Gizmos.DrawLine(far1y,far2y);
	}
}
