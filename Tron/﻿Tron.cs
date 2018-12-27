using System;
using System.Threading;

namespace Tron
{
    public class Tron
    {
        private const int MS_PER_FRAME = 100;

        private InputSystem input;
        private Renderer renderer;
        private bool[,] gameWorld;
        private Player player1;
        private Player player2;
        private readonly int xDim;
        private readonly int yDim;
        private bool winFlag;

        public Tron(int xdim, int ydim, Renderer renderer, InputSystem input)
        {
            this.input = input;
            xDim = xdim;
            yDim = ydim;

            // Initialize buffer where we store the game world
            gameWorld = new bool[xdim, ydim];

            // Save renderer into variable
            this.renderer = renderer;
        }

        public void MainMenu()
        {
            ConsoleKey lastKey;
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
                    lastKey = input.Ki.Key;

                    switch (lastKey)
                    {
                        case ConsoleKey.D1:
                            InitializeGame(false);
                            flag = true;
                            break;
                        case ConsoleKey.D2:
                            InitializeGame(true);
                            flag = true;
                            break;
                        case ConsoleKey.D3:
                            renderer.ShowCredits();
                            input.ResetUserInput();
                            flag = true;
                            break;
                        case ConsoleKey.D4:
                            renderer.ShowControls();
                            input.ResetUserInput();
                            flag = true;
                            break;
                        case ConsoleKey.D5:
                            bool[] eventsContent = input.CheckEvents();
                            if (eventsContent[0])
                                input.Player1KeysPressed -= player1.ChangeDirection;
                            if (eventsContent[1])
                                input.Player2KeysPressed -= player2.ChangeDirection;
                            Environment.Exit(1);
                            break;
                    }

                    // If an option leading to another window has been chosen
                    // flag is true, break to the upper cycle
                    if (flag == true) break;
                }
            }
        }

        private void InitializeGame(bool isAI)
        {
            InitializePlayers(isAI);
            CreateGameMap();
            UpdateWorldMap();

            while (true)
            {
                if (input.Ki.Key == ConsoleKey.Escape) break;
                winFlag = false;
                Gameloop();
            }
        }

        private void InitializePlayers(bool isAI)
        {
            player1 = new Player(PlayerDirections.Right, 0, xDim / 2);
            input.Player1KeysPressed += player1.ChangeDirection;
            if (!isAI)
            {
                player2 = new Player(PlayerDirections.Left, yDim - 1, xDim / 2);
                input.Player2KeysPressed += player2.ChangeDirection;
            }
            else
                player2 = new AI(gameWorld, xDim, yDim, PlayerDirections.Left, yDim - 1, xDim / 2);
        }

        public void Gameloop()
        {
            renderer.RenderGameWorld(gameWorld, player1, player2);
            renderer.RenderMatchCountdown();

            // Initialize game loop
            while (true)
            {
                // Obtain actual time in ticks
                long start = DateTime.Now.Ticks;

                // Update world
                Update();

                // If someone has won, break to upperCycle
                if (winFlag) break;
                if (input.Ki.Key == ConsoleKey.Escape)
                {
                    renderer.ShowExitMessage();
                    ResetGame();
                    ResetScore();
                    break;
                }

                // Send it to renderer
                renderer.RenderGameWorld(gameWorld, player1, player2);

                // Wait until it is time for the next iteration
                Thread.Sleep((int)
                    (start / 10000 + MS_PER_FRAME
                    - DateTime.Now.Ticks / 10000));
            }
        }

        private void Update()
        {
            MovePlayers();
            if (!VerifyWin())
                UpdateWorldMap();
            else winFlag = true;
        }

        private void MovePlayers()
        {
            player1.Move();
            player2.Move();
        }

        private bool VerifyWin()
        {
            bool endMatch = false;
            if (player1.Row == player2.Row && player1.Column == player2.Column)
            {
                endMatch = true;
                renderer.RenderDraw();
                ResetGame();
            }
            else if (player1.DetectCollision(gameWorld) &&
                player2.DetectCollision(gameWorld))
            {
                endMatch = true;
                renderer.RenderDraw();
                ResetGame();
            }
            else if (player1.DetectCollision(gameWorld))
            {
                endMatch = true;
                player2.IncreaseScore();
                renderer.RenderPlayer2Wins();
                ResetGame();
            }
            else if (player2.DetectCollision(gameWorld))
            {
                endMatch = true;
                player1.IncreaseScore();
                renderer.RenderPlayer1Wins();
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

        private void ResetScore()
        {
            player1.ResetScore();
            player2.ResetScore();
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
