using System;
using System.Threading;

namespace Tron
{
	public class Tron
	{
        private const int MS_PER_FRAME = 100;

        private InputSystem input;
        private Thread inputThread;
        private Renderer renderer;
		private bool[,] gameWorld;
		private Player player1;
		private Player player2;
		private int xDim;
		private int yDim;

        private bool flag;

		public Tron(int xdim, int ydim, Renderer renderer)
		{
            input = new InputSystem();
            inputThread = new Thread(input.GetUserInput);
            xDim = xdim;
			yDim = ydim;

			// Initialize double buffer where we store the game world
			gameWorld = new bool[xdim, ydim];

			player1 = new Player(PlayerDirections.Right, 0, xdim / 2);
			player2 = new Player(PlayerDirections.Left, ydim - 1, xdim / 2);

			input.Player1KeysPressed += player1.ChangeDirection;
			input.Player2KeysPressed += player2.ChangeDirection;

			// Save renderer into variable
			this.renderer = renderer;

			CreateGameMap();
            UpdateWorldMap();
        }

        public void MainMenu()
        {
            string input;

            while (true)
            {
                renderer.ShowMainMenu();
                input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        InitializeGame(false);
                        break;
                    case "2":
                        InitializeGame(true);
                        break;
                    case "3":
                        renderer.ShowCredits();
                        break;
                    case "4":
                        Environment.Exit(1);
                        break;
                }
            }
        }

        public void InitializeGame(bool isAI)
        {
            inputThread.Start();

            while (true)
            {
                input.PauseThread = false;
                flag = false;
                Gameloop();
            }
        }

		public void Gameloop()
		{
			renderer.RenderGameWorld(gameWorld, player1, player2);
			Console.ReadKey(true);

            // Initialize game loop
            while (true)
			{
				// Obtain actual time in ticks
				long start = DateTime.Now.Ticks;

				// Update world
				Update();

                if (flag) break;

				// Send it to renderer
				renderer.RenderGameWorld(gameWorld, player1, player2);

				// Wait until it is time for the next iteration
				Thread.Sleep((int)
					(start / 10000 + MS_PER_FRAME
					- DateTime.Now.Ticks / 10000));
			}
		}

		public void Update()
		{
			MovePlayers();
            if (!VerifyWin())
                UpdateWorldMap();
            else flag = true;
		}

		private void MovePlayers()
		{
			player1.Move();
			player2.Move();
		}

		private bool VerifyWin()
		{
			bool endMatch = false;
			if (player1.DetectCollision(gameWorld) &&
				player2.DetectCollision(gameWorld))
			{
                endMatch = true;
                renderer.Draw();
				ResetGame();
			}
			else if (player1.DetectCollision(gameWorld))
			{
                endMatch = true;
                player2.IncreaseScore();
				renderer.Player2Wins();
				ResetGame();
			}
			else if (player2.DetectCollision(gameWorld))
			{
                endMatch = true;
                player1.IncreaseScore();
				renderer.Player1Wins();
				ResetGame();
			}

			return endMatch;
		}

		private void ResetGame()
		{
			CreateGameMap();
			player1.Reset(PlayerDirections.Right, xDim / 2, 0);
			player2.Reset(PlayerDirections.Left, xDim / 2, yDim - 1);
            UpdateWorldMap();
        }
		
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

		private void UpdateWorldMap()
		{
			gameWorld[player1.Row, player1.Column] = true;
			gameWorld[player2.Row, player2.Column] = true;
		}
	}
}
