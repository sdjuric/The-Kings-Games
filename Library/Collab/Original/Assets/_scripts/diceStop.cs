using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class diceStop : MonoBehaviour {

	public GameObject diceC;
	public Sprite[] pics;

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
		int result = Random.Range (0, 6);
		//Debug.Log (result);
		diceC.GetComponent<SpriteRenderer> ().sprite = pics [result];
	}
}
