using UnityEngine;
using System.Collections;

public class AudienceController : MonoBehaviour {

	public GameControlLiaison layla;
	public TennisWizard wizard;
	public GameObject audiencePrefab;
	private GameObject[] wholeAudience;
	private Animator[] crowdMovements;
	private Vector3[] audienceCoordinates;
	private AudioSource[] crowdNoise;	// [0] is crowd drone sound [1] is crowd cheer
	private int playerCount;
	private Color[] colors;

	void Start () {
		playerCount = layla.GetNumberOfPlayers();

		colors = new Color[playerCount];
		Player[] players = wizard.players ;
		for (int i = 0; i < playerCount; i++) {
			colors[i] = players[i].color;
		}

		wholeAudience = new GameObject[72];
		crowdMovements = new Animator[72];
		LoadAudienceCoordinates ();
		PopulateAudience ();

		crowdNoise = GetComponents<AudioSource> ();
		crowdNoise[0].loop = true;
		crowdNoise [0].volume = 0.5f;
		crowdNoise [1].loop = false;
		crowdNoise[0].Play ();
	}
	
	void Update () {
	
	}

	/// <summary>
	/// Populates the audience using playerCount to determine number of varying colors.
	/// </summary>
	private void PopulateAudience () {
		int currentColor = 0;

		for (int i = 0; i < wholeAudience.Length; i++) {
			wholeAudience[i] = (GameObject) Instantiate(audiencePrefab, audienceCoordinates[i], Quaternion.identity);
			crowdMovements[i] = wholeAudience[i].GetComponent<Animator>();
			// will need to adjust rotation at end	

			wholeAudience[i].GetComponentInChildren<Renderer>().material.color = colors[currentColor++];
			if (currentColor + 1 > playerCount) {currentColor = 0;}
		}
	}

	/// <summary>
	/// Activate audience movement and sound in reaction to player kill.
	/// </summary>
	public void CryForBlood () {
		for (int i = 0; i < 72; i++) {
			crowdMovements[i].SetTrigger("Kill");
		}
		if(crowdNoise[0].volume > 0.5f)
		{
			crowdNoise[0].volume -= 0.05f * Time.deltaTime;
		}
		for (int i = 0; i < 72; i++) {
			crowdMovements[i].ResetTrigger("Kill");
		}
	}

	/// <summary>
	/// Activate audience movement and sound in reaction to goal scored.
	/// </summary>
	public void CrowdGoesWild () {
		for (int i = 0; i < 72; i++) {
			crowdMovements[i].SetTrigger("Goal");
		}
		crowdNoise [1].Play ();
		for (int i = 0; i < 72; i++) {
			crowdMovements[i].ResetTrigger("Goal");
		}
	}
		
	/// <summary>
	/// Sets up array with coordinates for all audience locations in current stadium.
	/// </summary>
	private void LoadAudienceCoordinates () {
		audienceCoordinates = new Vector3[72];

		audienceCoordinates [0] = new Vector3 (14.6f, 2.1f, 0.0f);
		audienceCoordinates [1] = new Vector3 (16.0f, 3.6f, 0.0f);
		audienceCoordinates [2] = new Vector3 (17.5f, 5.1f, 0.0f);
		audienceCoordinates [3] = new Vector3 (18.9f, 6.6f, 0.0f);
		audienceCoordinates [4] = new Vector3 (20.4f, 8.1f, 0.0f);
		audienceCoordinates [5] = new Vector3 (21.9f, 9.6f, 0.0f);
		audienceCoordinates [6] = new Vector3 (23.3f, 11.0f, 0.0f);
		audienceCoordinates [7] = new Vector3 (24.8f, 12.5f, 0.0f);
		audienceCoordinates [8] = new Vector3 (26.3f, 14.0f, 0.0f);
		audienceCoordinates [9] = new Vector3 (27.8f, 15.5f, 0.0f);

		audienceCoordinates [10] = new Vector3 (0.0f, 6.6f, 17.0f);
		audienceCoordinates [11] = new Vector3 (0.0f, 8.1f, 18.5f);
		audienceCoordinates [12] = new Vector3 (0.0f, 9.6f, 20.0f);
		audienceCoordinates [13] = new Vector3 (0.0f, 11.0f, 21.5f);
		audienceCoordinates [14] = new Vector3 (0.0f, 12.5f, 23.0f);
		audienceCoordinates [15] = new Vector3 (0.0f, 14.0f, 24.5f);
		audienceCoordinates [16] = new Vector3 (0.0f, 15.5f, 25.9f);
		audienceCoordinates [17] = new Vector3 (0.0f, 17.0f, 27.4f);

		for (int i = 0; i < 18; i++) {
			int j = i + 18;
			audienceCoordinates[j] = audienceCoordinates[i]; //* new Vector3 (-1.0f, 1.0f, -1.0f); 
			audienceCoordinates[j].x *= -1.0f;
			audienceCoordinates[j].z *= -1.0f;
		}

		for (int i = 0; i < 10; i++) {
			int j = i + 36;
			audienceCoordinates[j] = audienceCoordinates[i] + new Vector3 (7.4f, 16.3f, 0.0f);
		}

		for (int i = 10; i < 18; i++) {
			int j = i + 36;
			audienceCoordinates[j] = audienceCoordinates[i] + new Vector3 (0.0f, 14.8f, 7.4f);
		}

		for (int i = 18; i < 28; i++) {
			int j = i + 36;
			audienceCoordinates[j] = audienceCoordinates[i] + new Vector3 (-7.4f, 16.3f, 0.0f);
		}

		for (int i = 28; i < 36; i++) {
			int j = i + 36;
			audienceCoordinates[j] = audienceCoordinates[i] + new Vector3 (0.0f, 14.8f, -7.4f);
		}
	}

}
