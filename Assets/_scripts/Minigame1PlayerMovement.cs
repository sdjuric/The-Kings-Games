using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minigame1PlayerMovement : MonoBehaviour {
	private Vector2 moveObject;
	private Rigidbody2D rb;
	//Variable to keep track if player should keep mobing or not
	private bool allowMotion;

	public bool readInput = false;

	public bool won = false;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();
		//moveObject = new Vector2 (25,0);
		moveObject = new Vector2 (50,0);
		allowMotion = true;
	}

	// Update is called once per frame
	void Update () {

		//Only moving the box in x direction
		if (allowMotion) {
			if (readInput && Input.GetKeyUp (KeyCode.Space)) {
				rb.AddForce (moveObject, ForceMode2D.Force);
			}
		} else {
			rb.velocity = new Vector2 (0,0);
		}
	}

	//This method detects player collison with the Finish Line and stops them.
	void OnTriggerEnter2D(Collider2D other){
		if(other.tag == "FinishedMinigame1"){
			//Debug.Log ("asdasdasdasqwe qw eqw q ");
			allowMotion = false;
			won = true;
		}
	}

	// // // // // // // // // // //
	// // // // // // // // // // //
	// // // // // // // // // // //
	// // // // // // // // // // //
	// // // // // // // // // // //
	// // // // // // // // // // //
	// // // // // // // // // // //
	// // // // // // // // // // //

	//only use the following function to send info back and forth
	void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {

		//Debug.Log (waiting);

		if (stream.isWriting) {

			//this is my player on my screen
			//for example: stream.SendNext(transform.position);

			stream.SendNext (this.transform.localPosition.x);
			stream.SendNext (this.transform.localPosition.y);
			stream.SendNext (this.transform.localPosition.z);

		} else {

			//this is my player on others computer
			//for example: transform.localScale = (Vector3)stream.ReceiveNext();

			float x = (float)stream.ReceiveNext ();
			float y = (float)stream.ReceiveNext ();
			float z = (float)stream.ReceiveNext ();

			this.transform.position =  new Vector3 (x,y,z);
			//Mathf.Lerp(this.transform.localPosition.x,x,0.5f),
			//Mathf.Lerp(this.transform.localPosition.y,y,0.5f),
			//Mathf.Lerp(this.transform.localPosition.z,z,0.5f)
		}
	}
}
