using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class PaintSplatter : MonoBehaviour {

	public static PaintSplatter Instance;

	public Transform PaintSprite;
	public Text p1score;
	public Text p2score;
	public Text p3score;
	public Text p4score;

	private int minSplash = 0;
	private int maxSplash = 0;
	private float splashRange = 2f;

	private float minScale = 0;
	private float maxScale = 2;

	private bool characterHit = false;
	private Color color;
	private int count;

	// DEBUG TOOLS
	private bool mDrawDebug;
	private Vector3 mHitPoint;
	private List<Ray> mRaysDebug = new List<Ray>();


	// Use this for initialization
	void Awake () 
	{
		if (Instance != null) {
			Debug.Log ("More than one painter has been instantiated in the scene");
		}
		Instance = this;

		if(PaintSprite == null)
		{
			Debug.Log("Missing paintSprite Prefab");
		}
		int count = 0;
		SetCountText ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetMouseButton (0)) 
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit[] hits;

			hits = Physics.RaycastAll(ray);

			if(hits.Length > 0)
			{
				Debug.Log ("hits is greater than 0: " + hits[0].collider.tag);
				if(hits[0].collider.tag == "CapsuleC")
				{
					color = Color.cyan;
				}
				if(hits[0].collider.tag == "CapsuleM")
				{
					color = Color.magenta;
				}
				if(hits[0].collider.tag == "CapsuleY")
				{
					color = Color.yellow;
				}
				if(hits[0].collider.tag == "CapsuleB")
				{
					color = Color.black;
				}
				Debug.Log ("hit the character");
				characterHit = true;
				if(hits.Length > 1)
				{
					if(characterHit)
					{
						Debug.Log ("Paint!");
						Paint (hits[1].point + hits[1].normal * (splashRange/4f), color);
						count++;
						SetCountText();
						characterHit = false;
					}
				}
			}
		}
	}

	public void Paint(Vector3 location, Color color)
	{
		mHitPoint = location;
		mRaysDebug.Clear ();
		mDrawDebug = true;

		int n = -1;
		int drops = Random.Range (minSplash, maxSplash);
		RaycastHit hit;

		while (n <= drops) {
			n++;
			var fwd = transform.TransformDirection(Random.onUnitSphere * splashRange);
			mRaysDebug.Add (new Ray(location, fwd));

			if(Physics.Raycast(location, fwd, out hit, splashRange)){
				var paintSplat = GameObject.Instantiate(PaintSprite, hit.point, Quaternion.FromToRotation(Vector3.back, hit.normal)) as Transform;
				SpriteRenderer renderer = paintSplat.GetComponent<SpriteRenderer>();
				renderer.material.color = color;
				var scaler = Random.Range(minScale, maxScale);
				paintSplat.localScale = new Vector3(paintSplat.localScale.x * scaler,
				                                    paintSplat.localScale.y * scaler,
				                                    paintSplat.localScale.z);

				var rater = Random.Range(0, 180);
				paintSplat.transform.RotateAround(hit.point, hit.normal, rater);

				Destroy(paintSplat.gameObject, 15000);
			}
		}
	}

	void OnDrawGizmos()
	{
		if (mDrawDebug) {
			Gizmos.DrawSphere (mHitPoint, 0.2f);
			foreach(var r in mRaysDebug)
			{
				Gizmos.DrawRay(r);
			}
		}
	}

	void SetCountText()
	{
		p1score.text = "Score: " + count.ToString ();
		p2score.text = "Score: " + count.ToString ();
		p3score.text = "Score: " + count.ToString ();
		p4score.text = "Score: " + count.ToString ();
	}
}
