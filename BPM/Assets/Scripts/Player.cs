using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public Vector3 position; // current position of the player
    public Vector3 direction; // current direction player is facing (+/- X is right/left, +/- Y is up/down)
    public bool dead; // true if player has been attacked and killed, false otherwise
    public int score;

    protected int maxAttacks; // maximum number of squares player can attack with one attack

	// Use this for initialization
	void Start ()
    {
        if (tag.Equals("Player1"))
        {
            position = new Vector3(-3.5f, 3.5f, 0.0f);
            direction = new Vector3(1.0f, 0.0f, 0.0f);
        }
        else
        {
            position = new Vector3(3.5f, -3.5f, 0.0f);
            direction = new Vector3(-1.0f, 0.0f, 0.0f);
        }
        dead = false;
        maxAttacks = 1;
        score = 0;
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.position = position;
        if (dead) this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
	}

    public virtual Vector3[] attack()
    {
        Vector3[] attackedSpaces = new Vector3[maxAttacks];
        attackedSpaces[0] = position + direction;
        return attackedSpaces;
    }
}
