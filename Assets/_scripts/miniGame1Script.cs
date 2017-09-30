using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class miniGame1Script : MonoBehaviour {

	private GameObject player;

	// Use this for initialization
	void Start () {
		//connect ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

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

	public GameObject spawnPlayer(){
		int id = (int)PhotonNetwork.player.AllProperties["playerID"];
		//GameObject player = PhotonNetwork.Instantiate ("sideHorse", new Vector3(-8,-2,0), Quaternion.identity, 0);
		player = PhotonNetwork.Instantiate ("sideHorse", new Vector3(-8,-4 + id*2,0), Quaternion.identity, 0);
		player.GetComponent<Minigame1PlayerMovement> ().readInput = true;
		//PhotonNetwork.Instantiate ("sideHorse", new Vector3(-8,2,0), Quaternion.identity, 0);
		//PhotonNetwork.Instantiate ("sideHorse", new Vector3(-8,4,0), Quaternion.identity, 0);
		//player.GetComponent<RectTransform> ().position = Vector3.zero;
		//player.GetComponent<PlayerController> ().board = this.gameObject;
		//player.GetComponent<RectTransform>().localPosition = ;
		return player;
	}

	public void destroyPlayer(){
		if(player!=null)
			PhotonNetwork.Destroy (player);
	}

	void OnGUI(){
		GUILayout.Label (PhotonNetwork.connectionStateDetailed.ToString());
	}
}
