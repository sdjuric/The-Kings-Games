using System.Collections;
using System.Collections.Generic;
using UnityEngine;



/**
 * Controller for a neutral node;
 */
public class NodeController : Node {

	//private int x;
	//private int y;
	private int nodeValue;
	// Use this for initialization
	void Start () {
		//Debug.Log (this.transform.position.x);
		//this.x = (int)this.transform.position.x * 100;
		//this.y = (int)transform.position.y;
		this.nodeValue = 3;
	}

	// Update is called once per frame
	void Update () {
		
	}

	public override int getX(){
		return (int)GetComponent<RectTransform>().position.x;
	}

	public override int getY(){
		
		return (int)GetComponent<RectTransform>().position.y;
	}


	public override int getNodeValue (){
		return this.nodeValue;
	}



}
