using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoardController : MonoBehaviour {

	public Text gameText;
	public string gameMessage;
	public Node[] tiles = new Node[32];
	
	// Board control variables
	public int playerNum; // Player number of the current holder of the script
	public int currentGoingPlayer; // Number of the player currently going
	public int lobbySize; // Current lobby size(player nums should be between 1 and lobbySize)
	public bool displayButton; // If the dice button is displayed or not
	public int boardState; // State of the board. 0 = dice rolling state. 1 = minigame1, 2 = minigame2

	public GameObject diceA;
	public GameObject diceB;
	public GameObject diceC;

	public GameObject[] miniGamesCanvasss;

	public GameObject displayMSG;

	// current player state
	public enum states { 
		waiting = 0,
		rolling,
		moving,
		ending
	};


	private int currentState = (int)states.waiting;

	private float[] positionx = {-0.1f,0.1f,0.1f,-0.1f};
	private float[] positiony = {0.1f,0.1f,-0.1f,-0.1f};

	//private bool myTurn = false;
	//private bool lastTurn = false;

	// Use this for initialization
	void Start () {
		//connect ();
		spawnPlayer ();

		this.gameText = gameObject.GetComponentInChildren<Text> ();
		this.gameMessage = "Player 1's Turn";
		

		this.gameText.text = this.gameMessage;
		
		// Temp stuff for compliation
		//this.lobbySize = 4;
		//this.currentGoingPlayer = 1;
		//this.playerNum = 1;
		this.boardState = 0;
		this.displayButton = false;
	}

	private void spawnPlayer(){
		GameObject player = PhotonNetwork.Instantiate ("playerObject", Vector3.zero, Quaternion.identity, 0);
		//player.GetComponent<RectTransform> ().position = Vector3.zero;
		//player.GetComponent<PlayerController> ().board = this.gameObject;
		player.GetComponent<PlayerController> ().xOffset = positionx [(int)PhotonNetwork.player.AllProperties ["playerID"] - 1];
		player.GetComponent<PlayerController> ().yOffset = positiony [(int)PhotonNetwork.player.AllProperties ["playerID"] - 1];

		//player.GetComponent<PlayerController> ().init ();
	}
	
	// Update is called once per frame
	void Update () {

		// use this from now on, if(myTurn) do stuff; else do nothing and wait for others;
		bool myTurn = ((int)PhotonNetwork.player.AllProperties ["playerID"]==(int)PhotonNetwork.player.AllProperties ["currentTurn"])
			&& (PhotonNetwork.playerList.Length==(int)PhotonNetwork.player.AllProperties ["maxNumPlayers"]);;
		//lastTurn = ((int)PhotonNetwork.player.AllProperties ["maxNumPlayers"]==(int)PhotonNetwork.player.AllProperties ["currentTurn"]);

		this.updateText ( "Player "+((int)PhotonNetwork.player.AllProperties["currentTurn"]).ToString()+"'s Turn!" );

		//string s = "";
		//foreach (PhotonPlayer pl in PhotonNetwork.playerList) {
		//	s+="Player: " + pl.AllProperties ["playerID"].ToString ()+" "+pl.AllProperties ["currentTurn"].ToString();
		//}
		//Debug.Log (s);


		if (myTurn && !displayButton && boardState == 0) {
			// Stefan: Display a button. Button clickable, calls dice mover. Calls movePlayer within here with the value to move
			displayButton = true;
			switch (currentState) {
				
			case (int)states.waiting:
				currentState = (int)states.rolling;
				break;
			case (int)states.rolling:
				// do your dice animations and stuff here 
				// get the value, and put it in an int here 
				break;
			case (int)states.moving:
				break;
			
			
			default:
				break;
			

			}


		} else {
			// not my turn, wait


		}

	}


	void updateText (string text){
		this.gameText.text = text;
	}
	
	// Called with the button completing its job
	/*
	void movePlayer(int steps) {
		// Move the player for x steps, processing any special stuff
		// Hide the button for the client
		displayButton = false;
		if (currentGoingPlayer != lobbySize) {
			currentGoingPlayer++;
		} else {
			currentGoingPlayer = 1;
			boardState = 1;
			beginGame();
		}
	}*/

	void endGame() {
		boardState = 0;
	}
	
	void beginGame() {
		// Start a game
	}


	public Node getNode(int nodeNum){
		//Debug.Log ("grabbing node " + nodeNum);
		
		return this.tiles [nodeNum];
	}

	// ask nick or ahmed before you modifie the network code
	private void connect(){
		PhotonNetwork.ConnectUsingSettings ("The King's Games");
	}
	void OnJoinedLobby(){
		createRoom ();
	}
	private void createRoom (){
		string target = "";
		target += Random.Range (0,10);
		target += Random.Range (0,10);
		target += Random.Range (0,10);
		RoomOptions ro = new RoomOptions();
		ro.MaxPlayers = 1;
		PhotonNetwork.CreateRoom(target, ro, null);
	}
	void OnJoinedRoom()
	{
		PhotonNetwork.player.SetCustomProperties (new ExitGames.Client.Photon.Hashtable(){{"playerID", 1},{"maxNumPlayers", 1},{"currentTurn", 1}});
		spawnPlayer ();
	}

	//void OnGUI(){
	//	GUILayout.Label (PhotonNetwork.connectionStateDetailed.ToString());
	//}
}
