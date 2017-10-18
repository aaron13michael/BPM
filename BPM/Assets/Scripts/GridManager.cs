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

        // Establish starting positions for the players
        player1.GetComponent<Player>().position = gridSpace[0, 0];
        player2.GetComponent<Player>().position = gridSpace[7, 7];
        p1X = 0;
        p1Y = 0;
        p2X = 7;
        p2Y = 7;
    }
	
	// Update is called once per frame
	void Update ()
    {
        ProcessInput();
	}

    /// <summary>
    /// Processes both player1's and player2's input.
    /// </summary>
    void ProcessInput()
    {
        

        // Player 1's controls
        if (Input.GetKeyDown("w")) // Up
        {
            p1Y--;
            CheckWallBump();
            player1.GetComponent<Player>().position = gridSpace[p1Y, p1X];
        }
        
        if (Input.GetKeyDown("s")) // Down
        {
            p1Y++;
            CheckWallBump();
            player1.GetComponent<Player>().position = gridSpace[p1Y, p1X];
        }
        
        if (Input.GetKeyDown("a")) // Left
        {
            p1X--;
            CheckWallBump();
            player1.GetComponent<Player>().position = gridSpace[p1Y, p1X];
        }

        if (Input.GetKeyDown("d")) // Right
        {
            p1X++;
            CheckWallBump();
            player1.GetComponent<Player>().position = gridSpace[p1Y, p1X];
        }

        // Player 2's controls
        if (Input.GetKeyDown("up")) // Up
        {
            p2Y--;
            CheckWallBump();
            player2.GetComponent<Player>().position = gridSpace[p2Y, p2X];
        }

        if (Input.GetKeyDown("down")) // Down
        {
            p2Y++;
            CheckWallBump();
            player2.GetComponent<Player>().position = gridSpace[p2Y, p2X];
        }

        if (Input.GetKeyDown("left")) // Left
        {
            p2X--;
            CheckWallBump();
            player2.GetComponent<Player>().position = gridSpace[p2Y, p2X];
        }

        if (Input.GetKeyDown("right")) // Right
        {
            p2X++;
            CheckWallBump();
            player2.GetComponent<Player>().position = gridSpace[p2Y, p2X];
        }
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
    }
}
