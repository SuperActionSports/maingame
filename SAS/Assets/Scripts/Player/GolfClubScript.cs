using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GolfClubScript : MonoBehaviour {
	
	void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			GolfPlayerController victim = other.GetComponent<GolfPlayerController>();
			if (victim.alive)
			{
				victim.Kill( transform.right*-200f );
			}
		}
	}
}
