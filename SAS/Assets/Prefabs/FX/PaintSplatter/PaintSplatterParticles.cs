using UnityEngine;
using System.Collections;

public class PaintSplatterParticles : MonoBehaviour {

	private int timer;

	// Use this for initialization
	void Start () {
		timer = 60;
	}
	
	// Update is called once per frame
	void Update () {
		timer--;
		if (timer == 0) {
			Destroy (this.gameObject);
		}
	}
}
