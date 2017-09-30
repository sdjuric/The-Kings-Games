using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class diceStop : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnMouseDown() 
	{
		// Hide this to display the final value.
		gameObject.GetComponent<Renderer>().enabled = false;
		// load all frames in fruitsSprites array
		diceSprites = Resources.LoadAll<Sprite>("fruits");
	}

	void Start ()
	{

	}
}
