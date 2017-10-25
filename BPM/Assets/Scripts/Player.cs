using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

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

    public enum PlayerClass {Sword, Laser};
    public PlayerClass Class;

	public Dictionary<PlayerClass, AudioClip> attackSounds; //Holds all player class attack sounds (will be used once code is restructured for choosing player class)
	public AudioSource playerAudio; //The AudioSource attached to this player

    // Use this for initialization
    protected void Start()
    {
        // initialize direction vectors
        directionVectors = new Dictionary<Direction, Vector3>();
        directionVectors.Add(Direction.Up, new Vector3(0.0f, 1.0f, 0.0f));
        directionVectors.Add(Direction.Down, new Vector3(0.0f, -1.0f, 0.0f));
        directionVectors.Add(Direction.Left, new Vector3(-1.0f, 0.0f, 0.0f));
        directionVectors.Add(Direction.Right, new Vector3(1.0f, 0.0f, 0.0f));

		//Make sure player audio doesn't immediately play
		playerAudio.playOnAwake = false;

        canAct = false;
        if (tag.Equals("Player1"))
        {
            position = new Vector3(-3.5f, 3.5f, 0.0f);
            direction = Direction.Right;
            Class = PlayerClass.Laser;
			//playerAudio.clip = attackSounds[Class];
            maxAttacks = 5;
        }
        else
        {
            position = new Vector3(3.5f, -3.5f, 0.0f);
            direction = Direction.Left;
            Class = PlayerClass.Sword;
			//playerAudio.clip = attackSounds[Class];
            maxAttacks = 3;
        }
        dead = false;
        score = 0;
    }

    // Update is called once per frame
    protected void Update()
    {
        transform.position = position;
        if (dead) this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
    }

    public Vector3[] attack()
    {
		//Play attack sound if audio isn't already playing
		if(!playerAudio.isPlaying)
		{
			playerAudio.Play();
		}

        Vector3[] attackedSpaces = new Vector3[maxAttacks];
        attackedSpaces[0] = position + directionVectors[direction];
        if (Class == PlayerClass.Sword)
        {
            //Center Square
            attackedSpaces[0] = position + directionVectors[direction];
            if (direction == Direction.Up || direction == Direction.Down)
            {
                //Side Squares Vertical
                attackedSpaces[1] = attackedSpaces[0] + directionVectors[Direction.Left];
                attackedSpaces[2] = attackedSpaces[0] + directionVectors[Direction.Right];
            }
            else if (direction == Direction.Left || direction == Direction.Right)
            {
                //Side Square Horizontal
                attackedSpaces[1] = attackedSpaces[0] + directionVectors[Direction.Up];
                attackedSpaces[2] = attackedSpaces[0] + directionVectors[Direction.Down];
            }
        }
        else if(Class == PlayerClass.Laser)
        {
            // Shoot 5 squares in a straight line
            for(int i = 1; i <= 4; i++)
            {
                attackedSpaces[i] = attackedSpaces[0] + (directionVectors[direction] * i);
            }
        }
        return attackedSpaces;
    }
}
