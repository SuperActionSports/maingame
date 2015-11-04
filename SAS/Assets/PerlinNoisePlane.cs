using UnityEngine;
using System.Collections;

public class PerlinNoisePlane : MonoBehaviour {

	public float power = 2.0f;
	public float scale = 0.25f;
	private Vector2 noiseOrigin = new Vector2(0f, 0f);
	
	void Start () 
	{
		Noise();
	}
	
	void Noise() {
		noiseOrigin = new Vector2(Random.Range (0.0f, 100.0f), Random.Range (0.0f, 100.0f));
		MeshFilter mf = GetComponent<MeshFilter>();
		Vector3[] vertices = mf.mesh.vertices;
		for (int i = 0; i < vertices.Length; i++) 
		{    
			float xCoord = noiseOrigin.x + vertices[i].x  * scale;
			float yCoord = noiseOrigin.y + vertices[i].z  * scale;
			vertices[i].y = (Mathf.PerlinNoise (xCoord, yCoord) - 0.5f) * power; 
		}
		mf.mesh.vertices = vertices;
		mf.mesh.RecalculateBounds();
		mf.mesh.RecalculateNormals();
	}
}