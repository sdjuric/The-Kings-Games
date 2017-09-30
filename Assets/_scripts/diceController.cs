using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class diceController : MonoBehaviour {
    
	//string currentImage;
	//public bool started;
	//Sprite image;

	public GameObject diceB;

	void Awake(){
		// Loading up the spritesheet
		//diceSprites = Resources.LoadAll<Sprite>("diceSheet");
	}

    // Use this for initialization
    void Start () {
		//SpriteRenderer renderer = GetComponent<SpriteRenderer>();
		//renderer.sprite = Resources.Load<Sprite>("diceSheet_4");
		//this.value = 0;
		//currentImage = "diceSheet_0";
		//this.image = GetComponentInChildren<Sprite> ();

    }

    // Update is called once per frame
    void Update () {
		// If we're rolling the dice, it changes sprites on Update (once per frame.)
		//if (this.started) {
			// generate a random number 
			// put it into value 
			// each time you generate a new random number, change the sprite
			// SpriteRenderer renderer = GetComponent<SpriteRenderer>();
			// renderer.sprite = Resources.Load<Sprite>("diceSheet_" + Random.Range(0,6));
		//}
	}

	void OnMouseDown() 
	{
		// Show the transitioning die
		diceB.SetActive (true);
		//diceB.GetComponent<Renderer>().enabled = true;

		// Hide this to start the animation
		//gameObject.GetComponent<Renderer>().enabled = false;
		gameObject.SetActive (false);

	}
		
}
