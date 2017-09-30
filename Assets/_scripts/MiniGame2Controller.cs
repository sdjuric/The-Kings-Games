using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MiniGame2Controller : MonoBehaviour
{
	float time = 5.5f;
	public Text timer;
	public GameObject[] horses;
	public bool started = false;
	private GameObject player;

	//public GameObject coin;

	// Use this for initialization
	void Start ()
	{
		//spawnPlayer ();
		//connect();
	}

	//private void spawnPlayer(){
		//GameObject player = PhotonNetwork.Instantiate ("playerHorse", Vector3.zero, Quaternion.identity, 0);
		//player.GetComponent<RectTransform> ().position = Vector3.zero;
		//player.GetComponent<PlayerController> ().board = this.gameObject;

		//player.GetComponent<PlayerController> ().init ();
	//}
	// Update is called once per frame
	void Update ()
	{
		time -= Time.deltaTime;
		timer.text = "" + Mathf.Round(time);

		//swap from starting count to minute timer
		if((time <= 0.5) && (started == false))
		{
			timer.text = "START!";
			if(time <= -0.5)
			{
				started = true;
				GameStart ();
				time = 60.5f;
			}
		}

		if ((time <= 0) && (started == true)) 
		{
			GameOver ();
		}

	}

	void GameStart ()
	{
		//do start stuff
	}

	void GameOver ()
	{
		timer.text = "GAME OVER";
		//do game over stuff
	}

	////////////////
	/// ///////////
	/// 
	/// 
	/// 
	/// 
	/// 
	/// 

	private void connect(){
		PhotonNetwork.ConnectUsingSettings ("The King's Games");
	}
	void OnJoinedLobby(){
		createRoom ();
	}
	private void createRoom (){
		string target = "a1b2c3";
		//target += Random.Range (0,10);
		//target += Random.Range (0,10);
		//target += Random.Range (0,10);
		RoomOptions ro = new RoomOptions();
		ro.MaxPlayers = 2;
		PhotonNetwork.JoinOrCreateRoom(target, ro, null);
	}
	void OnJoinedRoom()
	{
		PhotonNetwork.player.SetCustomProperties (new ExitGames.Client.Photon.Hashtable(){{"playerID", 1},{"maxNumPlayers", 1},{"currentTurn", 1}});
		spawnPlayer ();
	}

	public GameObject spawnPlayer(){
		int id = (int)PhotonNetwork.player.AllProperties["playerID"];
		//GameObject player = PhotonNetwork.Instantiate ("sideHorse", new Vector3(-8,-2,0), Quaternion.identity, 0);
		player = PhotonNetwork.Instantiate ("playerHorse", new Vector3(-8 + id*2,0,0), Quaternion.identity, 0);
		player.GetComponent<horseController> ().readInput = true;
		//PhotonNetwork.Instantiate ("playerHorse", new Vector3(-8,2,0), Quaternion.identity, 0);
		//PhotonNetwork.Instantiate ("playerHorse", new Vector3(-8,4,0), Quaternion.identity, 0);
		//player.GetComponent<RectTransform> ().position = Vector3.zero;
		//player.GetComponent<PlayerController> ().board = this.gameObject;
		//player.GetComponent<RectTransform>().localPosition = ;
		//addOneToAll();
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

