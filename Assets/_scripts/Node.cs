using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Node : MonoBehaviour {

	private int coinValue;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public abstract int getX ();

	public abstract int getY ();

	// returns the nodes coin value
	public abstract int getNodeValue ();

}
