using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AdvancedPlayerColorHueSelection : MonoBehaviour {

	public Slider player_slider;
	public Image splat;
	public float playerR;
	public float playerG;
	public float playerB;
	public float sliderVal;
	public int times;
	public float remainder;
	// Use this for initialization
	void Start () {
		player_slider.maxValue = 1785;
		player_slider.minValue = 0;
		player_slider.wholeNumbers = true;
	}
	
	// Update is called once per frame
	void Update () {
		player_Color (player_slider.value);
		splat.color = new Color (playerR, playerG, playerB);
		sliderVal = (int)player_slider.value;
	}

	void player_Color(float slideNum){
		times = (int)(slideNum / 255f);
		remainder = slideNum % 255;



		switch (times) {
		case 0:
			if(remainder==0) {
				playerR = playerG = playerB = 0;
			}
			playerR = remainder/255f;
			playerG = 0;
			playerB = 0;
			break;
		case 1:
			playerR = 1;
			playerG = remainder/255f;
			playerB = 0;
			break;
		case 2:
			playerR = 1 - remainder/255f;
			playerG = 1;
			playerB = 0;
			break;
		case 3:
			playerR = 0;
			playerG = 1;
			playerB = remainder/255f;
			break;
		case 4:
			playerR = 0;
			playerG = 1 - remainder/255f;
			playerB = 1;
			break;
		case 5:
			playerR = remainder/255f;
			playerG = 0;
			playerB = 1;
			break;
		case 6:
			playerR = 1;
			playerG = 0;
			playerB = 1 - remainder/255f;
			break;
		case 7:
			playerR = playerG = playerB = 1;
			break;
		/*default:
			playerR = 255;
			playerG = 0;
			playerB = 0;
			break;*/

		}
	}


}
