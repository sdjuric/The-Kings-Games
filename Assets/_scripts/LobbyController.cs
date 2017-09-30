using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LobbyController : MonoBehaviour {

	public GameObject MainMenu;
	public GameObject Lobby;
	public GameObject[] imagesObject;
	public GameObject[] imagesLists;
	public GameObject readyObject;
	private int maxNumPlayers;
	private int funCounter=0;
	public GameObject msg;
	private GameObject localmsg;
	private IEnumerator coroutine;
	private int waitCounter = 5;
	private bool nowCounting = false;

	// Use this for initialization
	void Start () {
	
	}

	// setup function
	public void init(){
		
		maxNumPlayers = (int)PhotonNetwork.player.AllProperties["maxNumPlayers"];

		for (int i = 0; i < maxNumPlayers;i++) {
			Vector3 v = imagesObject [i].GetComponent<RectTransform> ().localPosition;
			v = new Vector3((maxNumPlayers)*(-50)+i*100+50, 0, 0);
			imagesObject [i].GetComponent<RectTransform> ().localPosition = v;
			imagesObject [i].SetActive (true);
		}
	}
	
	// Update is called once per frame
	void Update () {

		// check if a player lost connection
		bool[] alive = {false,false,false,false};
		foreach (PhotonPlayer pl in PhotonNetwork.playerList) {
			if (pl != null && pl.AllProperties ["playerID"] != null){
				alive [(int)pl.AllProperties ["playerID"] - 1] = true;
			}
		}

		// update the status of the online players
		bool startTheFun = true;
		for (int i = 0; i < maxNumPlayers; i++) {
			if (alive [i] == false) {
				imagesLists [i].GetComponent<lobbyImagesConnectionScript> ().setStatus (0);
			}
			if(imagesLists [i].GetComponent<lobbyImagesConnectionScript> ().getStatus ()!=2){
				startTheFun = false;
				funCounter = 0;
				waitCounter = 5;
				if(localmsg!=null)
					Destroy (localmsg);
				nowCounting = false;
			}
		}

		// check if everyone is ready
		if (startTheFun) {
			funCounter++;
		}
		if (funCounter == 1) {
			nowCounting = true;
			coroutine = WaitAndPrint ();
			StartCoroutine (coroutine);
		}

	}

	// diconnect from the server
	public void leave(){
		PhotonNetwork.Disconnect ();
		MainMenu.SetActive (true);
		Lobby.SetActive (false);
	}

	// change your status ready/not ready and broadcast it to everyone
	public void changeReadyState(){
		int x = 1;
		int id = (int)PhotonNetwork.player.AllProperties ["playerID"];
		if (imagesLists [id - 1].GetComponent<lobbyImagesConnectionScript> ().getStatus () == 1) {
			x = 2;
			readyObject.GetComponent<Text> ().text = "Not Ready";
		}else if (imagesLists [id - 1].GetComponent<lobbyImagesConnectionScript> ().getStatus () == 2) {
			x = 1;
			readyObject.GetComponent<Text> ().text = "Ready";
		}
		GetComponent<PhotonView>().RPC ("setPlayerStatus", PhotonTargets.AllBufferedViaServer, id, x);
	}

	[PunRPC]
	void setPlayerStatus(int position, int status){
		imagesLists [position-1].GetComponent<lobbyImagesConnectionScript> ().setStatus (status);
	}

	// start the match after 5 seconds if everyone is still ready
	IEnumerator WaitAndPrint(){
		while (nowCounting && waitCounter != 0) {
			if (localmsg == null) {
				localmsg = Instantiate (msg);
			}
			localmsg.GetComponent<displayMSGScript> ().setString ("Match will start in " + waitCounter);
			yield return new WaitForSeconds (1.0f);
			waitCounter--;
		}
		if(!nowCounting)
			yield break;
		else
			SceneManager.LoadScene ("GameScene");
	}
}
