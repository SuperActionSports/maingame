using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EquipmentScriptWithTennis : MonoBehaviour {
    
	//public Text scoreText;
	private int count;
	public int speed;
	private TennisControllerGans player;
	public float impactMod = 20;
	//public Text winText;

	// Use this for initialization
	void Start () {
		count = 0;
		Debug.Log ("Equip script is working");
		//SetScoreText ();
		//winText.text = "";
		player = GetComponentInParent<TennisControllerGans> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider other)
    {
		//Debug.Log ("Other tag: " + other.gameObject.tag);
		if (other.CompareTag("Player"))
		{
			if(player.isAttacking)
			{
				TennisControllerGans victim = other.GetComponent<TennisControllerGans>();
				if (victim.alive)
				{
					Vector3 killDirection = player.facingDirection();
					killDirection = new Vector3(impactMod*Mathf.Sin(killDirection.y),impactMod, impactMod*Mathf.Cos(killDirection.y));
					victim.Kill(killDirection);
					player.stats.AddKill();
					//This causes no movement at the center of the field
					count++;
					SetScoreText();
				}
			}
		}
    }

	/*void OnTriggerStay(Collider other)
	{
		if (other.CompareTag ("Ball")) 
		{
			Debug.Log ("Found ball");
			Rigidbody rbBall = other.GetComponent<Rigidbody>();
			Quaternion angle = Quaternion.AngleAxis(45.0f, Vector3.right);
			rbBall.AddForce(angle * -transform.forward * speed);
		}
	}*/

	void OnCollisionEnter()
	{
		Debug.Log ("Collision with racquet occurred.");
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
