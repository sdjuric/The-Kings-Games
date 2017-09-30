using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class horseController : MonoBehaviour {

	//public int wallet; 
	private float speed = 0.1f;

	public Rigidbody2D rb;
	//public RectTransform transform;

	//public GameObject coin;

	public bool readInput = false;
	private GameObject miniboard;

	public bool won = false;

	private bool startCounting = false;

	// Use this for initialization
	void Start () {

		if (rb == null) {
			this.rb = this.gameObject.GetComponent<Rigidbody2D> ();
		}
		//if (this.transform == null) {
		//	this.transform = GetComponent<RectTransform> ();
		//}
		// default speed
		//if (this.speed == 0) {
		//	this.speed = 0.1f;
		//}
		//if (this.wallet == 0) {
			// this initializes it, yes it looks weird
		//	this.wallet = 0;
		//}
		StartCoroutine("Fade");
	}
	
	// Update is called once per frame
	void Update () {
		if(readInput)
			move ();	
		
		float xBounce = 1;
		float yBounce = 1;

		// check for boundries
		float x = 1f * this.transform.localPosition.x ;
		float y = 1f * this.transform.localPosition.y ;

		if (x > 9.0f || x < -9.0f || y > 5.0f || y < -5.0f) {
			//if (this.gameObject != null) {
				PhotonNetwork.Destroy (this.gameObject);
				//removeOnefromALl ();
				//this.gameObject.SetActive (false);
			//}
		}

		//won = (GameObject.FindGameObjectsWithTag("horse").Length==1) && startCounting;
		this.transform.localPosition = new Vector3 (x *xBounce , y * yBounce , -1);
	}

	void LateUpdate (){


	}
	public float maxSpeed = 20f;
	void FixedUpdate()
	{
		if(rb.velocity.magnitude > maxSpeed)
		{
			rb.velocity = rb.velocity.normalized * maxSpeed;
		}
	}

	void move(){

		float xMove = Input.GetAxis ("Horizontal");
		float yMove = Input.GetAxis ("Vertical");

		Vector2 movement = new Vector2 (
			xMove * this.speed  ,
			yMove * this.speed                 	
		);


		this.rb.transform.Translate (movement);
		//Debug.Log (x + " " + y);

	}

	//void OnCollisionEnter2D(Collision2D otherObject) {
		//if (otherObject.gameObject.tag == "coin") {
			//Debug.Log ("hit!");
			//Destroy (otherObject.gameObject);

			// add funds
			//addWallet (1);
		//}
		// spawn for later
		//else if (otherObject.gameObject.tag == "horse") {
			//spawnCoin ();
		//}

	//}

	//int getWallet(){
		//return this.wallet;
	//}
		
	//void addWallet(int amount){
	//	this.wallet += amount;
	//}

	//void spawnCoin(){

		//for (int i = 0; i < 2; i++) {
			//int x = Random.Range (-1, 1);
			//int y = Random.Range (-1, 1);

		//GameObject Coin = Instantiate (coin, 
			//new Vector3 (x * 3.0f, y  * 3.0f, -1),
			//Quaternion.identity) as GameObject;
		//Coin.transform.SetParent(this.transform);
		//}

	//}

	IEnumerator Fade() {
		//moveSpaces (1);
		yield return new WaitForSeconds (2.0f);
		//StartCoroutine("Fade");
		startCounting = true;
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

		//Debug.Log ();

		if (stream.isWriting) {

			//this is my player on my screen
			//for example: stream.SendNext(transform.position);

			stream.SendNext (this.transform.localPosition);

		} else {

			//this is my player on others computer
			//for example: transform.localScale = (Vector3)stream.ReceiveNext();

			this.transform.position = (Vector3)stream.ReceiveNext ();

			//Vector3.Lerp(this.transform.position, (Vector3)stream.ReceiveNext (), 0.2f);

			//Mathf.Lerp(this.transform.localPosition.x,x,0.5f),
			//Mathf.Lerp(this.transform.localPosition.y,y,0.5f),
			//Mathf.Lerp(this.transform.localPosition.z,z,0.5f)
		}
	}

}
