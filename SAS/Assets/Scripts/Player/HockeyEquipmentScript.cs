using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HockeyEquipmentScript : MonoBehaviour {
    
	private HockeyPlayerController player;
	
	void Update () {
		player = transform.parent.GetComponent<HockeyPlayerController>();
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
		{
            HockeyPlayerController victim = other.GetComponentInParent<HockeyPlayerController>();
            if (victim.alive)
            {
                victim.Kill(transform.right*-200f);
				player.stats.AddKill ();
            }
        }
    }
}
