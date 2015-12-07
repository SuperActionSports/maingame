using UnityEngine;
using System.Collections;

public class AudienceController : MonoBehaviour {

	public GameObject audiencePrefabA;
	public GameObject audiencePrefabB;
	private GameObject[] wholeAudience;
	private Animator[] crowdMovements;
	private Vector3[] audienceCoordinates;
	private Vector3[] audienceRotation;
	private float scale;
	private AudioSource[] crowdNoise;	// [0] is crowd drone sound [1] is crowd cheer
	private string sport;
	private int playerCount;
	private Color[] colors;

	/// <summary>
	/// Creates appropriate audience and puts all the sounds and animations into action.  Should only be called by Wizard.
	/// </summary>
	/// <param name="sName">The name of the sport</param>
	/// <param name="players">The array of players</param>
	public void Initialize (string sName, Player[] players) {
		sport = sName.ToLower();
		playerCount = players.Length;
		colors = new Color[playerCount];
		for (int i = 0; i < playerCount; i++) { colors [i] = players [i].color;	}

		wholeAudience = new GameObject[100];
		crowdMovements = new Animator[100];
		audienceCoordinates = new Vector3[100];
		audienceRotation = new Vector3[100];
		LoadAudienceCoordinates ();
		LoadAudienceRotation ();
		PopulateAudience ();

		crowdNoise = GetComponents<AudioSource> ();
		crowdNoise[0].loop = true;
		crowdNoise [0].volume = 0.5f;
		crowdNoise [1].loop = false;
		crowdNoise[0].Play ();
	}

	/// <summary>
	/// Populates the audience using playerCount to determine number of varying colors.
	/// </summary>
	private void PopulateAudience () {
		int currentColor = 0;

		for (int i = 0; i < wholeAudience.Length; i++) {
			wholeAudience[i] = (GameObject) Instantiate(audiencePrefabA, audienceCoordinates[i], Quaternion.identity);
			wholeAudience[i].transform.Rotate(audienceRotation[i]);
			crowdMovements[i] = wholeAudience[i].GetComponent<Animator>();

			wholeAudience[i].GetComponentInChildren<Renderer>().material.color = colors[currentColor++];
			if (currentColor + 1 > playerCount) {currentColor = 0;}

			i++;

			wholeAudience[i] = (GameObject) Instantiate(audiencePrefabB, audienceCoordinates[i], Quaternion.identity);
			wholeAudience[i].transform.Rotate(audienceRotation[i]);
			crowdMovements[i] = wholeAudience[i].GetComponent<Animator>();

			wholeAudience[i].GetComponentInChildren<Renderer>().material.color = colors[currentColor++];
			if (currentColor + 1 > playerCount) {currentColor = 0;}
		}

		AdjustScale ();
	}

	/// <summary>
	/// Activate small audience movement and sound.  For use with player kills.
	/// </summary>
	public void SmallCheer () {
		for (int i = 0; i < 72; i++) {
			crowdMovements[i].SetTrigger("Kill");
		}
		if(crowdNoise[0].volume > 0.5f)	{
			crowdNoise[0].volume -= 0.05f * Time.deltaTime;
		}
	}

	/// <summary>
	/// Activate large audience movement and sound.  For use with goals scored.
	/// </summary>
	public void LargeCheer () {
		for (int i = 0; i < 72; i++) {
			crowdMovements[i].SetTrigger("Goal");
		}
		crowdNoise [1].Play ();
	}

	/// <summary>
	/// Removes all off-camera audience members to reduce load.
	/// </summary>
	public void DiminishCrowd () {
		for (int i = 50; i < 100; i++) {
			if (wholeAudience[i] != null) { wholeAudience[i].SetActive(false); }
		}
		if (sport == "tennis") {
			for (int i = 11; i < 18; i++) { wholeAudience[i].SetActive(false); }
		}
	}

	/// <summary>
	/// Adjusts the scale of the audience members to account for differences in game stadium scaling.
	/// </summary>
	private void AdjustScale () {
		foreach (GameObject member in wholeAudience) { member.transform.localScale *= scale; }
	}

	/// <summary>
	/// Sets up array with rotation values for all the audience members in the stadium for the current event.
	/// </summary>
	private void LoadAudienceRotation () {
		for (int i = 0; i < 100; i++) {
			switch (sport) {
			case "golf":
			case "tennis":
				if (i <= 9) { audienceRotation[i]= new Vector3 (0, 90, 0); } 
				else if (i >= 10 && i <= 17) { audienceRotation[i] = new Vector3 (0, 0, 0); } 
				else if (i >= 18 && i <= 27) { audienceRotation[i] = new Vector3 (0, -90, 0); } 
				else if (i >= 28 && i <= 35) { audienceRotation[i] = new Vector3 (0, 180, 0); } 
				else if (i >= 50 && i <= 59) { audienceRotation[i] = new Vector3 (0, 90, 0); } 
				else if (i >= 60 && i <= 67) { audienceRotation[i] = new Vector3 (0, 0, 0); } 
				else if (i >= 68 && i <= 77) { audienceRotation[i] = new Vector3 (0, -90, 0); } 
				else if (i >= 78 && i <= 85) { audienceRotation[i] = new Vector3 (0, 180, 0); }
				break;
			case "hockey":
				if (i >= 30 && i <= 49) { audienceRotation[i] = new Vector3 (0, 90, 0); }
				else if (i >= 50 && i <= 79) { audienceRotation[i] = new Vector3 (0, 180, 0); }
				else if (i >= 80 && i <= 99) { audienceRotation[i] = new Vector3 (0, -90, 0); }
				break;
			case "fencing":
				break;
			case "baseball":
				break;
			default:
				break;
			}
		}
	}

	/// <summary>
	/// Sets up array with coordinates for all audience locations in stadium of currently active event.
	/// </summary>
	private void LoadAudienceCoordinates () {
		switch (sport)	{
		case "golf" :
		case "tennis" :
			audienceCoordinates [0] = new Vector3 (14.6f, 2.1f, 0.0f);
			for (int i = 1; i < 10; i++) { audienceCoordinates[i] = audienceCoordinates[i - 1] + new Vector3 (1.47f,1.49f,0.0f); }

			audienceCoordinates [10] = new Vector3 (0.0f, 6.6f, 17.0f);
			for (int i = 11; i < 18; i++) { audienceCoordinates[i] = audienceCoordinates[i - 1] + new Vector3 (0.0f,1.49f,1.47f); }

			for (int i = 0; i < 18; i++) {
				int j = i + 18;
				audienceCoordinates[j] = audienceCoordinates[i]; //* new Vector3 (-1.0f, 1.0f, -1.0f); 
				audienceCoordinates[j].x *= -1.0f;
				audienceCoordinates[j].z *= -1.0f;
			}

			for (int i = 0; i < 10; i++) {
				int j = i + 50;
				audienceCoordinates[j] = audienceCoordinates[i] + new Vector3 (7.4f, 16.3f, 0.0f);
			}				
			for (int i = 10; i < 18; i++) {
				int j = i + 50;
				audienceCoordinates[j] = audienceCoordinates[i] + new Vector3 (0.0f, 14.8f, 7.4f);
			}
			for (int i = 18; i < 28; i++) {
				int j = i + 50;
				audienceCoordinates[j] = audienceCoordinates[i] + new Vector3 (-7.4f, 16.3f, 0.0f);
			}
			for (int i = 28; i < 36; i++) {
				int j = i + 50;
				audienceCoordinates[j] = audienceCoordinates[i] + new Vector3 (0.0f, 14.8f, -7.4f);
			}

			scale = 1.0f;
			break;
		case "hockey" :
			audienceCoordinates[0] = new Vector3 (0.0f, 0.25f, 4.75f);
			for (int i = 1; i < 10; i++) { audienceCoordinates[i] = audienceCoordinates[i - 1] + new Vector3 (0.0f, 0.5f, 0.5f); }
			for (int i = 0; i < 10; i++) {
				audienceCoordinates[i + 10] = audienceCoordinates[i] + new Vector3 (-6.0f, 0.0f, 0.0f);
				audienceCoordinates[i + 20] = audienceCoordinates[i] + new Vector3 (6.0f, 0.0f, 0.0f);
			}

			audienceCoordinates[30] = new Vector3 (8.5f, 0.25f, 2.5f);
			for (int i = 31; i < 40; i++) { audienceCoordinates[i] = audienceCoordinates[i - 1] + new Vector3 (0.5f, 0.5f, 0.0f); }
			for (int i = 30; i < 40; i++) { audienceCoordinates[i + 10] = audienceCoordinates[i] + new Vector3 (0.0f, 0.0f, -5.0f); }

			for (int i = 0; i < 50; i++) {
				audienceCoordinates[i + 50].x = audienceCoordinates[i].x * -1.0f;
				audienceCoordinates[i + 50].y = audienceCoordinates[i].y;
				audienceCoordinates[i + 50].z = audienceCoordinates[i].z * -1.0f;
			}

			scale = 0.5f;
			break;
		case "fencing" :
			// audience coordinates for fencing go here
			break;
		case "baseball" :
			// audience coordinates for baseball go here
			break;
		default:
			break;
		}
	}

}
