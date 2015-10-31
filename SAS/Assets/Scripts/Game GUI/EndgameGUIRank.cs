using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EndgameGUIRank : MonoBehaviour {


	public Sprite first;
	public Color firstTint;
	public Sprite second;
	public Color secondTint;
	public Sprite third;
	public Color thirdTint;
	public Sprite worst;
	public Color worstTint;
	private Sprite sprite;
	private Color tint;
	private Image image;
	public int ranking;
	// Use this for initialization
	void Start () {
		image = GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update () {
		//ranking = (int)(Time.time % 4);
		SetSprite(ranking);
		UpdateSprite();
	}
	
	public void SetSprite(int rank)
	{
		switch(rank)
		{
			case(0): 
			{ 
				sprite = first; 
				tint = firstTint;
				break; 
			}
			case(1): 
			{ 
				sprite = second; 
				tint = secondTint;
				break; 
			}
			case(2):
			{
				sprite = third; 
				tint = thirdTint;
				break;
			}
			case (3):
			{
				sprite = worst;
				tint = worstTint;
				break;
			}
		}
	}
	
	private void UpdateSprite()
	{
		image.sprite = sprite;
		image.color = tint;
	}
}
