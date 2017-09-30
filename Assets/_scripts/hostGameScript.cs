using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class hostGameScript : MonoBehaviour {

	public GameObject displayMSG;
    public GameObject roomMaxNumber;
    private int currentMax = 2;
	public GameObject lobby;
	private GameObject msg;
	private string roomname;
	public GameObject[] menuObjects;
	private string[] alpha = {
		"Q","W","E","R","T",
		"Y","U","I","O","P",
		"A","S","D","F","G",
		"H","J","K","L","Z",
		"X","C","V","B","N","M"
	};

	// Use this for initialization
	void Start () {
	
	}
		
	// show all button related to the host menu
	public void showAllButtons(){
		foreach (GameObject o in menuObjects) {
			o.SetActive (true);
		}
	}
		
	// Update is called once per frame
	void Update () {
        roomMaxNumber.GetComponent<Text>().text = ""+currentMax;
	}

	// control the GUI buttons
    public void roomNumberUp() {
        if (currentMax < 4)
            currentMax++;
        else
            currentMax = 2;
    }

	// control the GUI buttons
    public void roomNumberDown() {
        if (currentMax > 2)
            currentMax--;
        else
            currentMax = 4;
    }

	// create room
	public void hostRoom(){

		// create a connection then create a room, if already connected then create a room
		if (!PhotonNetwork.insideLobby)
			PhotonNetwork.ConnectUsingSettings ("The King's Games");
		else
			createRoom ();
		
		foreach(GameObject o in menuObjects){
			o.SetActive (false);
		}
			
		// display a message representing the state of the connection
		msg = Instantiate (displayMSG);
		msg.GetComponent<displayMSGScript> ().exitButton.SetActive (false);
		msg.GetComponent<displayMSGScript> ().setString ("Creating a new room, please wait! Status: "+ PhotonNetwork.connectionStateDetailed.ToString());
	}

	// create an online connection
	private void createRoom(){
		RoomOptions ro = new RoomOptions();
		ro.MaxPlayers = (byte)currentMax;
		roomname = getRandomString ();

		GUIUtility.systemCopyBuffer = roomname;

		PhotonNetwork.CreateRoom(roomname, ro, null);
	}

	public string getRandomString(){
		string target = "";
		target += alpha[Random.Range (0,26)];
		target += Random.Range (0,10);
		target += alpha[Random.Range (0,26)];
		target += Random.Range (0,10);
		target += alpha[Random.Range (0,26)];
		target += Random.Range (0,10);
		return target;
	}

	void OnJoinedLobby(){
		createRoom ();
	}

	void OnJoinedRoom()
	{
		// set your the first player properties
		PhotonNetwork.player.SetCustomProperties (new ExitGames.Client.Photon.Hashtable(){{"playerID", 1},{"maxNumPlayers", currentMax},{"currentTurn", 1},{"waiting", false}});

		// broadcast a message telling everyone that this player connected
		lobby.GetComponent<PhotonView>().RPC ("setPlayerStatus", PhotonTargets.AllBufferedViaServer, 1 ,1);

		// display the lobby scene, and hide the menu
		this.gameObject.SetActive (false);
		lobby.gameObject.SetActive (true);

		lobby.GetComponent<LobbyController> ().init ();

		msg.GetComponent<displayMSGScript> ().exitButton.SetActive (true);
		msg.GetComponent<displayMSGScript> ().setString ("Room Name: " + roomname);
	
	}
}
