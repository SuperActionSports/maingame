using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HockeyEquipmentScript : MonoBehaviour {
    
	public HockeyPlayerController player;
	
	void Update () {
		/* HockeyPlayer - Controller Attached
		 * Body
		 * BatHand
		 * Bat
		 * 
		 * therefore, need another .parent
		 */
		player = transform.parent.transform.parent.transform.parent.GetComponent<HockeyPlayerController>();
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
		{
            HockeyPlayerController victim = other.GetComponentInParent<HockeyPlayerController>();
			//player = this.transform.parent.transform.parent.GetComponent<HockeyPlayerController>();
            if (victim.alive)
            {
				//victim.Kill (new Vector3 (transform.localPosition.x*-1, transform.position.y, transform.localPosition.z * -1));
				victim.Kill(transform.right);
				Debug.Log("***Player: "+player);
				player.stats.AddKill ();
            }
        }
    }
}
