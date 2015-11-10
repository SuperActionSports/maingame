using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GolfClubScript : MonoBehaviour {

	private GolfPlayerController player;

	void Update () {
		player = transform.parent.transform.parent.GetComponent<GolfPlayerController>();
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag ("Player")) {
			GolfPlayerController victim = other.GetComponent<GolfPlayerController> ();
			if (victim.alive) {
				victim.Kill (transform.right);
			}
		} else if (other.CompareTag ("Ball")) {
			player.stats.AddMadePutt();
		}
	}
}
