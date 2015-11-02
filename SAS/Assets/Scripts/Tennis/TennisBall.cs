using UnityEngine;
using System.Collections;

public class TennisBall : MonoBehaviour {

	public TennisControllerGans lastPlayerHit;
	public TennisControllerGans SecondToLastPlayerHit;
	public bool lPHOrigin;
	public bool sLPHOrigin;
	public bool hitGround; //yes | no
	public bool lastGroundHit; //true = north | false = south
	public int hitpoints;

	void Start () {
		hitpoints = 10;
		lastPlayerHit = null;
	}

	void Update() {
		if (hitpoints == 0)
			Destroy (this);
		//Add particle effect to destroy for effect
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag ("Equipment")) {
			SecondToLastPlayerHit = lastPlayerHit;
			lastPlayerHit = other.GetComponent<TennisControllerGans>();
		} else if (other.CompareTag ("NothCourt")) {
			hitpoints--;

			if(lastPlayerHit.OriginSideNorth && SecondToLastPlayerHit!=null && lastGroundHit) {
				//SecondToLastPlayerHit.stats.AddPoint();
			} 
			else if(!lastPlayerHit.OriginSideNorth && SecondToLastPlayerHit!=null && lastGroundHit) {

			}
			else if(lastPlayerHit.OriginSideNorth && SecondToLastPlayerHit==null && lastGroundHit) {

			}
			else if(!lastPlayerHit.OriginSideNorth && SecondToLastPlayerHit==null && lastGroundHit) {
				
			}
			else if(lastPlayerHit.OriginSideNorth && SecondToLastPlayerHit!=null && !lastGroundHit) {
				//SecondToLastPlayerHit.stats.AddPoint();
			} 
			else if(!lastPlayerHit.OriginSideNorth && SecondToLastPlayerHit!=null && !lastGroundHit) {
				
			}
			else if(lastPlayerHit.OriginSideNorth && SecondToLastPlayerHit==null && !lastGroundHit) {
				
			}
			else if(!lastPlayerHit.OriginSideNorth && SecondToLastPlayerHit==null && !lastGroundHit) {
				
			}

		} else if (other.CompareTag ("SouthCourt")) {
			hitpoints--;
			
			if(lastPlayerHit.OriginSideNorth && SecondToLastPlayerHit!=null && lastGroundHit) {

			} 
			else if(!lastPlayerHit.OriginSideNorth && SecondToLastPlayerHit!=null && lastGroundHit) {
				
			}
			else if(lastPlayerHit.OriginSideNorth && SecondToLastPlayerHit==null && lastGroundHit) {
				
			}
			else if(!lastPlayerHit.OriginSideNorth && SecondToLastPlayerHit==null && lastGroundHit) {
				
			}
			else if(lastPlayerHit.OriginSideNorth && SecondToLastPlayerHit!=null && !lastGroundHit) {

			} 
			else if(!lastPlayerHit.OriginSideNorth && SecondToLastPlayerHit!=null && !lastGroundHit) {
				
			}
			else if(lastPlayerHit.OriginSideNorth && SecondToLastPlayerHit==null && !lastGroundHit) {
				
			}
			else if(!lastPlayerHit.OriginSideNorth && SecondToLastPlayerHit==null && !lastGroundHit) {
				
			}
		}
	}
}
