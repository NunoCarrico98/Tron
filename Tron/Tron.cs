using System;
using System.Threading;

namespace Tron
{
	/// <summary>
	/// Class that deals with all the engine operations.
	/// </summary>
    public class Tron
    {
		/// <summary>
		/// Constant that defines the miliseconds per frame
		/// </summary>
        private const int MS_PER_FRAME = 100;

		/// <summary>
		/// X Size of the game world.
		/// </summary>
		private readonly int xDim;
		/// <summary>
		/// Y Size of the game world.
		/// </summary>
		private readonly int yDim;
		/// <summary>
		/// Thread for the input system.
		/// </summary>
		private Thread inputThread;
		/// <summary>
		/// Reference to the input system created in Program.
		/// </summary>
		private readonly InputSystem input;
		/// <summary>
		/// Reference to the renderer created in Program.
		/// </summary>
        private readonly Renderer renderer;
		/// <summary>
		/// Array that represents the current state of the level.
		/// </summary>
        private bool[,] gameWorld;
		/// <summary>
		/// Instance of the first player.
		/// </summary>
        private Player player1;
		/// <summary>
		/// Instance of the second player.
		/// </summary>
        private Player player2;
		/// <summary>
		/// Flag that indicates if user wants to go back to main menu.
		/// </summary>
        private bool winFlag;

		/// <summary>
		/// Tron Constructor to Initialise game members.
		/// </summary>
		/// <param name="xdim">X Size of the World.</param>
		/// <param name="ydim">Y Size of the World.</param>
		/// <param name="renderer">Reference to the renderer instance created 
		/// in Program.</param>
		/// <param name="input">Reference to the input system created in 
		/// Program.</param>
		public Tron(int xdim, int ydim, Renderer renderer, InputSystem input)
        {
			// Initialise reference to the input system
            this.input = input;
			// Create and initialise input thread
			inputThread = new Thread(input.GetUserInput);
			// Start input thread
			inputThread.Start();

			// Initialise world sizes
			xDim = xdim;
            yDim = ydim;

            // Initialize buffer where we store the game world
            gameWorld = new bool[xdim, ydim];

            // Save renderer into variable
            this.renderer = renderer;
        }

		/// <summary>
		/// Method to deal with the main menu options.
		/// </summary>
        public void MainMenu()
        {
			// Flag to know when the user is on another menu.
            bool flag = false;

            // Infinite cycle for showing Main Menu options
            while (true)
            {
                // If flag is true (means option from the menu which leads to
                // another option has been selected)
                if (flag == true)
                {
                    // flag becomes false again
                    flag = false;

                    // Program only proceeds when user clicks ENTER to quit
                    // current menu window
                    while (input.Ki.Key != ConsoleKey.Enter)
                    {
                    }
                }

                // Main Menu is shown
                renderer.ShowMainMenu();

                // Infinite cycle to retrieve input from players
                while (true)
                {
					// Main Menu Options. Choose one according to user input
                    switch (input.Ki.Key)
                    {
						// If user presses 1, start a multiplayer game
                        case ConsoleKey.D1:
                            InitializeGame(false);
                            flag = true;
                            break;
						// If user presses 2, start a singleplayer game
                        case ConsoleKey.D2:
                            InitializeGame(true);
                            flag = true;
                            break;
						// If user presses 3, show the game developers
                        case ConsoleKey.D3:
							// Show credits and reset player input
                            renderer.ShowCredits();
                            input.ResetUserInput();
                            flag = true;
                            break;
						// If user presses 4, show the controls info
                        case ConsoleKey.D4:
							// Show controls and reset player input
                            renderer.ShowControls();
                            input.ResetUserInput();
                            flag = true;
                            break;
						// If user presses 5 quit game
                        case ConsoleKey.D5:
							// Check if the input events have listeners. If 
							// they do remove them from listenning.
                            bool[] eventsContent = input.CheckEvents();
                            if (eventsContent[0])
                                input.Player1KeysPressed -= player1.ChangeDirection;
                            if (eventsContent[1])
                                input.Player2KeysPressed -= player2.ChangeDirection;
							// Wait for the input thread to join the "main" thread
							inputThread.Join();
							// Quit Game
							Environment.Exit(1);
                            break;
                    }

                    // If an option leading to another window has been chosen
                    // flag is true, break to the upper cycle
                    if (flag == true) break;
                }
            }
        }

		/// <summary>
		/// Initialise a game, multiplayer or singleplayer, according to 
		/// previous user input.
		/// </summary>
		/// <param name="isAI">Bool that determines if game is singleplayer or 
		/// multiplayer.</param>
        private void InitializeGame(bool isAI)
        {
			// Create Initial World State
            InitializePlayers(isAI);
            CreateGameMap();
            UpdateWorldMap();

			// Start gameloop.
            while (true)
            {
				// Leave gameloop if user presses Escape
				if (input.Ki.Key == ConsoleKey.Escape) break;
                winFlag = false;
                Gameloop();
            }
        }

		/// <summary>
		/// Method to Initialise both players (Human or AI).
		/// </summary>
		/// <param name="isAI">Bool that determines if game is singleplayer or 
		/// multiplayer.</param>
		private void InitializePlayers(bool isAI)
        {
			// Player 1 is always a human player. Add listener to respective event
            player1 = new Player(PlayerDirections.Right, 0, xDim / 2);
            input.Player1KeysPressed += player1.ChangeDirection;
			// If game is multiplayer
            if (!isAI)
            {
				// Player 2 is human player. Add listener to respective event
                player2 = new Player(PlayerDirections.Left, yDim - 1, xDim / 2);
                input.Player2KeysPressed += player2.ChangeDirection;
            }
			// Else game is singleplayer
            else
				// Initialise player 2 as an AI
                player2 = new AI(gameWorld, xDim, yDim, PlayerDirections.Left, yDim - 1, xDim / 2);
        }

		/// <summary>
		/// Method Gameloop that is on loop while game is running.
		/// </summary>
        public void Gameloop()
        {
			// Render Initial game world state and first match countdown.
            renderer.RenderGameWorld(gameWorld, player1, player2);
            renderer.RenderMatchCountdown();

            // Initialize game loop
            while (true)
            {
                // Obtain actual time in ticks
                long start = DateTime.Now.Ticks;

                // Update world
                Update();

                // If someone has won, break to Main Menu
                if (winFlag) break;
				// If user presses Escape
                if (input.Ki.Key == ConsoleKey.Escape)
                {
					// Show quit message and reset game
                    renderer.ShowExitMessage();
                    ResetGame();
                    ResetScore();
                    break;
                }

                // Render game world
                renderer.RenderGameWorld(gameWorld, player1, player2);

                // Wait until it is time for the next iteration
                Thread.Sleep((int)
                    (start / 10000 + MS_PER_FRAME
                    - DateTime.Now.Ticks / 10000));
            }
        }

		/// <summary>
		/// Method that updates the world state.
		/// </summary>
        private void Update()
        {
			// Move both players
            MovePlayers();
			// If no one won
            if (!VerifyWin())
				// Update world state
                UpdateWorldMap();
			// Else someone won
            else winFlag = true;
        }

		/// <summary>
		/// Method that moves both players.
		/// </summary>
        private void MovePlayers()
        {
            player1.Move();
            player2.Move();
        }

		/// <summary>
		/// Method that verifies if someone won the game or if there is a draw.
		/// </summary>
		/// <returns>Return true if there is a draw or a win.</returns>
        private bool VerifyWin()
        {
            bool endMatch = false;
			// If players collide with each other at the same time
            if ((player1.DetectCollision(gameWorld) && player2.DetectCollision(gameWorld)) ||
				(player1.Row == player2.Row && player1.Column == player2.Column))
            {
				// End game as a draw
                endMatch = true;
				// NO PLAYER GETS POINTS
				// Render draw message
                renderer.RenderDraw();
				// Reset Game
                ResetGame();
            }
			// If player 1 collides
            else if (player1.DetectCollision(gameWorld))
            {
				// Player 2 Wins
                endMatch = true;
				// Increase Player Score
                player2.IncreaseScore();
				// Render Player 2 Message Wins
                renderer.RenderPlayer2Wins();
				// Reset Game
                ResetGame();
            }
			// If player 2 collides
			else if (player2.DetectCollision(gameWorld))
            {
				// Player 1 Wins
				endMatch = true;
				// Increase Player Score
				player1.IncreaseScore();
				// Render Player 1 Message Wins
				renderer.RenderPlayer1Wins();
				// Reset Game
				ResetGame();
            }

			// Returns true if there is a collision
            return endMatch;
        }

		/// <summary>
		/// Method to Reset the level.
		/// </summary>
        private void ResetGame()
        {
			// Reset World State
            CreateGameMap();
			// Reset Players
            player1.Reset(PlayerDirections.Right, xDim / 2, 0);
            player2.Reset(PlayerDirections.Left, xDim / 2, yDim - 1);
            UpdateWorldMap();
        }

		/// <summary>
		/// Method to reset player scores.
		/// </summary>
        private void ResetScore()
        {
            player1.ResetScore();
            player2.ResetScore();
        }

		/// <summary>
		/// Method to Initialise/Reset all tiles as false.
		/// </summary>
        private void CreateGameMap()
        {
            // Initialize world
            for (int i = 0; i < xDim; i++)
            {
                for (int j = 0; j < yDim; j++)
                {
                    // All tiles are set to false until players step on them
                    gameWorld[i, j] = false;
                }
            }
        }
		
		/// <summary>
		/// Method to update the map according to the players positions.
		/// </summary>
        private void UpdateWorldMap()
        {
            gameWorld[player1.Row, player1.Column] = true;
            gameWorld[player2.Row, player2.Column] = true;
        }
    }
}
