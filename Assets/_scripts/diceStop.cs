using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class diceStop : MonoBehaviour {

	public GameObject diceC;
	public Sprite[] diceSpritePics;
	public int value;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnMouseDown() 
	{
		// Set our random value and update the static dice
		value = Random.Range (0, 6);
		diceC.SetActive (true);
		//diceC.GetComponent<Renderer>().enabled = true;
		diceC.GetComponent<SpriteRenderer> ().sprite = diceSpritePics [value];

		// Hide this dice
		//gameObject.GetComponent<Renderer>().enabled = false;
		gameObject.SetActive (false);
	}

	// other classes call this
	int getValue(){
		return this.value + 1;
	}
}
