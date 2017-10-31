using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GridManager : MonoBehaviour
{

    public GameObject player1;
    public GameObject p1Arrow;
    public GameObject player2;
    public GameObject p2Arrow;
    public new GameObject audio;
    public Texture floorTexture1;
    public Texture floorTexture2;
	public Font MyFont;
    
    // X and Y values of both players' positions on the grid
    private int p1X;
    private int p1Y;
    private int p2X;
    private int p2Y;
	private int prevP1X;
	private int prevP1Y;
	private int prevP2X;
	private int prevP2Y;

    List<KeyCode> p1Actions;
    List<KeyCode> p2Actions;

    private Vector3[,] gridSpace; // Holds each location on the grid

    // Animators
    private Animator diskAnimator;
    private Animator laserAnimator;
    private int currentTexture = 1;

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

        diskAnimator = player1.GetComponent<Animator>();
        laserAnimator = player2.GetComponent<Animator>();
        // Setup game for first round
        ResetRound();
        gameObject.GetComponent<Renderer>().material.mainTexture = floorTexture1;
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInput();
        CheckDownbeat();

        if (player1.GetComponent<Player>().dead || player2.GetComponent<Player>().dead)
        {
            ResetRound();
        }

		if (player1.GetComponent<Player> ().score >= 3) 
		{
			//PlayerPrefs.SetString ("Winner", "Player 1 Wins"); ---> trying to display which player won (see Start() in UIManager)
			SceneManager.LoadScene ("GameOver");
		}

		if (player2.GetComponent<Player> ().score >= 3) 
		{
			//PlayerPrefs.SetString ("Winner", "Player 2 Wins"); ---> trying to display which player won see Start() in UIManager)
			SceneManager.LoadScene ("GameOver");
		}

	}

    /// <summary>
    /// Processes both player1's and player2's input.
    /// Adjusted to include player input and direction enums. 
    /// Processes inputs and stores them in action queue.
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
        if (Input.GetKeyDown(KeyCode.W)) // Up
        {
            diskAnimator.SetTrigger("diskWalkUp");
            player1.GetComponent<Player>().queuedAction = Player.Input.Move;
            player1.GetComponent<Player>().direction = Player.Direction.Up;
            p1Actions.Add(KeyCode.W);
            p1Arrow.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 90.0f);
        }
        if (Input.GetKeyDown(KeyCode.S)) // Down
        {
            diskAnimator.SetTrigger("diskWalkDown");
            player1.GetComponent<Player>().queuedAction = Player.Input.Move;
            player1.GetComponent<Player>().direction = Player.Direction.Down;
            p1Actions.Add(KeyCode.S);
            p1Arrow.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 270.0f);
        }
        if (Input.GetKeyDown(KeyCode.A)) // Left
		{
            diskAnimator.SetTrigger("diskWalkLeft");
            player1.GetComponent<Player>().queuedAction = Player.Input.Move;
            player1.GetComponent<Player>().direction = Player.Direction.Left;
            p1Actions.Add(KeyCode.A);
            p1Arrow.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 180.0f);
        }
        if (Input.GetKeyDown(KeyCode.D)) // Right
        {
            diskAnimator.SetTrigger("diskWalkRight");
            player1.GetComponent<Player>().queuedAction = Player.Input.Move;
            player1.GetComponent<Player>().direction = Player.Direction.Right;
            p1Actions.Add(KeyCode.D);
            p1Arrow.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
        }
        if (Input.GetKeyDown(KeyCode.LeftShift)) //Space Bar -> attack button
        {
			player1.GetComponent<Player>().queuedAction = Player.Input.Attack;
        }
        p1Arrow.transform.position = new Vector3(-5.0f, 3.0f, -0.5f);
    }
    /// <summary>
    /// Processes both player2's input.
    /// </summary>
    /// 
    void PlayerTwoInput()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow)) // Up
        {
            laserAnimator.SetTrigger("laserUpWalk");
            player2.GetComponent<Player>().queuedAction = Player.Input.Move;
            player2.GetComponent<Player>().direction = Player.Direction.Up;
            p2Actions.Add(KeyCode.UpArrow);
            p2Arrow.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 90.0f);
        }

        if (Input.GetKeyDown(KeyCode.DownArrow)) // Down
        {
            laserAnimator.SetTrigger("laserDownWalk");
            player2.GetComponent<Player>().queuedAction = Player.Input.Move;
            player2.GetComponent<Player>().direction = Player.Direction.Down;
            p2Actions.Add(KeyCode.DownArrow);
            p2Arrow.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 270.0f);
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow)) // Left
        {
            laserAnimator.SetTrigger("laserLeftWalk");
            player2.GetComponent<Player>().queuedAction = Player.Input.Move;
            player2.GetComponent<Player>().direction = Player.Direction.Left;
            p2Actions.Add(KeyCode.LeftArrow);
            p2Arrow.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 180.0f);
        }

        if (Input.GetKeyDown(KeyCode.RightArrow)) // Right
        {
            laserAnimator.SetTrigger("laserRightWalk");
            player2.GetComponent<Player>().queuedAction = Player.Input.Move;
            player2.GetComponent<Player>().direction = Player.Direction.Right;
            p2Actions.Add(KeyCode.RightArrow);
            p2Arrow.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
        }
        if (Input.GetKeyDown(KeyCode.RightShift))
        {
			player2.GetComponent<Player>().queuedAction = Player.Input.Attack;
        }
        p2Arrow.transform.position = new Vector3(5.0f, 3.0f, -0.5f);
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
		prevP1X = p1X;
		prevP1Y = p1Y;
		prevP2X = p2X;
		prevP2Y = p2Y;

        // Set both players to alive
        player1.GetComponent<Player>().dead = false;
        player2.GetComponent<Player>().dead = false;

        // Actions Lists initialization
        p1Actions = new List<KeyCode>();
        p2Actions = new List<KeyCode>();

        p1Actions.Add(KeyCode.D);
        p2Actions.Add(KeyCode.RightArrow);

        diskAnimator.SetTrigger("diskWalkRight");
        laserAnimator.SetTrigger("laserLeftWalk");
    }

	/// <summary>
	/// Denies players from attempting to walk off of the grid. Players are knocked back a square instead.
	/// </summary>
	void CheckCollisions()
	{
		// Check to see if the players are colliding
		if (p1X == p2X && p1Y == p2Y)
		{
			// Fact-to-face collisions (facing opposite directions)
			if (player1.GetComponent<Player>().direction == Player.Direction.Right && player2.GetComponent<Player>().direction == Player.Direction.Left)
			{
				p1X -= 2;
				p2X += 2;
			}
			else if (player1.GetComponent<Player>().direction == Player.Direction.Left && player2.GetComponent<Player>().direction == Player.Direction.Right)
			{
				p1X += 2;
				p2X -= 2;
			}

			if (player1.GetComponent<Player>().direction == Player.Direction.Up && player2.GetComponent<Player>().direction == Player.Direction.Down)
			{
				p1Y += 2;
				p2Y -= 2;
			}
			else if (player1.GetComponent<Player>().direction == Player.Direction.Down && player2.GetComponent<Player>().direction == Player.Direction.Up)
			{
				p1Y -= 2;
				p2Y += 2;
			}

			if (player2.GetComponent<Player>().direction == Player.Direction.Right && player1.GetComponent<Player>().direction == Player.Direction.Left)
			{
				p1X += 2;
				p2X -= 2;
			}
			else if (player2.GetComponent<Player>().direction == Player.Direction.Left && player1.GetComponent<Player>().direction == Player.Direction.Right)
			{
				p1X -= 2;
				p2X += 2;
			}

			if (player2.GetComponent<Player>().direction == Player.Direction.Up && player1.GetComponent<Player>().direction == Player.Direction.Down)
			{
				p1Y -= 2;
				p2Y += 2;
			}
			else if (player2.GetComponent<Player>().direction == Player.Direction.Down && player1.GetComponent<Player>().direction == Player.Direction.Up)
			{
				p1Y += 2;
				p2Y -= 2;
			}

			// Pushing animations (facing same directions)
			if (player1.GetComponent<Player>().direction == Player.Direction.Right && player2.GetComponent<Player>().direction == Player.Direction.Right)
			{
				if (prevP1X > prevP2X) 
				{
					p1X += 2;
					p2X -= 2;
				} 
				else
				{
					p1X -= 2;
					p2X += 2;
				}
			}
			if (player1.GetComponent<Player>().direction == Player.Direction.Left && player2.GetComponent<Player>().direction == Player.Direction.Left)
			{
				if (prevP1X > prevP2X) 
				{
					p1X += 2;
					p2X -= 2;
				} 
				else
				{
					p1X -= 2;
					p2X += 2;
				}
			}
			if (player1.GetComponent<Player>().direction == Player.Direction.Up && player2.GetComponent<Player>().direction == Player.Direction.Up)
			{
				if (prevP1Y > prevP2Y) 
				{
					p1Y += 2;
					p2Y -= 2;
				} 
				else
				{
					p1Y -= 2;
					p2Y += 2;
				}
			}
			if (player1.GetComponent<Player>().direction == Player.Direction.Down && player2.GetComponent<Player>().direction == Player.Direction.Down)
			{
				if (prevP1Y > prevP2Y) 
				{
					p1Y += 2;
					p2Y -= 2;
				} 
				else
				{
					p1Y -= 2;
					p2Y += 2;
				}
			}

			// Pushing animations (facing different axes)
			if (player1.GetComponent<Player> ().direction == Player.Direction.Right && (player2.GetComponent<Player> ().direction == Player.Direction.Up || player2.GetComponent<Player> ().direction == Player.Direction.Down))  
			{
				if (prevP1X == prevP2X) 
				{
					if (prevP1Y > prevP2Y) 
					{
						p1Y += 2;
						p2Y -= 2;
					} 
					else 
					{
						p1Y -= 2;
						p2Y += 2;
					}
				} 
				else 
				{
					p1X -= 2;
					p2X += 2;
				}
			} 
			else if (player2.GetComponent<Player> ().direction == Player.Direction.Right && (player1.GetComponent<Player> ().direction == Player.Direction.Up || player1.GetComponent<Player> ().direction == Player.Direction.Down)) 
			{
				if (prevP1X == prevP2X) 
				{
					if (prevP1Y > prevP2Y) 
					{
						p1Y += 2;
						p2Y -= 2;
					} 
					else 
					{
						p1Y -= 2;
						p2Y += 2;
					}
				} 
				else 
				{
					p1X -= 2;
					p2X += 2;
				}
			}

			if (player1.GetComponent<Player> ().direction == Player.Direction.Left && (player2.GetComponent<Player> ().direction == Player.Direction.Up || player2.GetComponent<Player> ().direction == Player.Direction.Down))
			{
				if (prevP1X == prevP2X) 
				{
					if (prevP1Y > prevP2Y) 
					{
						p1Y += 2;
						p2Y -= 2;
					} 
					else 
					{
						p1Y -= 2;
						p2Y += 2;
					}
				} 
				else 
				{
					p1X -= 2;
					p2X += 2;
				}
			} 
			else if (player2.GetComponent<Player> ().direction == Player.Direction.Left && (player1.GetComponent<Player> ().direction == Player.Direction.Up || player1.GetComponent<Player> ().direction == Player.Direction.Down)) 
			{

				if (prevP1X == prevP2X) 
				{
					if (prevP1Y > prevP2Y) 
					{
						p1Y += 2;
						p2Y -= 2;
					} 
					else 
					{
						p1Y -= 2;
						p2Y += 2;
					}
				} 
				else 
				{
					p1X -= 2;
					p2X += 2;
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
		prevP1X = p1X;
		prevP1Y = p1Y;
		prevP2X = p2X;
		prevP2Y = p2Y;
	}

    /// <summary>
    /// Checks audio manager for the beat. If it is the downbeat, performed the player's queued actions.
    /// </summary>
    void CheckDownbeat()
    {
        if (audio.GetComponent<AudioManager>().downbeat)
        {
            if (player1.GetComponent<Player>().queuedAction != Player.Input.Nothing)
            {
                if (player1.GetComponent<Player>().queuedAction == Player.Input.Move)
                {
                    if (player1.GetComponent<Player>().direction == Player.Direction.Up)
                    {
						//diskAnimator.SetTrigger("diskWalkUp");
						prevP1Y = p1Y;
						p1Y--;
						CheckCollisions();
                    }
                    else if (player1.GetComponent<Player>().direction == Player.Direction.Down)
                    {
						//diskAnimator.SetTrigger("diskWalkDown");
						prevP1Y = p1Y;
						p1Y++;
						CheckCollisions();
                    }
                    else if (player1.GetComponent<Player>().direction == Player.Direction.Left)
                    {
						//diskAnimator.SetTrigger("diskWalkLeft");
						prevP1X = p1X;
						p1X--;
						CheckCollisions();
                    }
                    else if (player1.GetComponent<Player>().direction == Player.Direction.Right)
                    {
						//diskAnimator.SetTrigger("diskWalkRight");
						prevP1X = p1X;
						p1X++;
						CheckCollisions();
                    }
                    player1.GetComponent<Player>().position = gridSpace[p1Y, p1X];
                }
				else if (player1.GetComponent<Player>().queuedAction == Player.Input.Attack)
				{
					ProcessAttack(player1, player2);
				}

				player1.GetComponent<Player>().queuedAction = Player.Input.Nothing;
            }

            if (player2.GetComponent<Player>().queuedAction != Player.Input.Nothing)
            {
                if (player2.GetComponent<Player>().queuedAction == Player.Input.Move)
                {
					if (player2.GetComponent<Player>().direction == Player.Direction.Up)
					{
						laserAnimator.SetTrigger("laserUpWalk");
						prevP2Y = p2Y;
						p2Y--;
						CheckCollisions();
					}
					else if (player2.GetComponent<Player>().direction == Player.Direction.Down)
					{
						laserAnimator.SetTrigger("laserDownWalk");
						prevP2Y = p2Y;
						p2Y++;
						CheckCollisions();
					}
					else if (player2.GetComponent<Player>().direction == Player.Direction.Left)
					{
						laserAnimator.SetTrigger("laserLeftWalk");
						prevP2X = p2X;
						p2X--;
						CheckCollisions();
					}
					else if (player2.GetComponent<Player>().direction == Player.Direction.Right)
					{
						laserAnimator.SetTrigger("laserRightWalk");
						prevP2X = p2X;
						p2X++;
						CheckCollisions();
					}
                    player2.GetComponent<Player>().position = gridSpace[p2Y, p2X];
                }
				else if (player2.GetComponent<Player>().queuedAction == Player.Input.Attack)
				{
					ProcessAttack(player2, player1);
				}
				player2.GetComponent<Player>().queuedAction = Player.Input.Nothing;
            }

            if (currentTexture == 1)
            {
                gameObject.GetComponent<Renderer>().material.mainTexture = floorTexture2;
                currentTexture = 2;
            }
            else
            {
                gameObject.GetComponent<Renderer>().material.mainTexture = floorTexture1;
                currentTexture = 1;
            }
        }
        player1.GetComponent<Player>().position = gridSpace[p1Y, p1X];
        player2.GetComponent<Player>().position = gridSpace[p2Y, p2X];
    }

    // Draws the buttons and lables that hold information about the scene onto the screen.
    void OnGUI()
    {
		// Font & Color
		GUI.skin.font = MyFont;
		GUI.color = Color.magenta;

		// Actions
        //GUI.Label(new Rect((Screen.width / 2.0f) - (Screen.width / 4.0f) - 250.0f, Screen.height / 2.0f, 150.0f, 25.0f), "p1 Action: " + p1Actions[p1Actions.Count - 1]);
        //GUI.Label(new Rect((Screen.width / 2.0f) + (Screen.width / 4.0f) + 125.0f, Screen.height / 2.0f, 150.0f, 25.0f), "p2 Action: " + p2Actions[p2Actions.Count - 1]);

		// Score
		GUI.Label(new Rect((Screen.width / 2.0f) - (Screen.width / 4.0f) - 250.0f, Screen.height / 2.0f - 285.0f, 150.0f, 25.0f), "p1 Score: " + player1.GetComponent<Player>().score.ToString());
		GUI.Label(new Rect((Screen.width / 2.0f) + (Screen.width / 4.0f) + 125.0f, Screen.height / 2.0f - 285.0f, 150.0f, 25.0f), "p2 Score: " + player2.GetComponent<Player>().score.ToString());
    }
}
