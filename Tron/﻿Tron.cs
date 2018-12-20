using System;
using System.Threading;

namespace Tron
{
	public class Tron
	{
		private Renderer renderer;
		private DoubleBuffer2D<bool> gameWorld;
		private Player player1;
		private Player player2;

		public Tron(int xdim, int ydim, Renderer renderer, InputSystem input)
		{
			// Initialize double buffer where we store the game world
			gameWorld = new DoubleBuffer2D<bool>(xdim, ydim);

			player1 = new Player(PlayerDirections.Left, 0, xdim / 2);
			player2 = new Player(PlayerDirections.Right, ydim - 1, xdim / 2);

			// Save renderer into variable
			this.renderer = renderer;

			// Initialize world
			for (int i = 0; i < xdim; i++)
			{
				for (int j = 0; j < ydim; j++)
				{
					// All tiles are set to false until players step on them
					gameWorld[i, j] = false;
				}
			}

			// Swap buffer arrays so we can read values from our Double Buffer
			gameWorld.Swap();
		}

		public void Gameloop(object msPerFrame)
		{
			renderer.RenderGameWorld(gameWorld);

			// Initialize game loop
			while (true)
			{
				// Obtain actual time in ticks
				long start = DateTime.Now.Ticks;

				ProcessInput();

				// Update world
				Update();

				// Swap buffer
				gameWorld.Swap();
				// Send it to renderer
				renderer.RenderGameWorld(gameWorld);

				// Wait until it is time for the next iteration
				Thread.Sleep((int)
					(start / 10000 + (int)msPerFrame
					- DateTime.Now.Ticks / 10000));
			}
		}

		public void ProcessInput()
		{

		}

		public void Update()
		{
			MovePlayers();
		}

		private void MovePlayers()
		{
			player1.Move();
			player2.Move();
			gameWorld[player1.Row, player1.Column] = true;
			gameWorld[player2.Row, player2.Column] = true;
		}
	}
}
