using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public Vector3 position; // current position of the player

	// Use this for initialization
	void Start ()
    {
        if (tag.Equals("Player1"))
        {
            position = new Vector3(-3.5f, 3.5f, 0.0f);
        }
        else
        {
            position = new Vector3(3.5f, -3.5f, 0.0f);
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.position = position;
	}
}
