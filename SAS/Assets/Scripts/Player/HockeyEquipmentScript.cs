using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HockeyEquipmentScript : MonoBehaviour {
    
	public HockeyPlayerController player;
	
	void Update () {
		player = transform.parent.transform.parent.GetComponent<HockeyPlayerController>();
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
		{
            HockeyPlayerController victim = other.GetComponentInParent<HockeyPlayerController>();
            if (victim.alive)
            {
                victim.Kill(transform.right*-8f);
				player.stats.AddKill ();
            }
        }
    }
}
