using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class resetDie : MonoBehaviour {

	public GameObject diceA;
	public GameObject diceB;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnMouseDown() {
		diceA.SetActive (true);
		diceA.GetComponent<Renderer> ().enabled = true;
		diceB.SetActive (false);
		diceB.GetComponent<Renderer> ().enabled = false;
	}
}
