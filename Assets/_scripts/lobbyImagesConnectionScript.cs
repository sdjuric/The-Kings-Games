using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class lobbyImagesConnectionScript : MonoBehaviour {

	private int status = 0;
	public Sprite[] images;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void setStatus(int status){
		this.status = status;
		GetComponent<Image>().sprite = images[status];
	}

	public int getStatus(){
		return status;
	}
}
