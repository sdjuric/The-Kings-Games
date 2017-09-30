using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class displayMSGScript : MonoBehaviour {

	public GameObject text;
	public GameObject exitButton;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void setString (string s){
		text.GetComponent<Text>().text = s;
	}

	public void removeDisplay(){
		Destroy(this.gameObject);
	}
}
