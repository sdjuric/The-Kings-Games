using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class joinRoomScript : MonoBehaviour {

	public GameObject textInput;
	public GameObject displayMSG;
	public GameObject lobby;
	public GameObject[] menuObjects;
	private GameObject msg;

	// Use this for initialization
	void Start () {
		
	}

	// hide menu when try to connect to lock info from being changed
	public void showAllButtons(){
		foreach (GameObject o in menuObjects) {
			o.SetActive (true);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (msg == null) {
			foreach (GameObject o in menuObjects) {
				o.SetActive (true);
			}
		} else {
			foreach (GameObject o in menuObjects) {
				o.SetActive (false);
			}
		}
	}

	// join a room if the info provided is correct
	public void joinRoom(){

		// display a message of the connection status
		if (msg == null) {
			msg = Instantiate (displayMSG);
		}

		// setup a connection and join the room if the user info is correct
		if (textInput.GetComponent<Text> ().text.Length == 0) {
			msg.GetComponent<displayMSGScript> ().setString ("Please enter a room name!");
		} else {
			if (!PhotonNetwork.insideLobby)
				PhotonNetwork.ConnectUsingSettings ("The King's Games");
			else
				connectToRoom ();
			msg.GetComponent<displayMSGScript> ().setString ("Trying to connect, please wait! Status: "+ PhotonNetwork.connectionStateDetailed.ToString());
		}
	}

	void OnJoinedLobby(){
		connectToRoom ();
	}

	private void connectToRoom(){
		PhotonNetwork.JoinRoom(textInput.GetComponent<Text> ().text.ToUpper());
	}

	void OnPhotonJoinRoomFailed() {
		if(msg==null)
			msg = Instantiate (displayMSG);

		msg.GetComponent<displayMSGScript> ().setString ("Failed to connect to room, please try again!");
	}

	void OnJoinedRoom()
	{
		if(msg)
			Destroy(msg);

		// setup the properties for all players online
		string s = PhotonNetwork.room.ToStringFull ();
		int maxNum = int.Parse (s.Split("/".ToCharArray())[1][0].ToString());
		int counter = 1;
		foreach (PhotonPlayer pl in PhotonNetwork.playerList) {
			pl.SetCustomProperties (new ExitGames.Client.Photon.Hashtable () {{"playerID", counter},{"maxNumPlayers", maxNum},{"currentTurn", 1},{"waiting", false}});
			counter++;
		}

		// update your status to everyone online
		lobby.GetComponent<PhotonView>().RPC ("setPlayerStatus", PhotonTargets.AllBufferedViaServer, (int)PhotonNetwork.player.AllProperties ["playerID"], 1);

		this.gameObject.SetActive (false);
		lobby.gameObject.SetActive (true);

		lobby.GetComponent<LobbyController> ().init ();
	}
}
