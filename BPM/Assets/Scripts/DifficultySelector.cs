using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultySelector : MonoBehaviour {


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
		
	public void SetDifficulty()
	{
		PlayerPrefs.SetString("Difficulty", tag);	
	}
}
