using System.Collections;
using System.Collections.Generic;
using UnityEngine;



/**
 * Controller for a neutral node;
 */
public class BadNodeController : Node {

	private int x;
	private int y;
	private int nodeValue;
	// Use this for initialization
	void Start () {
		this.x = (int)transform.position.x;
		this.y = (int)transform.position.y;
		this.nodeValue = -5;
	}

	// Update is called once per frame
	void Update () {
		
	}

	public override int getX(){
		return this.x;
	}

	public override int getY(){
		return this.y;
	}


	public override int getNodeValue (){
		return this.nodeValue;
	}



}
