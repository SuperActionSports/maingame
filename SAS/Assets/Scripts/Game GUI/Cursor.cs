using UnityEngine;
using System.Collections;
using InControl;
using UnityEngine.UI;

public class Cursor : MonoBehaviour {

	public InputDevice device;
	public float speedMod = 2;
	public bool hasDevice;
	public GameObject liaison;
	public Color colorBelow;
	public GameObject colorPool;
	public GridLayoutGroup colorPoolLayout;
	public Image[] colors;
	public Vector2[] buttonSizes;
	public int pointerID;

	// Use this for initialization
	void Start () {
		speedMod = 2;
		//colorPoolLayout = colorPool.GetComponent<LayoutGroup>();
		colorPool = GameObject.Find("Color Buttons");
		colorPoolLayout = colorPool.GetComponent<GridLayoutGroup>();
		colors = colorPool.GetComponentsInChildren<Image>();
		buttonSizes = new Vector2[colors.Length];
		int i = 0;
		foreach (Image c in colors)
		{
			buttonSizes[i].x = c.rectTransform.position.x;
			buttonSizes[i].y = c.rectTransform.position.x +   
			i++;
		}
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.position += new Vector3(device.Direction.X*speedMod,device.Direction.Y*speedMod,0);
		hasDevice = !(device == null);
		//colorBelow = 
		
	}
	
	public void OhHai()
	{
		Debug.Log("Hi there sailor");
	}
}
