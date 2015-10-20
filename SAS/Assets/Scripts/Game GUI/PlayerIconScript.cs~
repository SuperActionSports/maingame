using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerIconScript : MonoBehaviour {
	public Transform target;
	private Camera camera;
	private Text text;
	public Color color;
	public float xOffset = 6;
	public float yOffset = 10;
	public float zOffset;
	void Start() {
		camera = Camera.main;
		text = GetComponent<Text>();
		var icon = GetComponentInChildren<Image>();
		text.color = color;
		icon.color = color;
	}
	
	void Update() {
		Vector3 screenPos = camera.WorldToScreenPoint(target.position);
		screenPos += new Vector3(xOffset,yOffset,zOffset);
		transform.position = screenPos;
	}
}