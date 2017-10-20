using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public Vector3 position; // current position of the player
    //public Vector3 direction; // current direction player is facing (+/- X is right/left, +/- Y is up/down)

    protected bool canAct;

    public enum Input { Move, Attack, Nothing };
    public Input queuedAction;

    public enum Direction { Up, Down, Left, Right };
    public Direction direction;

    // Use this for initialization
    void Start ()
    {
        canAct = false;
        if (tag.Equals("Player1"))
        {
            position = new Vector3(-3.5f, 3.5f, 0.0f);
            direction = Direction.Right;
        }
        else
        {
            position = new Vector3(3.5f, -3.5f, 0.0f);
            direction = Direction.Left;
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.position = position;
	}
}
