using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public Vector3 position; // current position of the player
    public bool dead; // true if player has been attacked and killed, false otherwise
    public int score;

    protected int maxAttacks; // maximum number of squares player can attack with one attack

    protected bool canAct;

    public enum Input { Move, Attack, Nothing };
    public Input queuedAction;

    public enum Direction { Up, Down, Left, Right };
    public Direction direction;
    protected Dictionary<Direction, Vector3> directionVectors;
    // Use this for initialization
    protected void Start ()
    {
        // initialize direction vectors
        directionVectors = new Dictionary<Direction, Vector3>();
        directionVectors.Add(Direction.Up, new Vector3(0.0f, 1.0f, 0.0f));
        directionVectors.Add(Direction.Down, new Vector3(0.0f, -1.0f, 0.0f));
        directionVectors.Add(Direction.Left, new Vector3(-1.0f, 0.0f, 0.0f));
        directionVectors.Add(Direction.Right, new Vector3(1.0f, 0.0f, 0.0f));

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
        dead = false;
        maxAttacks = 1;
        score = 0;
	}
	
	// Update is called once per frame
	protected void Update ()
    {
        transform.position = position;
        if (dead) this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
	}

    public virtual Vector3[] attack()
    {
        Vector3[] attackedSpaces = new Vector3[maxAttacks];
        attackedSpaces[0] = position + directionVectors[direction];
        return attackedSpaces;
    }
}
