using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Photon.MonoBehaviour {

	// dont forget to declare!!!
	private GameObject board;
	private BoardController bc;

	public Sprite[] images;
	public GameObject imageObject;

	private float x = 0;
	private float y = 0;

	//offsets for the tiles, will be added later 
	public float xOffset = 0;
	public float yOffset = 0;

	private int moneyCount;
	private int nodeNumber = 0;
	//private Transform position;

	private bool myTurn = false;
	private bool lastTurn = false;

	private bool activatedA = false;

	private bool waiting = false;

	private int selectedBoard = 0;

	private bool gameOver = false;

	private bool doneMini1 = false;
	private GameObject innerPlayer1 = null;
	private bool doneMini2 = false;
	private GameObject innerPlayer2 = null;

	private bool startCounting = false;

	// Use this for initialization
	void Start () {
		init ();
	}

	public void init(){
		board = GameObject.Find ("BoardCanvas");
		bc = board.GetComponent<BoardController>();

		//this.position = this.transform;

		if (this.nodeNumber == 0) {
			Debug.Log ("No Node, placing player at start");

			this.nodeNumber = 0;
		}

		this.x = this.bc.getNode(this.nodeNumber).getX();

		this.y = this.bc.getNode(this.nodeNumber).getY();


		this.setPosition ();

		//	StartCoroutine("Fade");
		this.moneyCount = 0;
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log ("moving to " + this.x + " " +this.y);

		//imageObject.GetComponent<SpriteRenderer>().sprite = images[(int)PhotonNetwork.player.AllProperties ["playerID"]-1];

		myTurn = ((int)PhotonNetwork.player.AllProperties ["playerID"]==(int)PhotonNetwork.player.AllProperties ["currentTurn"])
			&& (PhotonNetwork.playerList.Length==(int)PhotonNetwork.player.AllProperties ["maxNumPlayers"]);
		lastTurn = ((int)PhotonNetwork.player.AllProperties ["maxNumPlayers"]==(int)PhotonNetwork.player.AllProperties ["currentTurn"]);
		waiting = (bool)PhotonNetwork.player.AllProperties["waiting"];

		if (myTurn && !waiting && !gameOver) {
			if (!activatedA) {
				bc.diceA.SetActive (true);
				activatedA = true;
			}
			if (bc.diceC.activeInHierarchy) {
				int dis = bc.diceB.GetComponent<diceStop> ().value+1;
				//Debug.Log ("moved: "+dis);
				moveSpaces (dis);
				bc.diceC.SetActive (false);

				if (!lastTurn) {
					endMyTurn ();
				} else {
					//waiting = true;
					makeThemAllWait();
					//StartCoroutine("Fade");
					selectedBoard = Random.Range (0,2);
					GetComponent<PhotonView>().RPC ("loadMiniGame", PhotonTargets.AllViaServer,selectedBoard);
				}
			}
		}


		//////

		if (!doneMini1 && innerPlayer1 && innerPlayer1.GetComponent<Minigame1PlayerMovement>().won) {
			doneMini1 = true;
			GetComponent<PhotonView>().RPC ("loadMainBoard", PhotonTargets.AllViaServer, 0);
		}

		if (!doneMini2 && innerPlayer2 && startCounting && (GameObject.FindGameObjectsWithTag("horse").Length==1)) {
			doneMini2 = true;
			GetComponent<PhotonView>().RPC ("loadMainBoard", PhotonTargets.AllViaServer, 1);
		}

		//imageObject.GetComponent<SpriteRenderer> ().enabled = !waiting;
		//Debug.Log (waiting);
		//this.imageObject.SetActive(!waiting);
	}

	void setPosition(){
		//Debug.Log ("moving to " + this.x + " " +this.y);
		GetComponent<RectTransform>().localPosition = new Vector3 
			( 
				(float)this.x + this.xOffset ,
				(float)this.y + this.yOffset ,
			    -1f
			);

	}

	// changes this objects money count 
	void changeBalence(int amount){
		this.moneyCount += amount;
	}

	int getMoney(){
		return this.moneyCount;
	}

	// changes this objects node position by [spaces]
	void moveSpaces(int spaces){
		
		this.nodeNumber += spaces;

		if (this.nodeNumber >= bc.tiles.Length - 1) {
			this.nodeNumber = bc.tiles.Length - 1;
			GetComponent<PhotonView>().RPC ("announceWinner", PhotonTargets.AllViaServer,PhotonNetwork.player.AllProperties["playerID"]);
		}
			
		this.x = this.bc.getNode(this.nodeNumber).getX();
		this.y = this.bc.getNode(this.nodeNumber).getY();

		this.setPosition ();

	} 

	//end my turn
	private void endMyTurn(){

		activatedA = false;

		if (myTurn) {
			int maxi = (int)PhotonNetwork.player.AllProperties ["maxNumPlayers"];
			int curT = (int)PhotonNetwork.player.AllProperties ["currentTurn"];
			int nextTurn = 0;

			foreach (PhotonPlayer pl in PhotonNetwork.playerList) {
				int id = (int)pl.AllProperties ["playerID"];
				if (maxi != curT)
					nextTurn = curT;
				pl.SetCustomProperties (new ExitGames.Client.Photon.Hashtable () {
					{ "playerID", id },
					{ "maxNumPlayers", maxi },
					{
						"currentTurn",
						nextTurn + 1
					},
					{"waiting", false}
				});
			}
		}
	}

	private void makeThemAllWait(){
		foreach (PhotonPlayer pl in PhotonNetwork.playerList) {
			int id = (int)pl.AllProperties ["playerID"];
			int maxi = (int)PhotonNetwork.player.AllProperties ["maxNumPlayers"];
			int curT = (int)PhotonNetwork.player.AllProperties ["currentTurn"];
			pl.SetCustomProperties (new ExitGames.Client.Photon.Hashtable () {
				{ "playerID", id },
				{ "maxNumPlayers", maxi },
				{
					"currentTurn",
					curT
				},
				{"waiting", true}
			});
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

		if (stream.isWriting) {
			
			//this is my player on my screen
			//for example: stream.SendNext(transform.position);

			stream.SendNext(this.xOffset);
			stream.SendNext(this.yOffset);
			stream.SendNext(this.x);
			stream.SendNext(this.y);

			stream.SendNext(info.sender.AllProperties["playerID"]);
			stream.SendNext(waiting);

			this.imageObject.GetComponent<SpriteRenderer> ().sprite = images[(int)info.sender.AllProperties["playerID"]-1];
			this.imageObject.SetActive( !waiting );

		} else {
			
			//this is my player on others computer
			//for example: transform.localScale = (Vector3)stream.ReceiveNext();

			this.xOffset = (float)stream.ReceiveNext();
			this.yOffset = (float)stream.ReceiveNext();
			this.x = (float)stream.ReceiveNext();
			this.y = (float)stream.ReceiveNext();

			this.imageObject.GetComponent<SpriteRenderer> ().sprite = images[(int)stream.ReceiveNext()-1];
			this.imageObject.SetActive( !(bool)stream.ReceiveNext() );

			this.setPosition ();
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

	IEnumerator Fade() {
		//moveSpaces (1);
		yield return new WaitForSeconds (2.0f);
		//StartCoroutine("Fade");
		//GetComponent<PhotonView>().RPC ("loadMainBoard", PhotonTargets.AllViaServer, selectedBoard);
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

	[PunRPC]
	void loadMiniGame(int boardIndex){
		//this.waiting = true;
		//boardIndex = 1;
		if (!gameOver) {
			doneMini1 = false;
			doneMini2 = false;
			StartCoroutine ("Fade");

			bc.miniGamesCanvasss [boardIndex].SetActive (true);

			if (boardIndex == 0) {
				innerPlayer1 = bc.miniGamesCanvasss [boardIndex].GetComponent<miniGame1Script> ().spawnPlayer ();
			}
			if (boardIndex == 1) {
				innerPlayer2 = bc.miniGamesCanvasss [boardIndex].GetComponent<MiniGame2Controller> ().spawnPlayer ();
			}
		}
	}

	[PunRPC]
	void loadMainBoard(int boardIndex){
		//this.waiting = false;
		//boardIndex = 1;
		innerPlayer1 = null;
		innerPlayer2 = null;
		startCounting = false;

		bc.miniGamesCanvasss[boardIndex].SetActive (false);

		if (boardIndex==0) {
			bc.miniGamesCanvasss [boardIndex].GetComponent<miniGame1Script> ().destroyPlayer();
		}
		if (boardIndex==1) {
			bc.miniGamesCanvasss [boardIndex].GetComponent<MiniGame2Controller> ().destroyPlayer();
		}
			
		if (lastTurn) {
			endMyTurn ();
		}
	}

	[PunRPC]
	void announceWinner(int playerID){
		gameOver = true;
		GameObject o =  Instantiate(bc.displayMSG);
		o.GetComponent<displayMSGScript> ().setString ("Player " + playerID + " won! GG WP");
	}

	[PunRPC]
	void wonMiniGame1(){
		if (lastTurn) {
			GetComponent<PhotonView>().RPC ("loadMainBoard", PhotonTargets.AllViaServer, selectedBoard);
		}
	}

}
