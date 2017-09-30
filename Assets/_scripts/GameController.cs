using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

	// scene declaration
	Scene activeScene;
	
	// various canvas menus
	GameObject mainMenu;
	public GameObject joinMenu;
	public GameObject hostMenu;
	public GameObject settingsMenu;

	// Use this for initialization
	void Start () {
		
		activeScene = SceneManager.GetActiveScene ();

		if (activeScene.name.Equals("MainMenu")){
			joinMenu.SetActive(false);
			hostMenu.SetActive (false);
			settingsMenu.SetActive (false);

		}

	}
	
	// Update is called once per frame
	void Update () {
		
	}


	public void quit(){
		Application.Quit()	;
	}
}
