using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EquipmentScriptWithTennis : MonoBehaviour {
    
	//public Text scoreText;
	private int count;
	public int speed;
	//public Text winText;

	// Use this for initialization
	void Start () {
		count = 0;
		Debug.Log ("Equip script is working");
		//SetScoreText ();
		//winText.text = "";
	}
	
	// Update is called once per frame
	void Update () {

	}

    void OnTriggerEnter(Collider other)
    {
		//Debug.Log ("Other tag: " + other.gameObject.tag);
        if (other.CompareTag("Player"))
        {
            TennisController victim = other.GetComponent<TennisController>();
            if (victim.alive)
            {
            	Debug.Log("Hit detected, sending "+ (transform.right*-1));
                victim.Kill(new Vector3 (transform.position.x * -1, transform.position.y,transform.position.z));
                //This causes no movement at the center of the field
				count++;
				SetScoreText();
            }
        }
		if (other.CompareTag ("Ball")) 
		{
			Debug.Log ("Found ball");
			Rigidbody rbBall = other.GetComponent<Rigidbody>();
			Quaternion angle = Quaternion.AngleAxis(45.0f, Vector3.right);
			rbBall.AddForce(angle * -transform.forward * speed);
		}
    }

	void OnCollisionEnter()
	{
		Debug.Log ("Collision with racquet occurred.");
	}

    void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Vector3 sweet = new Vector3(transform.position.x, transform.localPosition.y, transform.position.z);
        Gizmos.DrawLine(sweet, transform.forward * 1.5f);
    }

	void SetScoreText ()
	{
		//scoreText.text = "Score: " + count.ToString ();
		if (count >= 3)
		{
			//winText.text = "Winnner!";
		}
	}
}
