using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Code from: https://www.sitepoint.com/adding-pause-main-menu-and-game-over-screens-in-unity/
public class UIManager : MonoBehaviour 
{
	GameObject[] pauseObjects;
	GameObject quad; // added by Niko
    GameObject[] toHide;
    GameObject player1;
    GameObject player2;
	bool paused; // added by Niko

	// Use this for initialization
	void Start () {
		Time.timeScale = 1;
		pauseObjects = GameObject.FindGameObjectsWithTag("ShowOnPause");
		quad = GameObject.FindGameObjectWithTag ("Map"); // added by Niko
        toHide = GameObject.FindGameObjectsWithTag("HideOnPause");
        player1 = GameObject.FindGameObjectWithTag("Player1");
        player2 = GameObject.FindGameObjectWithTag("Player2");
        hidePaused();
		paused = false; // added by Niko
	}

	// Update is called once per frame
	void Update () {

		//uses the p button to pause and unpause the game
		// added by Niko
		if (Input.GetKeyDown (KeyCode.P)) {
			if (!paused) {
				quad.SetActive (false);
				showPaused ();
				paused = true;
			} else {
				hidePaused ();
				quad.SetActive (true);
				paused = false;
			}
		}


			/*
			 * Original Code
			if(Time.timeScale == 1)
			{
				Time.timeScale = 0;
				showPaused();
			} else if (Time.timeScale == 0){
				Debug.Log ("high");
				Time.timeScale = 1;
				hidePaused();
			}
			*/
	}


	//Reloads the Level
	public void Reload(){
		Application.LoadLevel(Application.loadedLevel);
	}

	//controls the pausing of the scene
	public void pauseControl()
	{
		// added by Niko
		hidePaused ();
		quad.SetActive (true);
			
		/*
		 * Original code
		if(Time.timeScale == 1)
		{
			Time.timeScale = 0;
			showPaused();
		} else if (Time.timeScale == 0){
			Time.timeScale = 1;
			hidePaused();
		}
		*/
	}

	//shows objects with ShowOnPause tag
	public void showPaused(){
		foreach(GameObject g in pauseObjects){
			g.SetActive(true);
		}
        foreach(GameObject g in toHide)
        {
            g.SetActive(false);
        }
        player1.SetActive(false);
        player2.SetActive(false);
	}

	//hides objects with ShowOnPause tag
	public void hidePaused(){
		foreach(GameObject g in pauseObjects){
			g.SetActive(false);
		}
        foreach (GameObject g in toHide)
        {
            g.SetActive(true);
        }
        if (player1 != null && player2 != null)
        {
            player1.SetActive(true);
            player2.SetActive(true);
        }
    }

	//loads inputted level
	public void LoadLevel(string level){
		Application.LoadLevel(level);
	}
}
