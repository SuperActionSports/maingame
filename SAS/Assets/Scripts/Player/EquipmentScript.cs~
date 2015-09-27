using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EquipmentScript : MonoBehaviour {
    
	//public Text scoreText;
	//private int count;
	//public Text winText;

	// Use this for initialization
	AudioSource sound;
	RapierOwnership owner;
	void Start () {
	sound = GetComponentInParent<AudioSource>();
		//count = 0;
		SetScoreText ();
		owner = GetComponent<RapierOwnership>();
	//winText.text = "";
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void setArmed(bool armed)
	{
		GetComponent<CapsuleCollider>().enabled = armed;
	}

    void OnTriggerEnter(Collider other)
    {/*
		Debug.Log("About to pick up. Availability: owned: " + !owner.owned + ", hasHit: " + owner.hasHit + ", " + owner.transform.parent);
        if (other.CompareTag("Player") && !owner.Available())
        {
            PlayerControllerMatt victim = other.GetComponent<PlayerControllerMatt>();
            if (victim.alive && GetComponent<RapierOwnership>().hasHit)
            {	
            	sound.Play();
                victim.Kill(new Vector3 (transform.position.x * -1, transform.position.y,transform.position.z));
			//	count++;
				SetScoreText();
            }
        }
        */
    }

	void SetScoreText ()
	{
		//scoreText.text = "Score: " + count.ToString ();
	//	if (count >= 3)
	//	{
		//	winText.text = "Winnner!";
	//	}
	}
}
