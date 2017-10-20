using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour {

    public GameObject player1;
    public GameObject player2;

    // X and Y values of both players' positions on the grid
    private int p1X; 
    private int p1Y;
    private int p2X;
    private int p2Y;

    private Vector3[,] gridSpace; // Holds each location on the grid

	// Use this for initialization
	void Start ()
    {
        // Initialize the gridSpace
        gridSpace = new Vector3[8,8];
        for(int i = 0; i < gridSpace.GetLength(0); i++)
        {   
            for(int j = 0; j < gridSpace.GetLength(1); j++)
            {
                gridSpace[i,j] = new Vector3(-3.5f + j, 3.5f - i, 0.0f);
            }
        }

        // Setup game for first round
        ResetRound();
    }
	
	// Update is called once per frame
	void Update ()
    {
        ProcessInput();
        if (player1.GetComponent<Player>().dead || player2.GetComponent<Player>().dead)
        {
            ResetRound();
            // Temporary score board
            Debug.Log("Current Score: ");
            Debug.Log("Player 1: " + player1.GetComponent<Player>().score.ToString());
            Debug.Log("Player 2: " + player2.GetComponent<Player>().score.ToString());
        }
	}

    /// <summary>
    /// Processes both player1's and player2's input.
    /// </summary>
    /// 
    void ProcessInput()
    {
        // Player 1's controls
        PlayerOneInput();

        // Player 2's controls
        PlayerTwoInput();
    }

    /// <summary>
    /// Processes both player1's input.
    /// </summary>
    /// 
    void PlayerOneInput()
    {
        if (Input.GetKeyDown("w")) // Up
        {
            player1.GetComponent<Player>().direction = new Vector3(0.0f, 1.0f, 0.0f);
            p1Y--;
            CheckWallBump();
            player1.GetComponent<Player>().position = gridSpace[p1Y, p1X];
        }

        if (Input.GetKeyDown("s")) // Down
        {
            player1.GetComponent<Player>().direction = new Vector3(0.0f, -1.0f, 0.0f);
            p1Y++;
            CheckWallBump();
            player1.GetComponent<Player>().position = gridSpace[p1Y, p1X];
        }

        if (Input.GetKeyDown("a")) // Left
        {
            player1.GetComponent<Player>().direction = new Vector3(-1.0f, 0.0f, 0.0f);
            p1X--;
            CheckWallBump();
            player1.GetComponent<Player>().position = gridSpace[p1Y, p1X];
        }

        if (Input.GetKeyDown("d")) // Right
        {
            player1.GetComponent<Player>().direction = new Vector3(1.0f, 0.0f, 0.0f);
            p1X++;
            CheckWallBump();
            player1.GetComponent<Player>().position = gridSpace[p1Y, p1X];
        }
        if (Input.GetKeyDown(KeyCode.LeftShift)) //Space Bar -> attack button
        {
            ProcessAttack(player1, player2);
        }
    }

    /// <summary>
    /// Processes both player2's input.
    /// </summary>
    /// 
    void PlayerTwoInput()
    {
        if (Input.GetKeyDown("up")) // Up
        {
            player2.GetComponent<Player>().direction = new Vector3(0.0f, 1.0f, 0.0f);
            p2Y--;
            CheckWallBump();
            player2.GetComponent<Player>().position = gridSpace[p2Y, p2X];
        }

        if (Input.GetKeyDown("down")) // Down
        {
            player2.GetComponent<Player>().direction = new Vector3(0.0f, -1.0f, 0.0f);
            p2Y++;
            CheckWallBump();
            player2.GetComponent<Player>().position = gridSpace[p2Y, p2X];
        }

        if (Input.GetKeyDown("left")) // Left
        {
            player2.GetComponent<Player>().direction = new Vector3(-1.0f, 0.0f, 0.0f);
            p2X--;
            CheckWallBump();
            player2.GetComponent<Player>().position = gridSpace[p2Y, p2X];
        }

        if (Input.GetKeyDown("right")) // Right
        {
            player2.GetComponent<Player>().direction = new Vector3(1.0f, 0.0f, 0.0f);
            p2X++;
            CheckWallBump();
            player2.GetComponent<Player>().position = gridSpace[p2Y, p2X];
        }
        if (Input.GetKeyDown(KeyCode.RightShift))
        {
            ProcessAttack(player2, player1);
        }
    }

    /// <summary>
    /// Process an attack action between two given player components.
    /// Increments score of attacker if attack hits target
    /// </summary>
    /// 
    void ProcessAttack(GameObject attacker, GameObject target)
    {
        Vector3[] attackedPositions = attacker.GetComponent<Player>().attack();
        foreach (Vector3 v in attackedPositions)
        {
            if (v.Equals(target.GetComponent<Player>().position))
            {
                target.GetComponent<Player>().dead = true;
                attacker.GetComponent<Player>().score++;
                break; // no need to continue this loop
            }
        }
        
    }

    /// <summary>
    /// Sets players back to starting positions alive for next round.
    /// Called to begin a set.
    /// </summary>
    void ResetRound()
    {
        // Establish starting positions for the players
        player1.GetComponent<Player>().position = gridSpace[0, 0];
        player2.GetComponent<Player>().position = gridSpace[7, 7];
        p1X = 0;
        p1Y = 0;
        p2X = 7;
        p2Y = 7;

        // Set both players to alive
        player1.GetComponent<Player>().dead = false;
        player2.GetComponent<Player>().dead = false;
    }

    /// <summary>
    /// Denies players from attempting to walk off of the grid. Players are knocked back a square instead.
    /// </summary>
    void CheckWallBump()
    {
        // Check that Player1 stays on the grid
        if (p1X >= gridSpace.GetLength(1))
        {
            p1X = gridSpace.GetLength(1) - 2;
        }
        else if(p1X < 0)
        {
            p1X = 1;
        }

        if (p1Y >= gridSpace.GetLength(0))
        {
            p1Y = gridSpace.GetLength(0) - 2;
        }
        else if(p1Y < 0)
        {
            p1Y = 1;
        }

        // Check that Player2 stays on the grid
        if (p2X >= gridSpace.GetLength(1))
        {
            p2X = gridSpace.GetLength(1) - 2;
        }
        else if (p2X < 0)
        {
            p2X = 1;
        }

        if (p2Y >= gridSpace.GetLength(0))
        {
            p2Y = gridSpace.GetLength(0) - 2;
        }
        else if (p2Y < 0)
        {
            p2Y = 1;
        }

        // Check to see if the players are colliding
        if(p1X == p2X && p1Y == p2Y)
        {
            if(player1.GetComponent<Player>().direction.x > 0)
            {
                p1X -= 2;
                p2X += 2;
            }
            else if(player1.GetComponent<Player>().direction.x < 0)
            {
                p1X += 2;
                p2X -= 2;
            }

            if (player1.GetComponent<Player>().direction.y > 0)
            {
                p1Y -= 2;
                p2Y += 2;
            }
            else if (player1.GetComponent<Player>().direction.y < 0)
            {
                p1Y += 2;
                p2Y -= 2;
            }

            if (player2.GetComponent<Player>().direction.x > 0)
            {
                p1X -= 2;
                p2X += 2;
            }
            else if (player2.GetComponent<Player>().direction.x < 0)
            {
                p1X += 2;
                p2X -= 2;
            }

            if (player2.GetComponent<Player>().direction.y > 0)
            {
                p1Y -= 2;
                p2Y += 2;
            }
            else if (player2.GetComponent<Player>().direction.y < 0)
            {
                p1Y += 2;
                p2Y -= 2;
            }
        }
    }
}
