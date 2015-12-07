using UnityEngine;
using System.Collections;

public class PerlinNoisePlane : MonoBehaviour {

	public float power = 2.0f;
	public float scale = 0.25f;
	public GameObject hole;
	private Vector2 noiseOrigin = new Vector2(0f, 0f);
	
	void Start () 
	{
		Noise();
	}
	
	void Noise() {
		noiseOrigin = new Vector2(Random.Range (0.0f, 100.0f), Random.Range (0.0f, 100.0f));
		MeshFilter mf = GetComponent<MeshFilter>();
		Vector3[] vertices = mf.mesh.vertices;
		//float maxCoord = transform.position.Scale.x * 10;

		int holePos = Mathf.FloorToInt(Random.Range (0, vertices.Length));
		for (int i = 0; i < vertices.Length; i++) 
		{    
			float xCoord = noiseOrigin.x + vertices[i].x  * scale;
			float zCoord = noiseOrigin.y + vertices[i].z  * scale;
			vertices[i].y = (Mathf.PerlinNoise (xCoord, zCoord) - 0.5f) * power;
		}
		mf.mesh.vertices = vertices;
		mf.mesh.RecalculateBounds();
		mf.mesh.RecalculateNormals();
		GetComponent<MeshCollider>().sharedMesh = mf.mesh;
	}

	public void ResetHolePosition() {
		hole.transform.position = new Vector3(transform.position.x, 2f, transform.position.z);
		hole.GetComponent<SphereCollider>().isTrigger = false;
		Rigidbody rb = hole.GetComponent<Rigidbody>();
		rb.isKinematic = false;
		rb.useGravity = true;
		rb.velocity = Vector3.zero;
		rb.constraints = RigidbodyConstraints.None;
	}
}