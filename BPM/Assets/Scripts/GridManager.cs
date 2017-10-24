using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{

    public GameObject player1;
    public GameObject player2;

    // X and Y values of both players' positions on the grid
    private int p1X;
    private int p1Y;
    private int p2X;
    private int p2Y;

    List<KeyCode> p1Actions;
    List<KeyCode> p2Actions;

    private Vector3[,] gridSpace; // Holds each location on the grid

    // Use this for initialization
    void Start()
    {
        // Initialize the gridSpace
        gridSpace = new Vector3[8, 8];
        for (int i = 0; i < gridSpace.GetLength(0); i++)
        {
            for (int j = 0; j < gridSpace.GetLength(1); j++)
            {
                gridSpace[i, j] = new Vector3(-3.5f + j, 3.5f - i, 0.0f);
            }
        }

        // Establish starting positions for the players
        player1.GetComponent<Player>().position = gridSpace[0, 0];
        player2.GetComponent<Player>().position = gridSpace[7, 7];
        p1X = 0;
        p1Y = 0;
        p2X = 7;
        p2Y = 7;

        // Actions Lists initialization
        p1Actions = new List<KeyCode>();
        p2Actions = new List<KeyCode>();

        p1Actions.Add(KeyCode.D);
        p2Actions.Add(KeyCode.RightArrow);
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInput();
    }

    /// <summary>
    /// Processes both player1's and player2's input.
    /// </summary>
    void ProcessInput()
    {
        // Player 1's controls
        if (Input.GetKeyDown(KeyCode.W)) // Up
        {
            p1Actions.Add(KeyCode.W);
            player1.GetComponent<Player>().direction = new Vector3(0.0f, 1.0f, 0.0f);
            p1Y--;
            CheckCollisions();
            player1.GetComponent<Player>().position = gridSpace[p1Y, p1X];
        }

        if (Input.GetKeyDown(KeyCode.S)) // Down
        {
            p1Actions.Add(KeyCode.S);
            player1.GetComponent<Player>().direction = new Vector3(0.0f, -1.0f, 0.0f);
            p1Y++;
            CheckCollisions();
            player1.GetComponent<Player>().position = gridSpace[p1Y, p1X];
        }

        if (Input.GetKeyDown(KeyCode.A)) // Left
        {
            p1Actions.Add(KeyCode.A);
            player1.GetComponent<Player>().direction = new Vector3(-1.0f, 0.0f, 0.0f);
            p1X--;
            CheckCollisions();
            player1.GetComponent<Player>().position = gridSpace[p1Y, p1X];
        }

        if (Input.GetKeyDown(KeyCode.D)) // Right
        {
            p1Actions.Add(KeyCode.D);
            player1.GetComponent<Player>().direction = new Vector3(1.0f, 0.0f, 0.0f);
            p1X++;
            CheckCollisions();
            player1.GetComponent<Player>().position = gridSpace[p1Y, p1X];
        }

        // Player 2's controls
        if (Input.GetKeyDown(KeyCode.UpArrow)) // Up
        {
            p2Actions.Add(KeyCode.UpArrow);
            player2.GetComponent<Player>().direction = new Vector3(0.0f, 1.0f, 0.0f);
            p2Y--;
            CheckCollisions();
            player2.GetComponent<Player>().position = gridSpace[p2Y, p2X];
        }

        if (Input.GetKeyDown(KeyCode.DownArrow)) // Down
        {
            p2Actions.Add(KeyCode.DownArrow);
            player2.GetComponent<Player>().direction = new Vector3(0.0f, -1.0f, 0.0f);
            p2Y++;
            CheckCollisions();
            player2.GetComponent<Player>().position = gridSpace[p2Y, p2X];
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow)) // Left
        {
            p2Actions.Add(KeyCode.LeftArrow);
            player2.GetComponent<Player>().direction = new Vector3(-1.0f, 0.0f, 0.0f);
            p2X--;
            CheckCollisions();
            player2.GetComponent<Player>().position = gridSpace[p2Y, p2X];
        }

        if (Input.GetKeyDown(KeyCode.RightArrow)) // Right
        {
            p2Actions.Add(KeyCode.RightArrow);
            player2.GetComponent<Player>().direction = new Vector3(1.0f, 0.0f, 0.0f);
            p2X++;
            CheckCollisions();
            player2.GetComponent<Player>().position = gridSpace[p2Y, p2X];
        }
    }

    /// <summary>
    /// Denies players from attempting to walk off of the grid. Players are knocked back a square instead.
    /// </summary>
    void CheckCollisions()
    {
        // Check to see if the players are colliding and if so, move both back to simulate a pushback
        if (p1X == p2X && p1Y == p2Y)
        {
            if (player1.GetComponent<Player>().direction.x > 0 && player2.GetComponent<Player>().direction.x < 0)
            {
                if (p1X == 1)
                {
                    p1X--;
                }
                else
                {
                    p1X -= 2;
                }
                if (p2X == 6)
                {
                    p2X++;
                }
                else
                {
                    p2X += 2;
                }
            }

            if (player1.GetComponent<Player>().direction.y > 0 && player2.GetComponent<Player>().direction.y < 0)
            {
                if (p1Y == 6)
                {
                    p1Y++;
                }
                else
                {
                    p1Y += 2;
                }
                if (p2Y == 1)
                {
                    p2Y--;
                }
                else
                {
                    p2Y -= 2;
                }
            }

            if (player2.GetComponent<Player>().direction.x > 0 && player1.GetComponent<Player>().direction.x < 0)
            {
                if (p1X == 6)
                {
                    p1X++;
                }
                else
                {
                    p1X += 2;
                }
                if (p2X == 1)
                {
                    p2X--;
                }
                else
                {
                    p2X -= 2;
                }
            }

            if (player2.GetComponent<Player>().direction.y > 0 && player1.GetComponent<Player>().direction.y < 0)
            {
                if (p1Y == 1)
                {
                    p1Y--;
                }
                else
                {
                    p1Y -= 2;
                }
                if (p2Y == 6)
                {
                    p2Y++;
                }
                else
                {
                    p2Y += 2;
                }
            }
        }

        // Check that Player1 stays on the grid
        if (p1X >= gridSpace.GetLength(1))
        {
            p1X = gridSpace.GetLength(1) - 2;
        }
        else if (p1X < 0)
        {
            p1X = 1;
        }

        if (p1Y >= gridSpace.GetLength(0))
        {
            p1Y = gridSpace.GetLength(0) - 2;
        }
        else if (p1Y < 0)
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

        player1.GetComponent<Player>().position = gridSpace[p1Y, p1X];
        player2.GetComponent<Player>().position = gridSpace[p2Y, p2X];
    }

    // Draws the buttons and lables that hold information about the scene onto the screen.
    void OnGUI()
    {
        GUI.Label(new Rect(100.0f, 215.0f, 150.0f, 25.0f), "p1 Action: " + p1Actions[p1Actions.Count - 1]);
        GUI.Label(new Rect(800.0f, 215.0f, 150.0f, 25.0f), "p2 Action: " + p2Actions[p2Actions.Count - 1]);
    }
}
