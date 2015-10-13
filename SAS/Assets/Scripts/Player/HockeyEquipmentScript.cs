using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HockeyEquipmentScript : MonoBehaviour {
    
	public Text scoreText;
	private int count;
	public Text winText;

	// Use this for initialization
	void Start () {
		count = 0;
		//SetScoreText ();
		//winText.text = "";
	}
	
	// Update is called once per frame
	void Update () {

	}

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            HockeyTempPlayerController victim = other.GetComponent<HockeyTempPlayerController>();
            if (victim.alive)
            {
            	Debug.Log("Hit detected, sending "+ (transform.right*-1));
                victim.Kill(new Vector3 (transform.position.x * -1, transform.position.y,transform.position.z));
                //This causes no movement at the center of the field
				count++;
				//SetScoreText();
            }
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Vector3 sweet = new Vector3(transform.position.x, transform.localPosition.y, transform.position.z);
        Gizmos.DrawLine(sweet, transform.forward * 1.5f);
    }

	/*void SetScoreText ()
	{
		scoreText.text = "Score: " + count.ToString ();
		if (count >= 3)
		{
			winText.text = "Winnner!";
		}
	}*/
}