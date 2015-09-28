using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EquipmentScript : MonoBehaviour {
    
	//public Text scoreText;
	//private int count;
	//public Text winText;

	// Use this for initialization
	AudioSource sound;
	RapierScript owner;
	void Start () {
	sound = GetComponentInParent<AudioSource>();
		//count = 0;
		SetScoreText ();
		owner = GetComponent<RapierScript>();
	//winText.text = "";
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void setArmed(bool armed)
	{
		GetComponent<CapsuleCollider>().enabled = armed;
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
