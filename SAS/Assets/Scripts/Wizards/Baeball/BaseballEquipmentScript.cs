using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BaseballEquipmentScript : MonoBehaviour {
    
//	public Text scoreText;
	private int count;
	private BaseballPlayerController player;
	public Text winText;

	// Use this for initialization
	void Start () {
		count = 0;
//		SetScoreText ();
//		winText.text = "";
	}
	
	// Update is called once per frame
	void Update () {
		player = transform.parent.GetComponent<BaseballPlayerController>();
	}

    void OnTriggerEnter(Collider other)
    {
    	Debug.Log("Bat has hit " + other.gameObject.tag);
        if (other.CompareTag ("Player") && transform.parent.gameObject != other.transform.gameObject) {
			BaseballPlayerController victim = other.GetComponent<BaseballPlayerController> ();
			if (victim.alive && player.alive) {
				Debug.Log ("Hit detected, sending " + (transform.right * -1));
				victim.Kill (new Vector3 (transform.position.x * -1, transform.position.y, transform.position.z));
				player.stats.AddKill ();
				//This causes no movement at the center of the field
				AudioSource hit = GetComponent<AudioSource> ();
				hit.Play ();
				count++;
//				SetScoreText();
			}
		} else if (other.CompareTag ("Ball") && transform.parent.gameObject != other.transform.gameObject) {
			player.stats.AddMadeHit();
		}
	} 

    void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Vector3 sweet = new Vector3(transform.position.x, transform.localPosition.y, transform.position.z);
        Gizmos.DrawLine(sweet, transform.forward * 1.5f);
    }

//	void SetScoreText ()
//	{
//		scoreText.text = "Score: " + count.ToString ();
//		if (count >= 3)
//		{
//			winText.text = "Winnner!";
//		}
//	}
}
