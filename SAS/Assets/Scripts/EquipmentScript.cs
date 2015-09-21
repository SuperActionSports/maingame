using UnityEngine;
using System.Collections;

public class EquipmentScript : MonoBehaviour {
    

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {

	}

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController disco = other.GetComponent<PlayerController>();
            if (disco.alive)
            {
                disco.Kill(transform.position*-1);
            }
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Vector3 sweet = new Vector3(transform.position.x, transform.localPosition.y, transform.position.z);
        Gizmos.DrawLine(sweet, transform.forward * 1.5f);
    }
}
